using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Sims2023.WPF.ViewModels
{
    public partial class GuideViewModel : IObserver, INotifyPropertyChanged
    {
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private UserService _userService;
        private TourReservationService _tourReservationService;
        private TourReviewService _tourReviewService;
        private VoucherService _voucherService;

        public Tour? Tour { get; set; }
        public Tour? SelectedTour { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }
        public ObservableCollection<Tour> AllTours { get; set; }
        public User LoggedInGuide { get; set; }
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

            _userService = new UserService();
            _userService.Subscribe(this);

            _tourReservationService = new TourReservationService();
            _tourReservationService.Subscribe(this);

            _tourReviewService = new TourReviewService();
            _tourReviewService.Subscribe(this);

            _voucherService = new VoucherService();
            _voucherService.Subscribe(this);

            ToursToDisplay = new ObservableCollection<Tour>();
            AllTours = new ObservableCollection<Tour>(_tourService.GetAll());

            LoggedInGuide = user;

            DisplayTours();
        }

        private void DisplayTours()
        {
            foreach (Tour tour in AllTours)
            {
                if (tour.CurrentState == Tour.State.Created && tour.Guide.Id == LoggedInGuide.Id)
                {
                    ToursToDisplay.Add(tour);
                }
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourViewModel createTourViewModel = new(_tourService, _locationService, _keyPointService, LoggedInGuide);
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

        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null && SelectedTour.Start >= DateTime.Now.AddHours(48))
            {
                SelectedTour.CurrentState = Tour.State.Cancelled;
                CreateVouchersForCancelledTour(SelectedTour.Id);
                Update();
                SuccessfulCancellationLabelEvent();
            }
            else
            {
                MessageBox.Show("Odaberite turu koju zelite da otkazete");
            }
        }

        private void CreateVouchersForCancelledTour(int toursId)
        {
            string additionalComment = Microsoft.VisualBasic.Interaction.InputBox("Unesite razlog:", "Input String");
            foreach (var reservation in _tourReservationService.GetAll())
            {
                if (reservation.Tour.Id == toursId)
                {
                    Voucher voucher = new(0,Voucher.VoucherType.CancelingTour, _userService.GetById(reservation.Tour.Id), _tourService.GetById(toursId), DateTime.Now, DateTime.Today.AddYears(1), additionalComment, false);
                    _voucherService.Create(voucher);
                }
            }
        }

        private void SuccessfulCancellationLabelEvent()
        {
            successfulCancellationLabel.Visibility = Visibility.Visible;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            successfulCancellationLabel.Visibility = Visibility.Hidden;
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
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

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            TourStatisticsViewModel tourStatisticsViewModel = new(LoggedInGuide, _tourService, _tourReservationService);
            tourStatisticsViewModel.Show();
        }

        public void Update()
        {
            ToursToDisplay.Clear();
            DisplayTours();
            _tourService.Save();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
