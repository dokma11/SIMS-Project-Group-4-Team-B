using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class GuideView : IObserver
    {
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private UserService _userService;
        private TourReservationService _tourReservationService;
        private TourReviewService _tourReviewService;
        private VoucherService _voucherService;
        private RequestService _requestService;
        public User LoggedInGuide { get; set; }
        public GuideViewModel GuideViewModel;
        public GuideView(User user)
        {
            InitializeComponent();

            LoggedInGuide = user;

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

            _requestService = new RequestService();
            _requestService.Subscribe(this);

            GuideViewModel = new(LoggedInGuide, _tourService, _voucherService, _userService, _tourReservationService);
            DataContext = GuideViewModel;
        }

        private void CreateTourButton_Click(object sender, RoutedEventArgs e)
        {
            GuideViewModel.Create(_tourService, _locationService, _keyPointService, LoggedInGuide);
            //CreateTourView createTourView = new(_tourService, _locationService, _keyPointService, LoggedInGuide);
            //FrameManagerGuide.Instance.MainFrame.Navigate(createTourView);
        }

        private void StartTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuideViewModel.IsTourSelected() && GuideViewModel.IsTourCreated())
            {
                startTourButton.IsEnabled = false;
                LiveTourTrackingView liveTourTrackingView = new(GuideViewModel.SelectedTour, _keyPointService, _tourReservationService, _userService, _tourService);
                liveTourTrackingView.Closed += LiveTourTrackingView_Closed;
                liveTourTrackingView.Show();
                Update();
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
        }

        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuideViewModel.IsTourSelected() && GuideViewModel.IsTourEligibleForCancellation())
            {
                string additionalComment = Microsoft.VisualBasic.Interaction.InputBox("Unesite razlog:", "Input String");
                if (!string.IsNullOrEmpty(additionalComment))
                {
                    GuideViewModel.CancelTour(additionalComment);
                    SuccessfulCancellationLabelEvent();
                }
            }
            else
            {
                MessageBox.Show("Odaberite turu koju zelite da otkazete");
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

        private void HandleTourRequestsView_Closed(object sender, EventArgs e)
        {
            Update();
        }

        public void Update()
        {
            GuideViewModel.Update();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FrameManagerGuide.Instance.MainFrame = MainFrameGuide;
        }

        private void HomePageButton_Click(object sender, RoutedEventArgs e) 
        {
            
        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RequestsButton_Click(object sender, RoutedEventArgs e)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            GuideAccountView guideAccountView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }
    }
}
