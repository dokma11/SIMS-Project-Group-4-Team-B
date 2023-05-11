using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class LiveTourTrackingView : Page
    {
        public Tour Tour { get; set; }
        private KeyPointService _keyPointService;
        private UserService _userService;
        private TourReservationService _tourReservationService;
        private TourService _tourService;
        private TourReviewService _tourReviewService;
        private RequestService _requestService;
        private LocationService _locationService;
        private VoucherService _voucherService;
        private CountriesAndCitiesService _countriesAndCitiesService;

        public List<User> MarkedGuests { get; set; }
        public LiveTourTrackingViewModel LiveTourTrackingViewModel;
        public User LoggedInGuide { get; set; }
        public LiveTourTrackingView(Tour selectedTour, KeyPointService keyPointService, TourReservationService tourReservationController, UserService userService, TourService tourService, TourReviewService tourReviewService, RequestService requestService, User loggedInGuide, LocationService locationService, VoucherService voucherService, CountriesAndCitiesService countriesAndCitiesService)
        {
            InitializeComponent();

            _keyPointService = keyPointService;
            _tourReservationService = tourReservationController;
            _userService = userService;
            _tourService = tourService;
            _tourReviewService = tourReviewService;
            _requestService = requestService;
            _locationService = locationService;
            _voucherService = voucherService;
            _countriesAndCitiesService = countriesAndCitiesService;

            Tour = selectedTour;
            MarkedGuests = new List<User>();
            LoggedInGuide = loggedInGuide;

            LiveTourTrackingViewModel = new(Tour, _keyPointService, _tourService, _tourReservationService, MarkedGuests, _userService);
            DataContext = LiveTourTrackingViewModel;
        }

        private void MarkKeyPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (LiveTourTrackingViewModel.IsKeyPointSelected() && !LiveTourTrackingViewModel.IsKeyPointVisited() &&
                LiveTourTrackingViewModel.IsKeyPointNextInLine())
            {
                LiveTourTrackingViewModel.MarkKeyPoint();
            }

            SuccessfulKeyPointMarkingLabelEvent();

            //Moram dodati one gluposti kao ne mozes ovo ne mozes ono itd

            if (LiveTourTrackingViewModel.LastKeyPointVisited)
            {
                //Close();
            }
        }

        private void SuccessfulKeyPointMarkingLabelEvent()
        {
            eventLabel.Content = "Uspešno ste označili trenutnu ključnu tačku!";
            eventLabel.Visibility = Visibility.Visible;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void MarkGuestsPresentButton_Click(object sender, RoutedEventArgs e)
        {
            if (guestDataGrid.SelectedItems.Count > 0)
            {
                List<User> users = guestDataGrid.SelectedItems.Cast<User>().ToList();
                LiveTourTrackingViewModel.AddMarkedGuests(users);
            }

            LiveTourTrackingViewModel.UpdateKeyPointList();

            if (LiveTourTrackingViewModel.AreAllGuestsAreMarked())
            {
                markGuestsPresentButton.IsEnabled = false;
            }

            SuccessfulGuestMarkingLabelEvent();
        }

        private void SuccessfulGuestMarkingLabelEvent()
        {
            eventLabel.Content = "Uspešno ste dodali sve izabrane goste na turu!";
            eventLabel.Visibility = Visibility.Visible;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            LiveTourTrackingViewModel.CancelTour();
            SuccessfulCancellationLabelEvent();
        }

        private void SuccessfulCancellationLabelEvent()
        {
            eventLabel.Content = "Uspešno ste otkazali turu!";
            eventLabel.Visibility = Visibility.Visible;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RequestsButton_Click(object sender, RoutedEventArgs e)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            GuideAccountView guideAccountView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            eventLabel.Visibility = Visibility.Hidden;
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
        }

        private void NavigateToPageToursView()
        {
            NavigationService.Navigate(new Uri("/ToursView.xaml", UriKind.Relative));
        }
    }
}
