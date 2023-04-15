using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Sims2023.WPF.Views.Guest2Views;
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
        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<Location> Locations { get; set; }

        public ObservableCollection<Voucher> Vouchers { get; set; }
        public Tour SelectedTour { get; set; }
        public Tour EditedTour { get; set; }

        public User User { get; set; }
        public Voucher Voucher { get; set; }

        public List<Tour> FilteredData = new List<Tour>();

        public TourReservation TourReservation { get; set; }

        private Guest2View Guest2View;


        public Guest2ViewModel(User user,Guest2View guest2View)
        {
            _tourService = new TourService();
            _locationService = new LocationService();
            _tourReservationService = new TourReservationService();
            _voucherService = new VoucherService();
            
            Tours = new ObservableCollection<Tour>(_tourService.GetAvailable());
            Locations = new ObservableCollection<Location>(_locationService.GetAll());
            Vouchers = new ObservableCollection<Voucher>(_voucherService.GetByUser(user));
            FilteredData = new List<Tour>();
            
            SelectedTour = new Tour();
            EditedTour = new Tour();
            TourReservation = new TourReservation();
            Voucher = new Voucher();

            User = user;
            Guest2View = guest2View;

            _tourService.AddLocationsToTour(Locations, Tours);
        }

        
        public void Window_Loaded()
        {
            bool confirmed;
            foreach (var tourReservation in _tourReservationService.GetNotConfirmedParticipation())
            {
                confirmed = DisplayMessageBox(tourReservation);
                _tourReservationService.ConfirmReservation(tourReservation, confirmed);
                break;
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
                _tourService.UpdateAvailableSpace(reservedSpace, EditedTour);
                
                Update();
                MessageBox.Show("Uspesna rezervacija");
                CheckVouchers(tourReservation);
               
                ShowVoucherListView();
                
            }
            else if (SelectedTour.AvailableSpace > 0)
            {
                DisplaySelectedTour();
            }
            else
            {
                DisplayAlternativeTours(reservedSpace, SelectedTour);
            }
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
        

        public void DisplaySelectedTour()
        {
            FilteredData.Clear();
            FilteredData.Add(SelectedTour);
            Guest2View.dataGridTours.ItemsSource = FilteredData;

            MessageBox.Show($"U ponudi je ostalo još {SelectedTour.AvailableSpace} slobodnih mesta.");
        }

        public void DisplayAlternativeTours(int reservedSpace, Tour selectedTour)
        {
            Guest2View.dataGridTours.ItemsSource = _tourService.GetAlternative(reservedSpace, selectedTour);
            MessageBox.Show("Nema slobodnih mesta, ali imamo na istoj lokaciji u ponudi:");
        }

        public void ShowVoucherListView()
        {
            var voucherListView = new VoucherListView(User);
            voucherListView.Show();
        }

        public void CheckVouchers(TourReservation tourReservation)
        {
            if (_tourReservationService.CheckVouchers(tourReservation))
            {
                Voucher = new Voucher(Voucher.VoucherType.FiveReservations, User, SelectedTour);
                _voucherService.Create(Voucher);
            }
        }
        public void Update()
        {
            Guest2View.dataGridTours.ItemsSource = new ObservableCollection<Tour>(_tourService.GetAvailable());
        }
    }
}
