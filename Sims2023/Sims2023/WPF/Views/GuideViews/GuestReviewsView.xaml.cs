using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class GuestReviewsView : Page
    {
        public GuestReviewsViewModel GuestReviewsViewModel;
        private RequestService _requestService;
        private TourService _tourService;
        private LocationService _locationService;
        private TourReviewService _tourReviewService;
        private KeyPointService _keyPointService;
        private TourReservationService _tourReservationService;
        private VoucherService _voucherService;
        private UserService _userService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        public User LoggedInGuide { get; set; }
        private bool _rowExpanded;

        public GuestReviewsView(TourService tourService, TourReviewService tourReviewService, LocationService locationService, RequestService requestService, KeyPointService keyPointService, User loggedInGuide, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService)
        {
            InitializeComponent();

            _requestService = requestService;
            _tourService = tourService;
            _tourReviewService = tourReviewService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _tourReservationService = tourReservationService;
            _voucherService = voucherService;
            _userService = userService;
            _countriesAndCitiesService = countriesAndCitiesService;

            LoggedInGuide = loggedInGuide;

            GuestReviewsViewModel = new(tourService, tourReviewService, loggedInGuide);
            DataContext = GuestReviewsViewModel;

            _rowExpanded = false;
        }

        private void DisplayReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuestReviewsViewModel.IsTourSelected())
            {
                GuestReviewsViewModel.UpdateReviewsList();
            }
        }

        private void ReportReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuestReviewsViewModel.IsReviewSelected() && GuestReviewsViewModel.IsReviewValid())
            {
                GuestReviewsViewModel.ReportReview();
                SuccessfulReportLabelEvent();
            }
        }

        private void SuccessfulReportLabelEvent()
        {
            successfulReportLabel.Visibility = Visibility.Visible;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            successfulReportLabel.Visibility = Visibility.Hidden;
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
        }

        private void DisplayCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReviewDataGrid.SelectedItem != null)
            {
                DataGridRow row = (DataGridRow)ReviewDataGrid.ItemContainerGenerator.ContainerFromItem(ReviewDataGrid.SelectedItem);
                row.DetailsVisibility = Visibility.Visible;
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
            {
                DataGridRow row = (DataGridRow)ReviewDataGrid.ItemContainerGenerator.ContainerFromItem(e.RemovedItems[0]);
                row.DetailsVisibility = Visibility.Collapsed;
            }
        }

        //TOOLBAR

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            GuideHomePageView guideHomePageView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideHomePageView);
        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }

        private void RequestsButton_Click(object sender, RoutedEventArgs e)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            GuideAccountView guideAccountView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }
    }
}
