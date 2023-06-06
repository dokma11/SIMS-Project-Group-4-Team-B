using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.GuideViews;
using System;
using static Sims2023.Domain.Models.Voucher;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class GuideAccountViewModel
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
        private TourNotificationService _tourNotificationService;
        public User LoggedInGuide { get; set; }
        public string NameLabelContent { get; set; }
        public string SurnameLabelContent { get; set; }
        public string AgeLabelContent { get; set; }
        public string PhoneNumberLabelContent { get; set; }
        public string EmailLabelContent { get; set; }
        public RelayCommand DismissalCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }
        public RelayCommand HomePageNavigationCommand { get; set; }
        public RelayCommand ToursPageNavigationCommand { get; set; }
        public RelayCommand RequestsPageNavigationCommand { get; set; }
        public RelayCommand ReviewsPageNavigationCommand { get; set; }

        public GuideAccountViewModel(TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, RequestService requestService, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, User loggedInGuide, TourNotificationService tourNotificationService)
        {
            DismissalCommand = new RelayCommand(Executed_DismissalCommand, CanExecute_DismissalCommand);
            LogOutCommand = new RelayCommand(Executed_LogOutCommand, CanExecute_LogOutCommand);
            HomePageNavigationCommand = new RelayCommand(Executed_HomePageNavigationCommand, CanExecute_HomePageNavigationCommand);
            ToursPageNavigationCommand = new RelayCommand(Executed_ToursPageNavigationCommand, CanExecute_ToursPageNavigationCommand);
            RequestsPageNavigationCommand = new RelayCommand(Executed_RequestsPageNavigationCommand, CanExecute_RequestsPageNavigationCommand);
            ReviewsPageNavigationCommand = new RelayCommand(Executed_ReviewsPageNavigationCommand, CanExecute_ReviewsPageNavigationCommand);

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _tourReviewService = tourReviewService;
            _requestService = requestService;
            _tourReservationService = tourReservationService;
            _voucherService = voucherService;
            _userService = userService;
            _countriesAndCitiesService = countriesAndCitiesService;
            _tourNotificationService = tourNotificationService;

            LoggedInGuide = loggedInGuide;

            NameLabelContent = LoggedInGuide.Name;
            SurnameLabelContent = LoggedInGuide.Surname;
            AgeLabelContent = LoggedInGuide.Age.ToString();
            PhoneNumberLabelContent = LoggedInGuide.PhoneNumber;
            EmailLabelContent = LoggedInGuide.Email;
        }

        public void Executed_DismissalCommand(object obj)
        {
            foreach (var tour in _tourService.GetGuidesCreated(LoggedInGuide))
            {
                foreach (var res in _tourReservationService.GetByToursid(tour.Id))
                {
                    Voucher newVoucher = new(0, VoucherType.CancelingTour, res.User, tour, DateTime.Now, DateTime.Now.AddYears(2), "The guide quit his job", false);
                    _voucherService.Create(newVoucher);
                }
            }
            _tourService.CancelAll(LoggedInGuide);
        }

        public bool CanExecute_DismissalCommand(object obj)
        {
            return true;
        }

        public void Executed_LogOutCommand(object obj)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
        }

        public bool CanExecute_LogOutCommand(object obj)
        {
            return true;
        }

        //TOOLBAR

        private void Executed_HomePageNavigationCommand(object obj)
        {
            GuideHomePageView guideHomePageView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideHomePageView);
        }

        private bool CanExecute_HomePageNavigationCommand(object obj)
        {
            return true;
        }

        private void Executed_ToursPageNavigationCommand(object obj)
        {
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }

        private bool CanExecute_ToursPageNavigationCommand(object obj)
        {
            return true;
        }

        private void Executed_RequestsPageNavigationCommand(object obj)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        private bool CanExecute_RequestsPageNavigationCommand(object obj)
        {
            return true;
        }

        private void Executed_ReviewsPageNavigationCommand(object obj)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        private bool CanExecute_ReviewsPageNavigationCommand(object obj)
        {
            return true;
        }
    }
}
