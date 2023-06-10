using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class YearlyStatisticsViewModel
    {
        public AccommodationStatisticsService _statisticsService;

        public AccommodationReservationService _reservationService;
        public List<AccommodationReservation> _reservations { get; set; }
        public ObservableCollection<YearlyStatistics> Statistics { get; set; }
        public Accommodation Accommodation { get; set; }
        public YearlyStatistics SelectedYear { get; set; }

        int busiestYear = 0;

        public YearlyStatisticsViewModel(Accommodation selectedAccommodation)
        {
            Statistics = new ObservableCollection<YearlyStatistics>();
            _statisticsService = new AccommodationStatisticsService();
            _reservationService = new AccommodationReservationService();
            _reservations = _reservationService.GetAllReservations();
            Accommodation = selectedAccommodation;
            LoadData();
        }

        private void LoadData()
        {
            double maxOccupancy = 0;
            
            for (int year = 2018; year <= 2023; year++)
            {
                int NumReservationss = CountReservations(Accommodation, year);
                YearlyStatistics yearlyStat = new YearlyStatistics
                {
                    Year = year,
                    NumReservations = NumReservationss,
                    NumCanceled = 0,
                    NumRescheduled = 0,
                    NumRenovationRecommendation = 0
                };
                foreach (AccommodationStatistics stat in _statisticsService.GetAll()) 
                {
                    if (stat.Accommodation.Name == Accommodation.Name && stat.DateOfEntry.Year == year)
                    {
                        if (stat.isCanceled) yearlyStat.NumCanceled++;
                        if (stat.isRescheduled) yearlyStat.NumRescheduled++;
                        if (stat.RenovationRecommendation) yearlyStat.NumRenovationRecommendation++;

                    }
                }

                if (NumReservationss > 0)
                {
                    double occupancyPercentage = (double)NumReservationss / 365 * 100;

                    if (occupancyPercentage > maxOccupancy)
                    {
                        maxOccupancy = occupancyPercentage;
                        busiestYear = year;
                    }
                }
                Statistics.Add(yearlyStat);
            }
        }

        public ObservableCollection<YearlyStatistics> GetStatistics()
        {
            return Statistics;
        }

        private int CountReservations(Accommodation accommodation, int year)
        {
            int counter = 0;
            foreach (AccommodationReservation reservation in _reservations)
            {
                if (reservation.Accommodation.Name == Accommodation.Name && reservation.StartDate.Year == year)
                {
                    counter++;
                }
            }
            return counter;
        }

        public void Details_Click()
        {
            if (SelectedYear!=null)
            {
                MonthlyStatiticsView monthly = new MonthlyStatiticsView(Accommodation, SelectedYear.Year);
                FrameManager.Instance.MainFrame.Navigate(monthly);
            }
            
        }

        public string BusiestYear()
        {
            return busiestYear.ToString();
        }


    }
}
