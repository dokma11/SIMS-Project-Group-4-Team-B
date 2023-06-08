using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.GuideViews;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class GuestReviewsViewModel
    {
        public Tour SelectedTour { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }
        public TourReview SelectedReview { get; set; }
        public ObservableCollection<TourReview> ReviewsToDisplay { get; set; }
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private TourReviewService _tourReviewService;
        private RequestService _requestService;
        private TourReservationService _tourReservationService;
        private VoucherService _voucherService;
        private UserService _userService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        private TourNotificationService _tourNotificationService;
        private ComplexTourRequestService _complexTourRequestService;
        private SubTourRequestService _subTourRequestService;

        public User LoggedInGuide;
        public string LabelContent { get; set; }
        public bool IsLabelVisible { get; set; }
        public RelayCommand DisplayReviewsCommand { get; set; }
        public RelayCommand ReportReviewCommand { get; set; }
        public RelayCommand HomePageNavigationCommand { get; set; }
        public RelayCommand ToursPageNavigationCommand { get; set; }
        public RelayCommand RequestsPageNavigationCommand { get; set; }
        public RelayCommand AccountPageNavigationCommand { get; set; }
        public GuestReviewsViewModel(TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, RequestService requestService, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, User loggedInGuide, TourNotificationService tourNotificationService, ComplexTourRequestService complexTourRequestService, SubTourRequestService subTourRequestService)
        {
            DisplayReviewsCommand = new RelayCommand(Executed_DisplayReviewsCommand, CanExecute_DisplayReviewsCommand);
            ReportReviewCommand = new RelayCommand(Executed_ReportReviewCommand, CanExecute_ReportReviewCommand);
            HomePageNavigationCommand = new RelayCommand(Executed_HomePageNavigationCommand, CanExecute_HomePageNavigationCommand);
            ToursPageNavigationCommand = new RelayCommand(Executed_ToursPageNavigationCommand, CanExecute_ToursPageNavigationCommand);
            RequestsPageNavigationCommand = new RelayCommand(Executed_RequestsPageNavigationCommand, CanExecute_RequestsPageNavigationCommand);
            AccountPageNavigationCommand = new RelayCommand(Executed_AccountPageNavigationCommand, CanExecute_AccountPageNavigationCommand);

            LoggedInGuide = loggedInGuide;

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
            _complexTourRequestService = complexTourRequestService;
            _subTourRequestService = subTourRequestService;

            ToursToDisplay = new ObservableCollection<Tour>(_tourService.GetFinished(loggedInGuide));
            ReviewsToDisplay = new ObservableCollection<TourReview>();

            GetAttendedGuestsNumber();

            IsLabelVisible = false;
        }

        public void GetAttendedGuestsNumber()
        {
            _tourReservationService.GetAttendedGuestsNumber(LoggedInGuide);
        }

        public void Executed_DisplayReviewsCommand(object obj)
        {
            UpdateReviewsList();
        }

        public bool CanExecute_DisplayReviewsCommand(object obj)
        {
            return SelectedTour != null;
        }

        public void Executed_ReportReviewCommand(object obj)
        {
            _tourReviewService.Report(SelectedReview);
            UpdateReviewsList(); ;
        }

        public bool CanExecute_ReportReviewCommand(object obj)
        {
            if (SelectedReview != null && SelectedReview.IsValid)
            {
                SuccessfulReportLabelEvent();
                return true;
            }
            else
            {
                AlreadyReportedLabelEvent();
                return false;
            }
        }

        public void UpdateReviewsList()
        {
            ReviewsToDisplay.Clear();
            foreach (var tourReview in _tourReviewService.GetByToursId(SelectedTour.Id))
            {
                ReviewsToDisplay.Add(tourReview);
            }
            _tourReviewService.Save();
        }

        private void SuccessfulReportLabelEvent()
        {
            LabelContent = "Uspešno ste prijavili recenziju!";
            IsLabelVisible = true;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void AlreadyReportedLabelEvent()
        {
            LabelContent = "Data recenzija je već prijavljena!";
            IsLabelVisible = true;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            IsLabelVisible = false;
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
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
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }
        private bool CanExecute_ToursPageNavigationCommand(object obj)
        {
            return true;
        }

        private void Executed_RequestsPageNavigationCommand(object obj)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }
        private bool CanExecute_RequestsPageNavigationCommand(object obj)
        {
            return true;
        }

        private void Executed_AccountPageNavigationCommand(object obj)
        {
            GuideAccountView guideAccountView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }
        private bool CanExecute_AccountPageNavigationCommand(object obj)
        {
            return true;
        }
    }
}
