using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Sims2023.WPF.ViewModels
{
    public partial class TourStatisticsViewModel : IObserver, INotifyPropertyChanged
    {
        private TourService _tourService;
        public Tour? Tour { get; set; }
        public Tour? SelectedTour { get; set; }
        public Tour? TheMostVisitedTour { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }
        public User LoggedInGuide { get; set; }
        public TourStatisticsViewModel(User loggedInGuide, TourService tourService)
        {
            _tourService = tourService;
            LoggedInGuide = loggedInGuide;

            DisplayTours();
            DisplayTheMostVisitedTour();
        }

        private void DisplayTours()
        {
            foreach (var tour in _tourService.GetAll())
            {
                if (tour.CurrentState == Tour.State.Finished || tour.CurrentState == Tour.State.Interrupted)
                {
                    ToursToDisplay.Add(tour);
                }
            }
        }

        private void DisplayTheMostVisitedTour()
        {

        }

        private void ShowStatisticsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {
            ToursToDisplay.Clear();
            DisplayTours();
        }
    }
}
