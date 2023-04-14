using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Sims2023.WPF.Views.Guest2Views;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Windows;
using Sims2023.WPF.Views;
using Sims2023.Observer;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    
    public class Guest2ViewModel:IObserver
    {
        private TourService _tourService;

        private LocationService _locationService;

        private TourReservationService _tourReservationService;

        private VoucherService _voucherService;

        private UserService _userService;
        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<Location> Locations { get; set; }
        public Tour SelectedTour { get; set; }
        public Tour EditedTour { get; set; }

        public User User { get; set; }

        public List<Tour> FilteredData = new List<Tour>();

        public TourReservation TourReservation { get; set; }

        private Guest2View Guest2View;


        public Guest2ViewModel(User user,Guest2View guest2View)
        {
            _tourService = new TourService();
            _locationService = new LocationService();
            _tourReservationService = new TourReservationService();
            _voucherService = new VoucherService();
            _userService = new UserService();
            

            //Tours = new ObservableCollection<Tour>(_tourService.GetAll());
            Tours = GetAvailableTours();
            Locations = new ObservableCollection<Location>(_locationService.GetAll());
            SelectedTour = new Tour();
            EditedTour = new Tour();
            User = user;
            FilteredData = new List<Tour>();
            TourReservation = new TourReservation();
            Guest2View = guest2View;

            AddLocationsToTour(Locations, Tours);
        }

        public ObservableCollection<Tour> GetAvailableTours()
        {
            ObservableCollection<Tour> availableTours = new ObservableCollection<Tour>();
            foreach (Tour tour in _tourService.GetAll())
            {
                if (tour.CurrentState == Tour.State.Created)
                {
                    availableTours.Add(tour);
                }
            }
            return availableTours;
        }
        public void AddLocationsToTour(ObservableCollection<Location> locations, ObservableCollection<Tour> tours)
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

        public void Window_Loaded()
        {
            bool confirmed = false;
            foreach (var tourReservation in _tourReservationService.GetAll())
            {
                if (tourReservation.ShouldConfirmParticipation)
                {
                    confirmed = DisplayMessageBox(tourReservation);
                    tourReservation.ShouldConfirmParticipation = false;
                    tourReservation.ConfirmedParticipation = confirmed;
                    _tourReservationService.Save();
                    break;
                }
            }

        }

        public bool DisplayMessageBox(TourReservation tourReservation)
        {
            string messageBoxText = "Do you want to confirm your participation?";
            string caption = "Confirmation";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            return (result == MessageBoxResult.Yes);
        }

        public bool ConfirmParticipation()
        {
            string messageBoxText = "Do you want to confirm your participation?";
            string caption = "Confirmation";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            return (result == MessageBoxResult.Yes);
        }


        public void SearchTours_Click()
        {
            string citySearchTerm = Guest2View.citySearchBox.Text.ToLower();
            string countrySearchTerm = Guest2View.countrySearchBox.Text.ToLower();
            string lengthSearchTerm = Guest2View.lengthSearchBox.Text.ToLower();
            string guideLanguageSearchTerm = Guest2View.guideLanguageSearchBox.Text.ToLower();
            int maxGuestNumberSearchTerm = (int)Guest2View.guestNumberBox.Value;

            FilteredData = Tours.Where(tour =>
                (string.IsNullOrEmpty(citySearchTerm) || tour.City.ToLower().Contains(citySearchTerm)) &&
                (string.IsNullOrEmpty(countrySearchTerm) || tour.Country.ToLower().Contains(countrySearchTerm)) &&
                (string.IsNullOrEmpty(lengthSearchTerm) || tour.Length.ToString().ToLower().Contains(lengthSearchTerm)) &&
                (string.IsNullOrEmpty(guideLanguageSearchTerm) || tour.GuideLanguage.ToString().ToLower().Contains(guideLanguageSearchTerm)) &&
                tour.MaxGuestNumber >= maxGuestNumberSearchTerm
            ).ToList();

            Guest2View.dataGridTours.ItemsSource = FilteredData;
            UpdateToursList();
        }

        public void MyReservations_Click()
        {
            Guest2TourListView guest2TourListView = new Guest2TourListView(User);
            guest2TourListView.Show();
        }

        public void ReserveTour_Click()
        {

            int reservedSpace = (int)Guest2View.guestNumberBox.Value;

            if (IsNull(SelectedTour))
                return;

            if (SelectedTour.AvailableSpace >= reservedSpace)
            {
                EditedTour = SelectedTour;

                TourReservation tourReservation = new TourReservation(SelectedTour, User, reservedSpace);
                _tourReservationService.Create(tourReservation);
                UpdateTours(reservedSpace);
                Guest2View.dataGridTours.ItemsSource = Tours;

                checkVouchers(tourReservation, EditedTour);

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
            Update();
        }

        public bool IsNull(Tour selectedTour)
        {
            if (selectedTour == null)
            {
                MessageBox.Show("Izaberite turu");
                return true;
            }

            return false;
        }
        public void UpdateTours(int reservedSpace)
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

        public void DisplayAlternativeTours(int reserveSpace)
        {
            FilteredData.Clear();
            Guest2View.dataGridTours.ItemsSource = Tours;
            foreach (Tour tour in Tours)
            {
                if (tour.LocationId == SelectedTour.LocationId && tour.AvailableSpace >= reserveSpace)
                    FilteredData.Add(tour);
            }

            Guest2View.dataGridTours.ItemsSource = FilteredData;
            MessageBox.Show("Izabrana tura je popunjena al u ponudi imamo ove ture sa istom lokacijom");
        }

        public void DisplaySelectedTour()
        {
            FilteredData.Clear();
            Guest2View.dataGridTours.ItemsSource = Tours;
            foreach (Tour tour in Tours)
            {
                if (tour == SelectedTour)
                    FilteredData.Add(tour);
            }

            Guest2View.dataGridTours.ItemsSource = FilteredData;
            MessageBox.Show("U ponudi je ostalo jos navedeni broj slobodnih mesta");
        }
        public void UpdateToursList()
        {
            Tours.Clear();
            foreach (var tour in _tourService.GetAll())
            {
                if (tour.CurrentState == Tour.State.Created)
                    Tours.Add(tour);
            }
        }

        public void checkVouchers(TourReservation tourReservation, Tour tour)
        {
            int CountReservation = 0;

            foreach (var reservation in _tourReservationService.GetAll())
            {
                if (tourReservation.User.Id == reservation.User.Id && tourReservation.ReservationTime.Year == reservation.ReservationTime.Year)
                {
                    CountReservation++;
                }
            }
            if (CountReservation > 0 && CountReservation % 5 == 0)
            {
                Voucher voucher = new Voucher(Voucher.VoucherType.FiveReservations, _userService.GetById(tourReservation.User.Id), _tourService.GetById(tour.Id));
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
