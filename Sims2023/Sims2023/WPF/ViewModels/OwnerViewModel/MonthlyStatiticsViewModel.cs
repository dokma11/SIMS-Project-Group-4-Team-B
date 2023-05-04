using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class MonthlyStatiticsViewModel
    {
        public AccommodationStatisticsService _statisticsService;

        public AccommodationReservationService _reservationService;
        public List<AccommodationReservation> _reservations { get; set; }
        public ObservableCollection<MonthlyStatistics> Statistics { get; set; }
        public Accommodation Accommodation { get; set; }
        public int SelectedYear { get; set; }

        public MonthlyStatiticsViewModel(Accommodation selectedAccommodation, int year)
        {
            Statistics = new ObservableCollection<MonthlyStatistics>();
            _statisticsService = new AccommodationStatisticsService();
            _reservationService = new AccommodationReservationService();
            _reservations = _reservationService.GetAllReservations();
            Accommodation = selectedAccommodation;
            SelectedYear = year;
            LoadData();
        }

        private void LoadData()
        {
            List<string> mjeseci = new List<String> { "Jan", "Feb", "Mar", "Apr", "Maj", "Jun", "Jul", "Avg", "Sep", "Okt", "Nov", "Dec" };
            for (int month = 1; month <= 12; month++)
            {
                MonthlyStatistics MonthlyStat = new MonthlyStatistics
                {
                    Month = mjeseci[month-1],
                    NumReservations = CountReservations(Accommodation, SelectedYear, month),
                    NumCanceled = 0,
                    NumRescheduled = 0,
                    NumRenovationRecommendation = 0
                };
                foreach (AccommodationStatistics stat in _statisticsService.GetAll())
                {
                    if (stat.Accommodation.Name == Accommodation.Name && stat.DateOfEntry.Year == SelectedYear && stat.DateOfEntry.Month == month)
                    {
                        if (stat.isCanceled) MonthlyStat.NumCanceled++;
                        if (stat.isRescheduled) MonthlyStat.NumRescheduled++;
                        if (stat.RenovationRecommendation) MonthlyStat.NumRenovationRecommendation++;

                    }
                }
                Statistics.Add(MonthlyStat);
            }
        }

        private int CountReservations(Accommodation accommodation, int year, int month)
        {
            int counter = 0;
            foreach (AccommodationReservation reservation in _reservations)
            {
                if (reservation.Accommodation.Name == Accommodation.Name && reservation.StartDate.Year == year && reservation.EndDate.Month == month)
                {
                    counter++;
                }
            }
            return counter;
        }
    }
}
