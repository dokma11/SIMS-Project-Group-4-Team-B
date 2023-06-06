using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class CreateTourFromFrequentLanguageViewModel
    {
        public Tour NewTour { get; set; }
        public Location NewLocation { get; set; }
        public KeyPoint NewKeyPoint { get; set; }
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
        public RequestsLanguage SelectedLanguage { get; set; }
        private List<DateTime> _dateTimeList;
        private List<string> _keyPointsList;

        public User LoggedInGuide { get; set; }
        public string CountryComboBoxText { get; set; }
        public string CityComboBoxText { get; set; }
        public string KeyPointTextBoxText { get; set; }
        public ObservableCollection<string> ListBoxItems { get; set; }
        public bool IsButtonEnabled { get; set; }


        public RelayCommand CreateCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand HomePageNavigationCommand { get; set; }
        public RelayCommand ToursPageNavigationCommand { get; set; }
        public RelayCommand ReviewsPageNavigationCommand { get; set; }
        public RelayCommand AccountPageNavigationCommand { get; set; }
        public CreateTourFromFrequentLanguageViewModel(RequestsLanguage selectedLanguage, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, RequestService requestService, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, User loggedInGuide, TourNotificationService tourNotificationService)
        {
            CreateCommand = new RelayCommand(Executed_CreateCommand, CanExecute_CreateCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand, CanExecute_CancelCommand);
            AddItemCommand = new RelayCommand(Executed_AddItemCommand, CanExecute_AddItemCommand);
            HomePageNavigationCommand = new RelayCommand(Executed_HomePageNavigationCommand, CanExecute_HomePageNavigationCommand);
            ToursPageNavigationCommand = new RelayCommand(Executed_ToursPageNavigationCommand, CanExecute_ToursPageNavigationCommand);
            ReviewsPageNavigationCommand = new RelayCommand(Executed_ReviewsPageNavigationCommand, CanExecute_ReviewsPageNavigationCommand);
            AccountPageNavigationCommand = new RelayCommand(Executed_AccountPageNavigationCommand, CanExecute_AccountPageNavigationCommand);

            SelectedLanguage = selectedLanguage;

            NewTour = new();
            NewLocation = new();
            NewKeyPoint = new();

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
            _countriesAndCitiesService = new CountriesAndCitiesService();

            _dateTimeList = new List<DateTime>();
            _keyPointsList = new List<string>();
            IsButtonEnabled = false;

            LoggedInGuide = loggedInGuide;
        }


        public void Executed_CreateCommand(object obj)
        {
            NewLocation.City = CityComboBoxText;
            NewLocation.Country = CountryComboBoxText;
            NewTour.Location = NewLocation;
            NewTour.GuideLanguage = (ToursLanguage)SelectedLanguage;
            _locationService.Create(NewLocation);
            _tourService.Create(NewTour, _dateTimeList, NewLocation, LoggedInGuide);
            int firstToursId = NewTour.Id - _dateTimeList.Count + 1;
            _tourService.AddToursLocation(NewTour, NewLocation, _dateTimeList.Count);
            _keyPointService.Create(NewKeyPoint, _keyPointsList, firstToursId, _dateTimeList.Count);
            _tourService.AddToursKeyPoints(_keyPointsList, firstToursId);
            NotifyGuests();

            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        public bool CanExecute_CreateCommand(object obj)
        {
            return true;
        }

        public void Executed_CancelCommand(object obj)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }

        private void Executed_AddItemCommand(object obj)
        {
            ListBoxItems.Add(KeyPointTextBoxText);
            KeyPointTextBoxText = string.Empty;
        }
        private bool CanExecute_AddItemCommand(object obj)
        {
            return !string.IsNullOrWhiteSpace(KeyPointTextBoxText) && KeyPointTextBoxText != "Unesite ključne tačke";
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

        private void Executed_ReviewsPageNavigationCommand(object obj)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }
        private bool CanExecute_ReviewsPageNavigationCommand(object obj)
        {
            return true;
        }

        private void Executed_AccountPageNavigationCommand(object obj)
        {
            GuideAccountView guideAccountView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }
        private bool CanExecute_AccountPageNavigationCommand(object obj)
        {
            return true;
        }




        public List<CountriesAndCities> GetCitiesAndCountries()
        {
            return _countriesAndCitiesService.GetAllLocations();
        }

        public void AddKeyPointsToList(string inputText)
        {
            if (!_keyPointsList.Contains(inputText))
            {
                _keyPointsList.Add(inputText);
            }
        }

        public void AddDatesToList(string inputText)
        {
            if (DateTime.TryParse(inputText, out DateTime dateTime))
            {
                if (!_dateTimeList.Contains(dateTime))
                {
                    _dateTimeList.Add(dateTime);
                }
            }
        }

        public void NotifyGuests()
        {
            foreach (var req in _requestService.GetByLanguage(SelectedLanguage.ToString()))
            {
                TourNotification tourNotification = new(NewTour, req.Guest, NotificationType.MatchedTourRequestsLanguage);
                _tourNotificationService.Create(tourNotification);
            }
        }
    }
}
