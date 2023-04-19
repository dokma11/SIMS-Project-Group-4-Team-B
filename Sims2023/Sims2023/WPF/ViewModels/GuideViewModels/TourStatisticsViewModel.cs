using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class TourStatisticsViewModel
    {
        private TourService _tourService;
        private TourReservationService _tourReservationService;

        public Tour? SelectedTour { get; set; }
        public ObservableCollection<Tour> TheMostVisitedTour { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }
        public ObservableCollection<string> ComboBoxItems { get; set; }
        public User LoggedInGuide { get; set; }

        public TourStatisticsViewModel(TourService tourService, TourReservationService tourReservationService, User loggedInGuide)
        {
            _tourService = tourService;
            _tourReservationService = tourReservationService;

            LoggedInGuide = loggedInGuide;

            GetAttendedGuestsNumber();
            ToursToDisplay = new ObservableCollection<Tour>(_tourService.GetFinishedTours(LoggedInGuide));
            TheMostVisitedTour = new ObservableCollection<Tour>
            {
                _tourService.GetTheMostVisitedTour(LoggedInGuide, "Svih vremena")
            };

            ComboBoxItems = new ObservableCollection<string>();
            GetYearsForComboBox();
        }
        public void GetAttendedGuestsNumber()
        {
            _tourService.GetAttendedGuestsNumber(LoggedInGuide);
            //_tourService.SaveWrite();
            //_tourService.SaveRead();
        }

        public Tour GetTheMostVisitedTour(User loggedInGuide, string year)
        {
            return _tourService.GetTheMostVisitedTour(loggedInGuide, year);
        }

        public void GetYearsForComboBox()
        {
            foreach (var tour in _tourService.GetFinishedTours(LoggedInGuide))
            {
                if (!ComboBoxItems.Contains(tour.Start.Year.ToString()))
                {
                    ComboBoxItems.Add(tour.Start.Year.ToString());
                }
            }
            ComboBoxItems.Add("Svih vremena");
        }

        public string DisplayAgeStatistics(string ageGroup)
        {
            return _tourReservationService.GetAgeStatistics(SelectedTour, ageGroup);
        }

        public string DisplayVoucherPercentage(bool used)
        {
            return _tourReservationService.GetVoucherStatistics(SelectedTour, used);
        }

        public bool IsTourSelected()
        {
            return SelectedTour != null;
        }

        public void UpdateTheMostVisitedTour(User loggedInGuide, string year)
        {
            TheMostVisitedTour.Clear();
            TheMostVisitedTour.Add(_tourService.GetTheMostVisitedTour(loggedInGuide, year));
        }
    }
}
