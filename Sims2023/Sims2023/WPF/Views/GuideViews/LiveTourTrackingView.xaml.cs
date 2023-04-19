using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class LiveTourTrackingView
    {
        public Tour Tour { get; set; }
        private KeyPointService _keyPointService;
        private UserService _userService;
        private TourReservationService _tourReservationService;
        private TourService _tourService;
        public List<User> MarkedGuests { get; set; }
        public LiveTourTrackingViewModel LiveTourTrackingViewModel;
        public LiveTourTrackingView(Tour selectedTour, KeyPointService keyPointService, TourReservationService tourReservationController, UserService userService, TourService tourService)
        {
            InitializeComponent();

            _keyPointService = keyPointService;
            _tourReservationService = tourReservationController;
            _userService = userService;
            _tourService = tourService;

            Tour = selectedTour;
            MarkedGuests = new List<User>();

            LiveTourTrackingViewModel = new(Tour, _keyPointService, _tourService, _tourReservationService, MarkedGuests);
            DataContext = LiveTourTrackingViewModel;
        }

        private void MarkKeyPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (LiveTourTrackingViewModel.IsKeyPointSelected() && !LiveTourTrackingViewModel.IsKeyPointVisited() &&
                LiveTourTrackingViewModel.IsKeyPointNextInLine())
            {
                LiveTourTrackingViewModel.MarkKeyPoint();
            }
            else if (LiveTourTrackingViewModel.IsKeyPointSelected() && LiveTourTrackingViewModel.IsKeyPointBeingVisited())
            {
                MessageBox.Show("Ne mozete oznaciti tacku na kojoj se trenutno nalazite");
            }
            else if (LiveTourTrackingViewModel.IsKeyPointSelected() && LiveTourTrackingViewModel.IsKeyPointVisited())
            {
                MessageBox.Show("Ne mozete oznaciti tacku koju ste prosli");
            }
            else
            {
                MessageBox.Show("Izaberite kljucnu tacku koju zelite da oznacite");
            }

            if (LiveTourTrackingViewModel.LastKeyPointVisited)
            {
                ConfirmEnd();
                Close();
            }
        }

        private void MarkGuestsPresentButton_Click(object sender, RoutedEventArgs e)
        {
            if (LiveTourTrackingViewModel.IsKeyPointSelected() && LiveTourTrackingViewModel.IsKeyPointBeingVisited())
            {
                MarkGuestsPresentView markGuestsPresentView = new(LiveTourTrackingViewModel.SelectedKeyPoint, _tourReservationService, _userService, _keyPointService, MarkedGuests);
                markGuestsPresentView.Closed += MarkGuestsPresentView_Closed;
                markGuestsPresentView.Show();
            }
            else
            {
                MessageBox.Show("Molimo odaberite kljucnu tacku za koju zelite da obelezite goste koji su se prikljucili");
            }
            LiveTourTrackingViewModel.MarkGuestsPresent();
        }

        private void MarkGuestsPresentView_Closed(object sender, EventArgs e)
        {
            LiveTourTrackingViewModel.UpdateKeyPointList();
            if (LiveTourTrackingViewModel.AreAllGuestsAreMarked())
            {
                markGuestsPresentButton.IsEnabled = false;
            }
        }

        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = ConfirmExit();
            if (result == MessageBoxResult.Yes)
            {
                LiveTourTrackingViewModel.CancelTour();
                Close();
            }
        }

        private static MessageBoxResult ConfirmExit()
        {
            string sMessageBoxText = $"Izlaskom cete prekinuti trenutnu turu\n";
            string sCaption = "Da li ste sigurni da zelite da izadjete?";

            MessageBoxButton messageBoxButton = MessageBoxButton.YesNo;
            MessageBoxImage messageBoxImage = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, messageBoxButton, messageBoxImage);
            return result;
        }

        private static MessageBoxResult ConfirmEnd()
        {
            string sMessageBoxText = $"Vasa tura se uspesno zavrsila. Potvrdite zavrsetak pritiskom na OK\n";
            string sCaption = "Potvrda zavrsetka";

            MessageBoxButton messageBoxButton = MessageBoxButton.OK;
            MessageBoxImage messageBoxImage = MessageBoxImage.Asterisk;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, messageBoxButton, messageBoxImage);
            return result;
        }
    }
}
