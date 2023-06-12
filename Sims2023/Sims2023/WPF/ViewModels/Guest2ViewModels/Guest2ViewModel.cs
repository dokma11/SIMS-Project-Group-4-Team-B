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

        private Guest2View Guest2View;
        public Guest2ViewModel(User user,Guest2View guest2View)
        {
            _tourService = new TourService();
            _tourReservationService = new TourReservationService();
            _voucherService = new VoucherService();
            _tourNotificationService = new TourNotificationService();
            
            Tours = new ObservableCollection<Tour>(_tourService.GetCreated());
            FilteredData = new List<Tour>();
            
            SelectedTour = null;
            User = user;
            Guest2View = guest2View;

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

        


       
    }
}
