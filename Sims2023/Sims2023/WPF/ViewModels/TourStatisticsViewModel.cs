using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.ViewModels
{
    public partial class TourStatisticsViewModel
    {
        private TourService _tourService;
        private TourReservationService _tourReservationService;
        public Tour? Tour { get; set; }
        public Tour? SelectedTour { get; set; }
        public ObservableCollection<Tour> TheMostVisitedTour { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }
        public User LoggedInGuide { get; set; }
        public TourStatisticsViewModel(User loggedInGuide, TourService tourService, TourReservationService tourReservationService)
        {
            InitializeComponent();
            DataContext = this;

            _tourService = tourService;
            _tourReservationService = tourReservationService;
            LoggedInGuide = loggedInGuide;

            ToursToDisplay = new ObservableCollection<Tour>();
            TheMostVisitedTour = new ObservableCollection<Tour>();

            DisplayTours();
            DisplayTheMostVisitedTour();
        }

        private void DisplayTours()
        {
            foreach (var tour in _tourService.GetAll())
            {
                if ((tour.CurrentState == Tour.State.Finished || tour.CurrentState == Tour.State.Interrupted)
                    && tour.Guide.Id == LoggedInGuide.Id)
                {
                    GetAttendedGuestsNumber(tour);
                    ToursToDisplay.Add(tour);
                }
            }
        }

        private void GetAttendedGuestsNumber(Tour tour)
        {
            tour.AttendedGuestsNumber = _tourReservationService.GetAll()
                .Where(res => res.Tour.Id == tour.Id && res.ConfirmedParticipation)
                .Sum(res => res.GuestNumber);
        }

        private void DisplayTheMostVisitedTour()
        {
            TheMostVisitedTour.Add(FindTheMostVisitedTour("Svih vremena"));
        }

        private Tour FindTheMostVisitedTour(string year)
        {
            if (year == "Svih vremena")
            {
                return _tourService.GetAll().Where(tour => tour.Guide.Id == LoggedInGuide.Id &&
                    (tour.CurrentState == Tour.State.Finished || tour.CurrentState == Tour.State.Interrupted))
                    .OrderByDescending(tour => tour.AttendedGuestsNumber)
                    .FirstOrDefault();

            }
            var tour = _tourService.GetAll().Where(tour => tour.Guide.Id == LoggedInGuide.Id &&
                 (tour.CurrentState == Tour.State.Finished || tour.CurrentState == Tour.State.Interrupted) &&
                 tour.Start.Year.ToString() == year)
                 .OrderByDescending(tour => tour.AttendedGuestsNumber)
                 .FirstOrDefault();
            if (tour != null)
            {
                return tour;
            }
            else
            {
                Tour t = new();
                MessageBox.Show("Ne postoje ture u toj godini");
                return t;
            }
        }

        private void YearComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            string year = ((ComboBoxItem)cBox.SelectedItem).Content.ToString();
            UpdateMostViewedTour(year);

        }

        private void UpdateMostViewedTour(string year)
        {
            TheMostVisitedTour.Clear();
            TheMostVisitedTour.Add(FindTheMostVisitedTour(year));
        }

        private void ShowStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                FindAgeStatistics(SelectedTour);
                FindVoucherStatistics(SelectedTour);
            }
            else
            {
                MessageBox.Show("Izaberite turu molim Vas lepo");
            }
        }

        private void FindAgeStatistics(Tour tour)
        {
            int young = _tourReservationService.GetAll()
                .Where(res => res.Tour.Id == tour.Id && res.ConfirmedParticipation && res.User.Age <= 18)
                .Sum(res => res.GuestNumber);

            int middle = _tourReservationService.GetAll()
                .Where(res => res.Tour.Id == tour.Id && res.ConfirmedParticipation && res.User.Age > 18 && res.User.Age <= 50)
                .Sum(res => res.GuestNumber);

            int old = _tourReservationService.GetAll()
                .Where(res => res.Tour.Id == tour.Id && res.ConfirmedParticipation && res.User.Age > 50)
                .Sum(res => res.GuestNumber);

            youngNumberLabel.Content = young.ToString();
            middleNumberLabel.Content = middle.ToString();
            oldNumberLabel.Content = old.ToString();
        }

        private void FindVoucherStatistics(Tour tour)
        {
            var reservations = _tourReservationService.GetAll().Where(res => res.Tour.Id == tour.Id);

            int usedCounter = reservations.Count(res => res.UsedVoucher && res.ConfirmedParticipation);
            int notUsedCounter = reservations.Count(res => !res.UsedVoucher && res.ConfirmedParticipation);

            double usedPercentage = (double)usedCounter / (usedCounter + notUsedCounter);
            double notUsedPercentage = (double)notUsedCounter / (usedCounter + notUsedCounter);

            usedVoucherLabel.Content = usedPercentage.ToString("0.00");
            notUsedVoucherLabel.Content = notUsedPercentage.ToString("0.00");
        }
    }
}
