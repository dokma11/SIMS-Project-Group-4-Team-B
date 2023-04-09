using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Sims2023.WPF.ViewModels
{
    public partial class GuideViewModel : IObserver, INotifyPropertyChanged
    {
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private UserController _userService;
        private TourReservationController _tourReservationService;
        private TourReviewController _tourReviewService;

        public Tour ?Tour { get; set; }
        public Tour ?SelectedTour { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }
        public ObservableCollection<Tour> AllTours { get; set; }

        public GuideViewModel(User user)
        {
            InitializeComponent();
            DataContext = this;

            _tourService = new TourService();
            _tourService.Subscribe(this);

            _locationService = new LocationService();
            _locationService.Subscribe(this);

            _keyPointService = new KeyPointService();
            _keyPointService.Subscribe(this);

            _userService = new UserController();
            _userService.Subscribe(this);
            
            _tourReservationService = new TourReservationController();
            _tourReservationService.Subscribe(this);

            _tourReviewService = new TourReviewController();
            _tourReviewService.Subscribe(this);

            ToursToDisplay = new ObservableCollection<Tour>();
            AllTours = new ObservableCollection<Tour>(_tourService.GetAllTours());

            DisplayTodaysTours();
        }

        private void DisplayTodaysTours()
        {
            foreach (Tour tour in AllTours)
            {
                if (tour.Start == DateTime.Today)
                {
                    ToursToDisplay.Add(tour);
                }
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourViewModel createTourViewModel = new(_tourService, _locationService, _keyPointService);
            createTourViewModel.Show();
        }

        private void StartTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null && SelectedTour.CurrentState == Tour.State.Created)
            {
                startTourButton.IsEnabled = false;
                LiveTourTrackingViewModel liveTourTrackingViewModel = new(SelectedTour, _keyPointService, _tourReservationService, _userService);
                liveTourTrackingViewModel.Closed += LiveTourTrackingView_Closed;
                liveTourTrackingViewModel.Show();
                Update();
            }
            else if (SelectedTour != null && SelectedTour.CurrentState != Tour.State.Created)
            {
                MessageBox.Show("Ne mozete zapoceti turu koja je ranije zapoceta");
            }
            else
            {
                MessageBox.Show("Odaberite turu koju zelite da zapocnete");
            }
        }

        private void LiveTourTrackingView_Closed(object sender, EventArgs e)
        {
            startTourButton.IsEnabled = true;
            Update();
            _tourService.Save();
            _keyPointService.Save();
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs eventArgs)
        {
            GuestReviewsViewModel guestReviewsViewModel = new(_tourService, _tourReviewService, _keyPointService);
            guestReviewsViewModel.Show();
        }

        public void Update()
        {
            //updating the tours that are currently displayed
            ToursToDisplay.Clear();
            AllTours.Clear();
            foreach (var tour in _tourService.GetAllTours())
            {
                AllTours.Add(tour);
                if (tour.Start == DateTime.Today)
                {
                    ToursToDisplay.Add(tour);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
