using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class TourStatisticsView : Window
    {
        public TourStatisticsViewModel tourStatisticsViewModel;
        public User LoggedInGuide { get; set; }

        public TourStatisticsView(User loggedInGuide, TourService tourService, TourReservationService tourReservationService)
        {
            InitializeComponent();

            LoggedInGuide = loggedInGuide;

            tourStatisticsViewModel = new(tourService, tourReservationService, LoggedInGuide);
            DataContext = tourStatisticsViewModel;
        }

        private void DisplayStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            if (tourStatisticsViewModel.IsTourSelected())
            {
                youngNumberLabel.Content = tourStatisticsViewModel.DisplayAgeStatistics("young");
                middleNumberLabel.Content = tourStatisticsViewModel.DisplayAgeStatistics("middleAged");
                oldNumberLabel.Content = tourStatisticsViewModel.DisplayAgeStatistics("old");
                usedVoucherLabel.Content = tourStatisticsViewModel.DisplayVoucherPercentage(true);
                notUsedVoucherLabel.Content = tourStatisticsViewModel.DisplayVoucherPercentage(false);
            }
            else
            {
                MessageBox.Show("Odaberite turu");
            }
        }

        private void YearComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            string year = (cBox.SelectedItem.ToString());
            tourStatisticsViewModel.UpdateTheMostVisitedTour(LoggedInGuide, year);
        }
    }
}
