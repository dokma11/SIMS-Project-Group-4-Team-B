using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for CreateTourFromFrequentLocation.xaml
    /// </summary>
    public partial class CreateTourFromFrequentLocationView : Page
    {
        public CreateTourFromFrequentLocationViewModel CreateTourFromFrequentLocationViewModel;
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private TourReviewService _tourReviewService;
        private RequestService _requestService;
        private TourReservationService _tourReservationService;
        private VoucherService _voucherService;
        private UserService _userService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        private TourNotificationService _tourNotificationService;

        public User LoggedInGuide { get; set; }
        public CreateTourFromFrequentLocationView(Location selectedLocation, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, RequestService requestService, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, User loggedInGuide, TourNotificationService tourNotificationService)
        {
            InitializeComponent();

            CreateTourFromFrequentLocationViewModel = new(selectedLocation, tourService, keyPointService, loggedInGuide, requestService, tourNotificationService);
            DataContext = CreateTourFromFrequentLocationViewModel;

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _tourReviewService = tourReviewService;
            _requestService = requestService;
            _tourReservationService = tourReservationService;
            _voucherService = voucherService;
            _userService = userService;
            _countriesAndCitiesService = countriesAndCitiesService;

            LoggedInGuide = loggedInGuide;

            _tourNotificationService = tourNotificationService;

            foreach (var date in _tourService.GetBusyDates(LoggedInGuide))
            {
                requestDatePicker.BlackoutDates.Add(new CalendarDateRange(date, date.AddHours(1)));
            }

            requestDatePicker.DisplayDateStart = DateTime.Today.AddDays(1);
        }

        private void RequestDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateTourFromFrequentLocationViewModel.NewTour.Start = (DateTime)requestDatePicker.SelectedDate;
            CreateTourFromFrequentLocationViewModel.AddDatesToList(requestDatePicker.Text);
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (keyPointsOutput.Items.Count > 1)
            {
                CreateTourFromFrequentLocationViewModel.ConfirmCreation();
                RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
                FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            string language = ((ComboBoxItem)cBox.SelectedItem).Content.ToString();
            CreateTourFromFrequentLocationViewModel.SetToursLanguage(language);
        }

        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = keyPointTextBox.Text;
            keyPointsOutput.Items.Add(inputText);
            CreateTourFromFrequentLocationViewModel.AddKeyPointsToList(inputText);
            keyPointTextBox.Clear();
        }

        private void KeyPointTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addKeyPointsButton.IsEnabled = !string.IsNullOrEmpty(keyPointTextBox.Text);
        }

        //TOOLBAR

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            GuideHomePageView guideHomePageView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideHomePageView);
        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            GuideAccountView guideAccountView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }
    }
}
