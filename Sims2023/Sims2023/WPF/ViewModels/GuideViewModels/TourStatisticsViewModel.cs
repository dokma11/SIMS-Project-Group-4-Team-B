using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Collections.ObjectModel;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class TourStatisticsViewModel
    {
        private TourService _tourService;
        public Tour? SelectedTour { get; set; }
        public ObservableCollection<Tour> TheMostVisitedTour { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }
        public User LoggedInGuide { get; set; }

        public TourStatisticsViewModel(TourService tourService, User loggedInGuide)
        {
            _tourService = tourService;
            LoggedInGuide = loggedInGuide;
            ToursToDisplay = new ObservableCollection<Tour>(_tourService.GetFinishedTours(LoggedInGuide));
            TheMostVisitedTour = new ObservableCollection<Tour>
            {
                _tourService.GetTheMostVisitedTour(LoggedInGuide, "Svih vremena")
            };
            GetAttendedGuestsNumber();
        }
        public void GetAttendedGuestsNumber()
        {
            _tourService.GetAttendedGuestsNumber(LoggedInGuide);
        }

        public Tour GetTheMostVisitedTour(User loggedInGuide, string year)
        {
            return _tourService.GetTheMostVisitedTour(loggedInGuide, year);
        }

        public string DisplayAgeStatistics(string ageGroup)
        {
            return _tourService.GetAgeStatistics(SelectedTour, ageGroup);
        }

        public string DisplayVoucherPercentage(bool used)
        {
            return _tourService.GetVoucherStatistics(SelectedTour, used);
        }

        public void UpdateTheMostVisitedTour(User loggedInGuide, string year)
        {
            TheMostVisitedTour.Clear();
            TheMostVisitedTour.Add(_tourService.GetTheMostVisitedTour(loggedInGuide, year));
        }
    }
}
