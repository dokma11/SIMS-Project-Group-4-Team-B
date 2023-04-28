using Sims2023.Application.Services;
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

        public RequestStatisticsView(RequestService requestService)
        {
            InitializeComponent();

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
    }
}
