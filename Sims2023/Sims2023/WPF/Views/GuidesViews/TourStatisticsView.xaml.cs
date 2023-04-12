using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuidesViews
{
    public partial class TourStatisticsView : Window
    {
        public Tour? Tour { get; set; }
        public Tour? SelectedTour { get; set; }
        public ObservableCollection<Tour> TheMostVisitedTour { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }
        public User LoggedInGuide { get; set; }

        public TourStatisticsViewModel tourStatisticsViewModel;

        public TourStatisticsView(User loggedInGuide, TourService tourService, TourReservationService tourReservationService)
        {
            InitializeComponent();
            DataContext = this;

            tourStatisticsViewModel = new(tourService, loggedInGuide);

            LoggedInGuide = loggedInGuide;

            ToursToDisplay = new ObservableCollection<Tour>(tourStatisticsViewModel.ToursToDisplay);
            TheMostVisitedTour = new ObservableCollection<Tour>(tourStatisticsViewModel.TheMostVisitedTour);

            GetAttendedGuestsNumber();
        }

        private void GetAttendedGuestsNumber()
        {
            tourStatisticsViewModel.GetAttendedGuestsNumber();
        }

        private void DisplayStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                youngNumberLabel.Content = tourStatisticsViewModel.DisplayAgeStatistics(SelectedTour, "young");
                middleNumberLabel.Content = tourStatisticsViewModel.DisplayAgeStatistics(SelectedTour, "middleAged");
                oldNumberLabel.Content = tourStatisticsViewModel.DisplayAgeStatistics(SelectedTour, "old");
                usedVoucherLabel.Content = tourStatisticsViewModel.DisplayVoucherPercentage(SelectedTour, true);
                notUsedVoucherLabel.Content = tourStatisticsViewModel.DisplayVoucherPercentage(SelectedTour, false);
            }
            else
            {
                //za hci moram da menjam
                MessageBox.Show("Nije odabrana tura");
            }
        }

        private void YearComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            string year = ((ComboBoxItem)cBox.SelectedItem).Content.ToString();
            UpdateTheMostVisitedTour(year);
        }

        public void UpdateTheMostVisitedTour(string year)
        {
            TheMostVisitedTour.Clear();
            TheMostVisitedTour.Add(tourStatisticsViewModel.GetTheMostVisitedTour(LoggedInGuide, year));
        }
    }
}
