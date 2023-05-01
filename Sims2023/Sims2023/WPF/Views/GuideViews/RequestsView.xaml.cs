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
        public User LoggedInGuide { get; set; }

        public RequestsView(RequestService requestService, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, User loggedInGuide)
        {
            InitializeComponent();

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _tourReviewService = tourReviewService;
            _requestService = requestService;

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

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            GuideAccountView guideAccountView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }
    }
}
