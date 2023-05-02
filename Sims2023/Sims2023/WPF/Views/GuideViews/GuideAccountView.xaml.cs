using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for GuideAccountView.xaml
    /// </summary>
    public partial class GuideAccountView : Page
    {
        private RequestService _requestService;
        private TourService _tourService;
        private LocationService _locationService;
        private TourReviewService _tourReviewService;
        private KeyPointService _keyPointService;
        private TourReservationService _tourReservationService;
        private VoucherService _voucherService;
        private UserService _userService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        public User LoggedInGuide { get; set; }
        public GuideAccountView(TourService tourService, TourReviewService tourReviewService, LocationService locationService, RequestService requestService, KeyPointService keyPointService, User loggedInGuide, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService)
        {
            InitializeComponent();

            _tourService = tourService;
            _tourReviewService = tourReviewService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _userService = userService;
            _voucherService = voucherService;
            _countriesAndCitiesService = countriesAndCitiesService;
            _tourReservationService = tourReservationService;
            _requestService = requestService;

            LoggedInGuide = loggedInGuide;

            nameLabel.Content = loggedInGuide.Name;
            surnameLabel.Content = loggedInGuide.Surname;
            ageLabel.Content = loggedInGuide.Age;
            phoneNumberLabel.Content = loggedInGuide.PhoneNumber;
            emailLabel.Content = loggedInGuide.Email;
        }

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            GuideHomePageView guideHomePageView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideHomePageView);
        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService);
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

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
        }
    }
}
