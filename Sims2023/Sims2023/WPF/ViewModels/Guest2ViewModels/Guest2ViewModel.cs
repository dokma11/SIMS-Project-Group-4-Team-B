using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Sims2023.WPF.Views.Guest2Views;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Windows;
using Sims2023.WPF.Views;
using Sims2023.Observer;
using System.Windows.Navigation;
using Sims2023.WPF.Commands;
using System;
using System.Windows.Controls;
using Sims2023.WPF.Views.Guest2Views.Themes;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{   
    public class Guest2ViewModel//IObserver
    {
        private string currentTheme;
        public string CurrentTheme
        {
            get { return currentTheme; }
            set
            {
                currentTheme = value;
            }
        }


        private TourReservationService _tourReservationService;

        private TourNotificationService _tourNotificationService;
       
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        public User User { get; set; }
        public List<Tour> FilteredData { get; set; }

        private Guest2View Guest2View;
        private App app;
        public NavigationService NavService { get; set; }
        public RelayCommand NavigateToStartPageCommand { get; set; }
        public RelayCommand NavigateToTourListPageCommand { get; set; }
        public RelayCommand NavigateToGuest2TourListPageCommand { get; set; }
        public RelayCommand NavigateToGuest2TourRequestListPageCommand { get; set; }
        public RelayCommand NavigateToGuest2ComplexTourRequestListPageCommand { get; set; }
        public RelayCommand NavigateToGuest2VoucherListPageCommand { get; set; }
        public RelayCommand OpenReportCommand { get; set; }
        public RelayCommand OpenNotificationWindowCommand { get; set; }
        public RelayCommand SwitchToSerbianLanguageCommand { get; set; }
        public RelayCommand SwitchToEnglishLanguageCommand { get; set; }
        public RelayCommand SwitchThemeCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        private const string SRB = "sr-LATN";
        private const string ENG = "en-US";
        private const string DARK = "dark";
        private const string LIGHT = "light";
        public Guest2ViewModel(User user, NavigationService navService,Guest2View guest2View)
        {
            this._tourReservationService = new TourReservationService();
            this._tourNotificationService = new TourNotificationService();
            this.CurrentTheme = "LIGHT";
            this.NavigateToStartPageCommand = new RelayCommand(Execute_NavigateToStartPageCommand, CanExecute_NavigateCommand);
            this.NavigateToTourListPageCommand = new RelayCommand(Execute_NavigateToTourListPageCommand, CanExecute_NavigateCommand);
            this.NavigateToGuest2TourListPageCommand = new RelayCommand(Execute_NavigateToGuest2TourListPageCommand, CanExecute_NavigateCommand);
            this.NavigateToGuest2TourRequestListPageCommand = new RelayCommand(Execute_NavigateToGuest2TourRequestListPageCommand, CanExecute_NavigateCommand);
            this.NavigateToGuest2ComplexTourRequestListPageCommand = new RelayCommand(Execute_NavigateToGuest2ComplexTourRequestListPageCommand, CanExecute_NavigateCommand);
            this.NavigateToGuest2VoucherListPageCommand= new RelayCommand(Execute_NavigateToGuest2VoucherListPageCommand,CanExecute_NavigateCommand);
            this.LogoutCommand = new RelayCommand(Execute_LogoutCommand,CanExecute_NavigateCommand);
            this.SwitchToSerbianLanguageCommand=new RelayCommand(Execute_SwitchToSerbianLanguageCommand, CanExecute_NavigateCommand);
            this.SwitchToEnglishLanguageCommand=new RelayCommand(Execute_SwitchToEnglishLanguageCommand, CanExecute_NavigateCommand); 
            this.SwitchThemeCommand=new RelayCommand(Execute_SwichThemeCommand, CanExecute_NavigateCommand);
            this.OpenReportCommand=new RelayCommand(Execute_OpenReportCommand, CanExecute_NavigateCommand);

            app = (App)System.Windows.Application.Current;
            Guest2View = guest2View;
            FrameManagerGuest2.Instance.MainFrame = Guest2View.MainFrameGuest2;
            NavigateToStartPageCommand.Execute(this);
            

            



            this.NavService = navService;
            this.User = user;


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

        private void Execute_NavigateToStartPageCommand(object obj)
        {

            Page Guest2StartView = new Guest2StartView();
            FrameManagerGuest2.Instance.MainFrame.Navigate(Guest2StartView);
        }
        private void Execute_NavigateToTourListPageCommand(object obj)
        {
            
            Page tourList = new TourListView(User);
            FrameManagerGuest2.Instance.MainFrame.Navigate(tourList);
        }
        private void Execute_NavigateToGuest2TourListPageCommand(object obj)
        {
            Page Guest2TourListView = new Guest2TourListView(User);
            FrameManagerGuest2.Instance.MainFrame.Navigate(Guest2TourListView);
        }
        private void Execute_NavigateToGuest2TourRequestListPageCommand(object obj)
        {
            Page Guest2TourRequestListView = new Guest2TourRequestListView(User);
            FrameManagerGuest2.Instance.MainFrame.Navigate(Guest2TourRequestListView);
        }
        private void Execute_NavigateToGuest2ComplexTourRequestListPageCommand(object obj)
        {
            Page Guest2ComplexTourRequestListView = new Guest2ComplexTourRequestListView(User);
            FrameManagerGuest2.Instance.MainFrame.Navigate(Guest2ComplexTourRequestListView);
        }
        private void Execute_NavigateToGuest2VoucherListPageCommand(object obj)
        {
            Page Guest2VoucherListView = new Guest2VoucherListView(User);
            FrameManagerGuest2.Instance.MainFrame.Navigate(Guest2VoucherListView);
        }
        private void Execute_LogoutCommand(object obj)
        {
            MainWindow LogIn = new();
            LogIn.Show();
            Guest2View.Close();
        }
        private void Execute_SwitchToSerbianLanguageCommand(object obj)
        {
            app.ChangeLanguage(SRB);
        }
        private void Execute_SwitchToEnglishLanguageCommand(object obj)
        {
            app.ChangeLanguage(ENG);
        }
        private void Execute_SwichThemeCommand(object obj)
        {
            if (CurrentTheme == "DARK")
            {
                AppTheme.ChangeTheme(new Uri("WPF/Views/Guest2Views/Themes/Light.xaml", UriKind.Relative));
                CurrentTheme = "LIGHT";
            }
            else
            {
                AppTheme.ChangeTheme(new Uri("WPF/Views/Guest2Views/Themes/Dark.xaml", UriKind.Relative));
                CurrentTheme = "DARK";
            }
        }

        private void Execute_OpenReportCommand(object obj)
        {
            Guest2ReportView Guest2ReportView = new Guest2ReportView(User);
            Guest2ReportView.Show();
        }
        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }







    }
}
