using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for RequestStatisticsView.xaml
    /// </summary>
    public partial class RequestStatisticsView : Window
    {
        public RequestStatisticsViewModel RequestStatisticsViewModel;
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        public User LoggedInGuide { get; set; }

        public RequestStatisticsView(RequestService requestService, TourService tourService, LocationService locationService, KeyPointService keyPointService, User loggedInGuide)
        {
            InitializeComponent();

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;

            LoggedInGuide = loggedInGuide;

            RequestStatisticsViewModel = new(requestService);
            DataContext = RequestStatisticsViewModel;

            languageComboBox.ItemsSource = RequestStatisticsViewModel.GetLanguages();
            locationComboBox.ItemsSource = RequestStatisticsViewModel.GetLocations();
            languageYearComboBox.ItemsSource = RequestStatisticsViewModel.GetYears();
            locationYearComboBox.ItemsSource = RequestStatisticsViewModel.GetYears();
        }

        private void LanguageComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (languageComboBox.SelectedItem != null && languageYearComboBox.SelectedItem != null)
            {
                RequestStatisticsViewModel.DisplayLanguageStatistics(languageComboBox.SelectedItem.ToString(), languageYearComboBox.SelectedItem.ToString());
            }
        }

        private void LocationComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (locationComboBox.SelectedItem != null && locationYearComboBox.SelectedItem != null)
            {
                RequestStatisticsViewModel.DisplayLocationStatistics(locationComboBox.SelectedItem.ToString(), locationYearComboBox.SelectedItem.ToString());
            }
        }

        private void LanguageConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourFromFrequentLanguageView createTourFromFrequentLanguageView = new(RequestStatisticsViewModel.TheMostRequestedLanguage, _tourService, _locationService, _keyPointService, LoggedInGuide);
            createTourFromFrequentLanguageView.Show();
        }
        
        private void LocationConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourFromFrequentLocationView createTourFromFrequentLocationView = new(RequestStatisticsViewModel.TheMostRequestedLocation, _tourService, _keyPointService, LoggedInGuide);
            createTourFromFrequentLocationView.Show();
        }
    }
}
