using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Sims2023.WPF;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.Views
{
    /// <summary>
    /// Interaction logic for Guest2View.xaml
    /// </summary>
    public partial class Guest2View : Window, IObserver
    {

        private TourService _tourService;

        private LocationService _locationService;

        private TourReservationController _tourReservationController;

        private VoucherService _voucherService;

        private UserService _userService;
        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<Location> Locations { get; set; }
        public Tour SelectedTour { get; set; }
        public Tour EditedTour { get; set; }

        public User User { get; set; }

        public List<Tour> FilteredData = new List<Tour>();






        public TourReservation TourReservation { get; set; }

        public Guest2View(User user)
        {
            InitializeComponent();
            DataContext = this;

            _tourService = new TourService();
            _tourService.Subscribe(this);

            _locationService = new LocationService();
            _locationService.Subscribe(this);

            _tourReservationController = new TourReservationController();
            _tourReservationController.Subscribe(this);

            _voucherService = new VoucherService();
            _voucherService.Subscribe(this);

            _userService = new UserService();
            _userService.Subscribe(this);

            Tours = new ObservableCollection<Tour>(_tourService.GetAll());
            Locations = new ObservableCollection<Location>(_locationService.GetAll());

            SelectedTour = new Tour();
            EditedTour = new Tour();
            User = user;
            FilteredData = new List<Tour>();

            AddLocationsToTour(Locations, Tours);

           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DisplayMessageBox())
            {
                ConfirmParticipation();
            }
        }

        private bool DisplayMessageBox() 
        {
            foreach(var tourReservation in _tourReservationController.GetAllReservations())
            {
                if (tourReservation.ShouldConfirmParticipation)
                {
                    tourReservation.ShouldConfirmParticipation = false;
                    _tourReservationController.Save();
                    return true;
                }
            }
            return false;
        }
        private static MessageBoxResult ConfirmParticipation()
        {
            string sMessageBoxText = $"Potvrdite Vase prisustvo na turi pritskom na taster OK\n";
            string sCaption = "Potvrda prisustva";

            MessageBoxButton messageBoxButton = MessageBoxButton.OK;
            MessageBoxImage messageBoxImage = MessageBoxImage.Asterisk;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, messageBoxButton, messageBoxImage);
            return result;
        }

        private void AddLocationsToTour(ObservableCollection<Location> locations, ObservableCollection<Tour> tours)
        {

            foreach (var tour in tours)
            {
                foreach (var location in locations)
                {
                    if (tour.LocationId == location.Id)
                    {
                        tour.City = location.City;
                        tour.Country = location.Country;
                    }
                }
            }

        }

        private void SearchTours_Click(object sender, RoutedEventArgs e)
        {

            FilteredData.Clear();
            dataGridTours.ItemsSource = Tours;

            string citySearchTerm = citySearchBox.Text;
            string countrySearchTerm = countrySearchBox.Text;
            string lengthSearchTerm = lengthSearchBox.Text;
            string guideLanguageSearchTerm = guideLanguageSearchBox.Text.ToLower();
            int maxGuestNumberSearchTerm = (int)guestNumberBox.Value;





            foreach (Tour tour in Tours)
            {

                bool cityCondition = true;
                bool countryCondition = true;
                bool lengthCondition = true;
                bool guideLanguageCondition = true;
                bool maxGuestNumberCondition = true;

                if (!string.IsNullOrEmpty(citySearchTerm))
                {
                    if (!tour.City.ToLower().Contains(citySearchTerm.ToLower()))
                    {
                        cityCondition = false;
                    }
                }

                if (!string.IsNullOrEmpty(countrySearchTerm))
                {
                    if (!tour.Country.ToLower().Contains(countrySearchTerm.ToLower()))
                    {
                        countryCondition = false;
                    }
                }
                if (!string.IsNullOrEmpty(lengthSearchTerm))
                {
                    if (!tour.Length.ToString().ToLower().Contains(lengthSearchTerm.ToLower()))
                    {
                        lengthCondition = false;
                    }
                }
                if (!string.IsNullOrEmpty(guideLanguageSearchTerm))
                {
                    if (!tour.GuideLanguage.ToString().ToLower().Contains(guideLanguageSearchTerm.ToLower()))
                    {
                        guideLanguageCondition = false;
                    }
                }
                //if (guestNumberBox.Value > 0)
                {
                    if (tour.MaxGuestNumber < maxGuestNumberSearchTerm)
                    {
                        maxGuestNumberCondition = false;
                    }
                }


                if (cityCondition && countryCondition && lengthCondition && guideLanguageCondition && maxGuestNumberCondition)
                {
                    FilteredData.Add(tour);

                }

            }

            dataGridTours.ItemsSource = FilteredData;




        }

        private void ReserveTour_Click(object sender, RoutedEventArgs e)
        {

            int reservedSpace = (int)guestNumberBox.Value;


            if (IsNull(SelectedTour))
                return;
            

            if (SelectedTour.AvailableSpace >= reservedSpace)
            {
                EditedTour = SelectedTour;
                TourReservation tourReservation = new TourReservation();

                tourReservation.TourId = SelectedTour.Id;
                tourReservation.UserId = User.Id;
                tourReservation.GuestNumber = reservedSpace;
                _tourReservationController.Create(tourReservation);
                UpdateTours(reservedSpace);
                dataGridTours.ItemsSource = Tours;
                checkVouchers(tourReservation,EditedTour);
                VoucherListView voucherListView = new VoucherListView(User);
                voucherListView.Show();
            }
            else if (SelectedTour.AvailableSpace > 0)
            {
                DisplaySelectedTour();
            }
            else
            {

                DisplayAlternativeTours(reservedSpace);
            }

        }

        private bool IsNull(Tour selectedTour)
        {
            if (selectedTour == null)
            {
                MessageBox.Show("Izaberite turu");
                return true;
            }

            return false;
        }




        /*private bool IsEmpty(TextBox textBox)
        {
            int value;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("Popuni potrebno polje");
                return true;
            }
            if (!int.TryParse(textBox.Text, out value))
            {
                MessageBox.Show("Unesite broj");
                return true;
            }

            return false;
        }*/

        private void UpdateTours(int reservedSpace)
        {
            foreach (Tour tour in Tours)
            {
                if (EditedTour == tour)
                {
                    EditedTour.AvailableSpace -= reservedSpace;
                    _tourService.Edit(EditedTour, tour);
                    break;
                }
            }
            MessageBox.Show("Uspesna rezervacija");
        }

        private void DisplayAlternativeTours(int reserveSpace)
        {
            FilteredData.Clear();
            dataGridTours.ItemsSource = Tours;
            foreach (Tour tour in Tours)
            {
                if (tour.LocationId == SelectedTour.LocationId && tour.AvailableSpace >= reserveSpace)
                    FilteredData.Add(tour);
            }

            dataGridTours.ItemsSource = FilteredData;
            MessageBox.Show("Izabrana tura je popunjena al u ponudi imamo ove ture sa istom lokacijom");
        }

        private void DisplaySelectedTour()
        {
            FilteredData.Clear();
            dataGridTours.ItemsSource = Tours;
            foreach (Tour tour in Tours)
            {
                if (tour == SelectedTour)
                    FilteredData.Add(tour);
            }

            dataGridTours.ItemsSource = FilteredData;
            MessageBox.Show("U ponudi je ostalo jos navedeni broj slobodnih mesta");
        }
        private void UpdateToursList()
        {
            Tours.Clear();
            foreach (var tour in _tourService.GetAll())
            {
                Tours.Add(tour);
            }
        }

        private void checkVouchers(TourReservation tourReservation,Tour tour)
        {
            int CountReservation = 0;
         
            foreach (var reservation in _tourReservationController.GetAllReservations())
            {
                if (tourReservation.UserId == reservation.UserId && tourReservation.ReservationTime.Year == reservation.ReservationTime.Year)
                {
                    CountReservation++;
                }
            }
            if (CountReservation > 0 && CountReservation % 5 ==0)
            {
                    Voucher voucher= new Voucher(Voucher.VoucherType.FiveReservations, _userService.GetById(tourReservation.UserId), _tourService.GetById(tour.Id));
                    _voucherService.Create(voucher);
                    MessageBox.Show("Osvojili ste kupon");
            }
        }

        public void Update()
        {
            UpdateToursList();
        }
    }
}
