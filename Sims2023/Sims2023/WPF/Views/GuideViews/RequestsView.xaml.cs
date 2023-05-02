using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for RequestStatisticsView.xaml
    /// </summary>
    public partial class RequestsView : Page
    {
        public RequestsViewModel RequestsViewModel;
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private TourReviewService _tourReviewService;
        private RequestService _requestService;
        private TourReservationService _tourReservationService;
        private VoucherService _voucherService;
        private UserService _userService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        public User LoggedInGuide { get; set; }

        public RequestsView(RequestService requestService, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, User loggedInGuide, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService)
        {
            InitializeComponent();

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _tourReviewService = tourReviewService;
            _requestService = requestService;
            _tourReservationService = tourReservationService;
            _countriesAndCitiesService = countriesAndCitiesService;
            _voucherService = voucherService;   
            _userService = userService;

            LoggedInGuide = loggedInGuide;

            RequestsViewModel = new(requestService);
            DataContext = RequestsViewModel;

            languageComboBox.ItemsSource = RequestsViewModel.GetLanguages();
            locationComboBox.ItemsSource = RequestsViewModel.GetLocations();
            languageYearComboBox.ItemsSource = RequestsViewModel.GetYears();
            locationYearComboBox.ItemsSource = RequestsViewModel.GetYears();
        }

        private void LanguageComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (languageComboBox.SelectedItem != null && languageYearComboBox.SelectedItem != null)
            {
                RequestsViewModel.DisplayLanguageStatistics(languageComboBox.SelectedItem.ToString(), languageYearComboBox.SelectedItem.ToString());
            }
        }

        private void LocationComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (locationComboBox.SelectedItem != null && locationYearComboBox.SelectedItem != null)
            {
                RequestsViewModel.DisplayLocationStatistics(locationComboBox.SelectedItem.ToString(), locationYearComboBox.SelectedItem.ToString());
            }
        }

        private void LanguageConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourFromFrequentLanguageView createTourFromFrequentLanguageView = new(RequestsViewModel.TheMostRequestedLanguage, _tourService, _locationService, _keyPointService, LoggedInGuide);
            createTourFromFrequentLanguageView.Show();
        }

        private void LocationConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourFromFrequentLocationView createTourFromFrequentLocationView = new(RequestsViewModel.TheMostRequestedLocation, _tourService, _keyPointService, LoggedInGuide);
            createTourFromFrequentLocationView.Show();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string locationSearchTerm = locationTextBox.Text;
            string guestNumberSearchTerm = guestNumberTextBox.Text;
            string languageSearchTerm = languageTextBox.Text;
            string dateStartSearchTerm = dateStartTextBox.Text;
            string dateEndSearchTerm = dateEndTextBox.Text;

            requestDataGrid.ItemsSource = RequestsViewModel.FilterRequests(locationSearchTerm, guestNumberSearchTerm, languageSearchTerm, dateStartSearchTerm, dateEndSearchTerm);
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourFromRequestView createTourFromRequestView = new(RequestsViewModel.SelectedRequest, LoggedInGuide, _tourService, _keyPointService);
            createTourFromRequestView.Show();
            RequestsViewModel.AcceptRequest();
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            RequestsViewModel.DeclineRequest();
        }

        //TOOLBAR

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            GuideHomePageView guideHomePageView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideHomePageView);
        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
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
    }
}
