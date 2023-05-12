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
    public class Guest2ViewModel//IObserver
    {
        private TourService _tourService;

        private TourReservationService _tourReservationService;

        private VoucherService _voucherService;

       

        private TourNotificationService _tourNotificationService;
       
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        public User User { get; set; }
        public List<Tour> FilteredData { get; set; }

        private Guest2View TourListView;
        public Guest2ViewModel(User user,Guest2View tourListView)
        {
            _tourService = new TourService();
            _tourReservationService = new TourReservationService();
            _voucherService = new VoucherService();
            _tourNotificationService = new TourNotificationService();
            
            Tours = new ObservableCollection<Tour>(_tourService.GetCreated());
            FilteredData = new List<Tour>();
            
            SelectedTour = null;
            User = user;
            TourListView = tourListView;

        }

        
        public void Window_Loaded()
        {
            bool confirmedParticipation;
            foreach (var tourReservation in _tourReservationService.GetNotConfirmedParticipation())
            {
                confirmedParticipation = DisplayReservationConfirmationMessageBox(tourReservation);
                _tourReservationService.ConfirmReservation(tourReservation, confirmedParticipation);
                break;
            }

            DisplayAcceptedTourMessageBox();
            DisplayMatchedTourRequestsLocationMessageBox();
            DisplayMatchedTourRequestsLanguageMessageBox();
            

        }

        public void DisplayAcceptedTourMessageBox()
        {
            foreach(TourNotification tourNotification in _tourNotificationService.GetAcceptedTourRequest(User))
            {
                _tourNotificationService.SetIsNotified(tourNotification);
                string message = $"Name: {tourNotification.Tour.Name}\nLocation: {tourNotification.Tour.Location.City},{tourNotification.Tour.Location.Country}\nLanguage:{tourNotification.Tour.GuideLanguage}\nStart:{tourNotification.Tour.Start}";
                MessageBox.Show(message, "Accepted tour request");
            }
        }

        public void DisplayMatchedTourRequestsLocationMessageBox()
        {
            foreach (TourNotification tourNotification in _tourNotificationService.GetMatchedTourRequestsLocation(User))
            {
                _tourNotificationService.SetIsNotified(tourNotification);
                string message = $"Name: {tourNotification.Tour.Name}\nLocation: {tourNotification.Tour.Location.City},{tourNotification.Tour.Location.Country}\nLanguage:{tourNotification.Tour.GuideLanguage}\nStart:{tourNotification.Tour.Start}";
                MessageBox.Show(message, "New tour with location same as your request");
            }
        }

        public void DisplayMatchedTourRequestsLanguageMessageBox()
        {
            foreach (TourNotification tourNotification in _tourNotificationService.GetMatchedTourRequestsLanguage(User))
            {
                _tourNotificationService.SetIsNotified(tourNotification);
                string message = $"Name: {tourNotification.Tour.Name}\nLocation: {tourNotification.Tour.Location.City},{tourNotification.Tour.Location.Country}\nLanguage:{tourNotification.Tour.GuideLanguage}\nStart:{tourNotification.Tour.Start}";
                MessageBox.Show(message, "New tour with language same as your request");
            }
        }

        public bool DisplayReservationConfirmationMessageBox(TourReservation tourReservation)
        {
            string messageBoxText = "Do you want to confirm your participation?";
            string caption = "Confirmation";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            return (result == MessageBoxResult.Yes);
        }

        


       /*public void SearchTours_Click()
        {
            string citySearchTerm = TourListView.citySearchBox.Text.ToLower();
            string countrySearchTerm = TourListView.countrySearchBox.Text.ToLower();
            string lengthSearchTerm = TourListView.lengthSearchBox.Text.ToLower();
            string guideLanguageSearchTerm = TourListView.guideLanguageSearchBox.Text.ToLower();
            int maxGuestNumberSearchTerm = (int)TourListView.guestNumberBox.Value;

            FilteredData = _tourService.GetFiltered(citySearchTerm, countrySearchTerm, lengthSearchTerm, guideLanguageSearchTerm, maxGuestNumberSearchTerm);
            TourListView.dataGridTours.ItemsSource = FilteredData;
        }

        public void MyReservations_Click()
        {
            Guest2TourListView guest2TourListView = new Guest2TourListView(User);
            guest2TourListView.Show();
        }

        public void ReserveTour_Click()
        {
            int reservedSpace = (int)TourListView.guestNumberBox.Value;

            if (IsNull(SelectedTour))
                return;

            if (SelectedTour.AvailableSpace >= reservedSpace)
            {
                TourReservation tourReservation = new TourReservation(SelectedTour, User, reservedSpace);
                _tourReservationService.Create(tourReservation);
                _tourService.UpdateAvailableSpace(reservedSpace, SelectedTour);
                
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
            TourListView.dataGridTours.ItemsSource = FilteredData;

            MessageBox.Show($"U ponudi je ostalo još {SelectedTour.AvailableSpace} slobodnih mesta.");
        }

        public void DisplayAlternativeTours(int reservedSpace, Tour selectedTour)
        {
            TourListView.dataGridTours.ItemsSource = _tourService.GetAlternatives(reservedSpace, selectedTour);
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
                Voucher Voucher = new Voucher(Voucher.VoucherType.FiveReservations, User, SelectedTour);
                _voucherService.Create(Voucher);
            }
        }

        public void SeeDetails_Click()
        {
            if (IsNull(SelectedTour))
                return;
            else
            {
                TourDetailedView TourDetailedView = new TourDetailedView(SelectedTour);
                TourDetailedView.Show();
            }
        }

        public void CreateTourRequest_Click()
        {
            CreateTourRequestView CreateTourRequestView = new CreateTourRequestView(User);
            CreateTourRequestView.Show();
        }
        public void Update()
        {
            TourListView.dataGridTours.ItemsSource = new ObservableCollection<Tour>(_tourService.GetCreated());
        }*/
    }
}
