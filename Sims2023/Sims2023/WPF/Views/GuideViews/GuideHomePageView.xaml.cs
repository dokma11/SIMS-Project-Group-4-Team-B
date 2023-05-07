using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class GuideHomePageView : Page, IObserver
    {
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private UserService _userService;
        private TourReservationService _tourReservationService;
        private TourReviewService _tourReviewService;
        private VoucherService _voucherService;
        private RequestService _requestService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        public User LoggedInGuide { get; set; }
        public GuideViewModel GuideViewModel;
        public GuideHomePageView(User user)
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

            _countriesAndCitiesService = new CountriesAndCitiesService();

            GuideViewModel = new(LoggedInGuide, _tourService, _voucherService, _userService, _tourReservationService);
            DataContext = GuideViewModel;

            DisplayLabels();
        }

        private void DisplayLabels()
        {
            if(_tourService.GetTodaysNumber(LoggedInGuide) > 0)
            {
                scheduledToursLabel.Content = "- Danas imate " + _tourService.GetTodaysNumber(LoggedInGuide) + " zakazane ture!";
            }
            else
            {
                scheduledToursLabel.Content = "- Nemate zakazanih tura za danas!";
            }

            if(_requestService.GetOnHold().Count > 0) 
            {
                tourRequestsLabel.Content = "- Imate " + _requestService.GetOnHold().Count + " nova zahteva koja niste pregledali!";
            }
            else
            {
                tourRequestsLabel.Content = "- Pregledali ste sve zahteve!";
            }

            tourReviewsLabel.Content = "- Proverite recenzije gostiju";
        }

        private void GoToToursButton_Click(object sender, RoutedEventArgs e)
        {
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }
        
        private void GoToRequestsButton_Click(object sender, RoutedEventArgs e)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }
        
        private void GoToReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        //Toolbar options

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }

        private void RequestsButton_Click(object sender, RoutedEventArgs e)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            GuideAccountView guideAccountView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }

        public void Update()
        {
            GuideViewModel.Update();
        }
    }
}
