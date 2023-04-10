using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.ViewModels
{
    public partial class TourStatisticsViewModel : IObserver, INotifyPropertyChanged
    {
        private TourService _tourService;
        private TourReservationController _tourReservationService;
        public Tour? Tour { get; set; }
        public Tour? SelectedTour { get; set; }
        public ObservableCollection<Tour> TheMostVisitedTour { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }
        public User LoggedInGuide { get; set; }
        public TourStatisticsViewModel(User loggedInGuide, TourService tourService, TourReservationController tourReservationService)
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
                    ToursToDisplay.Add(tour);
                }
            }
        }

        private void DisplayTheMostVisitedTour()
        {
            TheMostVisitedTour.Add(FindTheMostVisitedTour("2023"));
        }

        private Tour FindTheMostVisitedTour(string year)
        {
            int max = 0;
            Tour best = new();
            foreach (var tour in _tourService.GetAll())
            {
                if ((tour.CurrentState == Tour.State.Finished || tour.CurrentState == Tour.State.Interrupted) &&
                    tour.MaxGuestNumber > max && tour.Start.Year.ToString() == year && tour.Guide.Id == LoggedInGuide.Id)
                {
                    best = tour;
                    max = tour.MaxGuestNumber;
                }
            }
            return best;
        }

        private void YearComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //TheMostVisitedTour.Add(FindTheMostVisitedTour(yearComboBox.SelectedItem.ToString()));
            ComboBox cBox = (ComboBox)sender;
            string year = ((ComboBoxItem)cBox.SelectedItem).Content.ToString();

            switch (year)
            {
                case "2023":
                    UpdateMostViewedTour("2023");
                    break;
                case "2022":
                    UpdateMostViewedTour("2022");
                    break;
                case "2021":
                    UpdateMostViewedTour("2021");
                    break;
                case "2020":
                    UpdateMostViewedTour("2020");
                    break;
            }
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
            int young = 0, middle = 0, old = 0;
            foreach (var res in _tourReservationService.GetAllReservations())
            {
                if (res.Tour.Id == tour.Id)
                {
                    if (res.User.Age <= 18)
                    {
                        young += res.GuestNumber;
                    }
                    else if (res.User.Age <= 50)
                    {
                        middle += res.GuestNumber;
                    }
                    else
                    {
                        old += res.GuestNumber;
                    }
                }
            }
            youngNumberLabel.Content = young.ToString();
            middleNumberLabel.Content = middle.ToString();
            oldNumberLabel.Content = old.ToString();
        }

        private void FindVoucherStatistics(Tour tour)
        {
            int usedCounter = 0;
            int notUsedCounter = 0;
            foreach (var res in _tourReservationService.GetAllReservations())
            {
                if (res.Tour.Id == tour.Id)
                {
                    if (res.UsedVoucher)
                    {
                        usedCounter++;
                    }
                    else
                    {
                        notUsedCounter++;
                    }
                }

            }
            double usedPercentage = Convert.ToDouble(usedCounter) / Convert.ToDouble(usedCounter + notUsedCounter);
            double notUsedPercentage = Convert.ToDouble(notUsedCounter) / Convert.ToDouble(usedCounter + notUsedCounter);
            usedVoucherLabel.Content = usedPercentage.ToString("0.00");
            notUsedVoucherLabel.Content = notUsedPercentage.ToString("0.00");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {
            ToursToDisplay.Clear();
            TheMostVisitedTour.Clear();
            DisplayTours();
            DisplayTheMostVisitedTour();
        }
    }
}
