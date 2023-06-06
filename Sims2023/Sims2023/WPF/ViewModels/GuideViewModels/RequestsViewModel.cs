using LiveCharts;
using LiveCharts.Wpf;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class RequestsViewModel
    {
        public ObservableCollection<string> Labels { get; set; }
        public ObservableCollection<string> LabelsForTheMostRequested { get; set; }
        public string[] LabelsMonth { get; set; }
        public Func<int, string> Values { get; set; }
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
        public User LoggedInGuide { get; set; }
        public SeriesCollection LanguageSeriesCollection { get; set; }
        public SeriesCollection TheMostRequestedLanguageSeriesCollection { get; set; }
        public SeriesCollection LocationSeriesCollection { get; set; }
        public SeriesCollection TheMostRequestedLocationSeriesCollection { get; set; }
        public RequestsLanguage TheMostRequestedLanguage { get; set; }
        public Location TheMostRequestedLocation { get; set; }
        public string TheMostRequestedLocationString { get; set; }
        public ObservableCollection<TourRequest> RequestsToDisplay { get; set; }
        public ObservableCollection<RequestsLanguage> LanguagesToDisplay { get; set; }
        public ObservableCollection<string> LocationsToDisplay { get; set; }
        public ObservableCollection<string> LanguageYearsToDisplay { get; set; }
        public ObservableCollection<string> LocationYearsToDisplay { get; set; }
        public TourRequest SelectedRequest { get; set; }
        public string LocationTextBox { get; set; }
        public string LanguageTextBox { get; set; }
        public string GuestNumberTextBox { get; set; }
        public string DateStartTextBox { get; set; }
        public string DateEndTextBox { get; set; }
        public RelayCommand LocationConfirmCommand { get; set; }
        public RelayCommand LanguageConfirmCommand { get; set; }
        public RelayCommand AcceptRequestCommand { get; set; }
        public RelayCommand DeclineRequestCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand HomePageNavigationCommand { get; set; }
        public RelayCommand ToursPageNavigationCommand { get; set; }
        public RelayCommand AccountPageNavigationCommand { get; set; }
        public RelayCommand ReviewsPageNavigationCommand { get; set; }

        public RequestsViewModel(RequestService requestService, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, User loggedInGuide, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, TourNotificationService tourNotificationService)
        {
            LocationConfirmCommand = new RelayCommand(Executed_LocationConfirmCommand, CanExecute_LocationConfirmCommand);
            LanguageConfirmCommand = new RelayCommand(Executed_LanguageConfirmCommand, CanExecute_LanguageConfirmCommand);
            AcceptRequestCommand = new RelayCommand(Executed_AcceptRequestCommand, CanExecute_AcceptRequestCommand);
            DeclineRequestCommand = new RelayCommand(Executed_DeclineRequestCommand, CanExecute_DeclineRequestCommand);
            FilterCommand = new RelayCommand(Executed_FilterCommand, CanExecute_FilterCommand);
            HomePageNavigationCommand = new RelayCommand(Executed_HomePageNavigationCommand, CanExecute_HomePageNavigationCommand);
            ToursPageNavigationCommand = new RelayCommand(Executed_ToursPageNavigationCommand, CanExecute_ToursPageNavigationCommand);
            AccountPageNavigationCommand = new RelayCommand(Executed_AccountPageNavigationCommand, CanExecute_AccountPageNavigationCommand);
            ReviewsPageNavigationCommand = new RelayCommand(Executed_ReviewsPageNavigationCommand, CanExecute_ReviewsPageNavigationCommand);

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _tourReviewService = tourReviewService;
            _requestService = requestService;
            _tourReservationService = tourReservationService;
            _countriesAndCitiesService = countriesAndCitiesService;
            _voucherService = voucherService;
            _userService = userService;
            _requestService = requestService;
            _tourNotificationService = tourNotificationService;

            LoggedInGuide = loggedInGuide;

            TheMostRequestedLanguage = new();
            TheMostRequestedLanguage = GetTheMostRequestedLanguage();

            TheMostRequestedLocation = new();
            TheMostRequestedLocation = GetTheMostRequestedLocation();
            TheMostRequestedLocationString = TheMostRequestedLocation.City + ", " + TheMostRequestedLocation.Country;

            LanguageSeriesCollection = new SeriesCollection();
            TheMostRequestedLanguageSeriesCollection = new SeriesCollection();
            LocationSeriesCollection = new SeriesCollection();
            TheMostRequestedLocationSeriesCollection = new SeriesCollection();

            LabelsMonth = new[] { "Januar", "Februar", "Mart", "April", "Maj", "Jun", "Jul", "Avgust", "Septembar", "Oktobar", "Novembar", "Decembar" };

            Labels = new ObservableCollection<string>();
            foreach (var l in LabelsMonth)
            {
                Labels.Add(l);
            }

            LabelsForTheMostRequested = new ObservableCollection<string>();
            foreach (var l in LabelsMonth)
            {
                LabelsForTheMostRequested.Add(l);
            }

            DisplayTheMostRequestedLanguage();
            DisplayTheMostRequestedLocation();

            RequestsToDisplay = new ObservableCollection<TourRequest>(_requestService.GetOnHold());

            LanguagesToDisplay = new();
            GetLanguages();

            LocationsToDisplay = new();
            GetLocations();

            LanguageYearsToDisplay = new();
            LocationYearsToDisplay = new();
            GetYears();
        }

        public void GetLanguages()
        {
            LanguagesToDisplay.Clear();
            foreach(var language in _requestService.GetComboBoxData("languages").Select(Enum.Parse<RequestsLanguage>).ToList())
            {
                LanguagesToDisplay.Add(language);
            }
        }

        public void GetLocations()
        {
            LocationsToDisplay.Clear();
            foreach (var location in _requestService.GetComboBoxData("locations"))
            {
                LocationsToDisplay.Add(location);
            }
        }

        public void GetYears()
        {
            LocationYearsToDisplay.Clear();
            LanguageYearsToDisplay.Clear();
            foreach (var year in _requestService.GetComboBoxData("years"))
            {
                LocationYearsToDisplay.Add(year);
                LanguageYearsToDisplay.Add(year);
            }
        }

        public void DisplayLanguageStatistics(string language, string year)
        {
            if (year == "Svih vremena")
            {
                DisplayYearlyLanguageStatistics(language, year);
            }
            else
            {
                DisplayMonthlyLanguageStatistics(language, year);
            }
        }

        public void DisplayYearlyLanguageStatistics(string language, string year)
        {
            LanguageSeriesCollection.Clear();
            var yearlyStats = new ChartValues<int>();
            foreach (var y in _requestService.GetComboBoxData("years"))
            {
                yearlyStats.Add(_requestService.GetYearlyLanguageStatistics(language, y));
            }
            LanguageSeriesCollection.Add(new ColumnSeries { Values = yearlyStats, Title = "Broj zahteva po godinama" });
            Labels.Clear();
            foreach (var l in _requestService.GetComboBoxData("years"))
            {
                Labels.Add(l);
            }
            Values = value => value.ToString("D");
        }

        public void DisplayMonthlyLanguageStatistics(string language, string year)
        {
            LanguageSeriesCollection.Clear();
            var monthlyStats = new ChartValues<int>();
            for (int month = 1; month <= 12; month++)
            {
                monthlyStats.Add(_requestService.GetMonthlyLanguageStatistics(language, month, year));
            }
            LanguageSeriesCollection.Add(new ColumnSeries { Values = monthlyStats, Title = "Broj zahteva u " + year + ":" });
            Labels.Clear();
            foreach (var l in LabelsMonth)
            {
                Labels.Add(l);
            }
            Values = value => value.ToString("D");
        }

        public void DisplayLocationStatistics(string location, string year)
        {
            if (year == "Svih vremena")
            {
                DisplayYearlyLocationStatistics(location);
            }
            else
            {
                DisplayMonthlyLocationStatistics(location, year);
            }
        }

        public void DisplayMonthlyLocationStatistics(string location, string year)
        {
            LocationSeriesCollection.Clear();
            var monthlyStats = new ChartValues<int>();
            for (int month = 1; month <= 12; month++)
            {
                monthlyStats.Add(_requestService.GetMonthlyLocationStatistics(location, month, year));
            }
            LocationSeriesCollection.Add(new ColumnSeries { Values = monthlyStats, Title = "Broj zahteva u " + year + ":" });
            Labels.Clear();
            foreach (var l in LabelsMonth)
            {
                Labels.Add(l);
            }
            Values = value => value.ToString("D");
        }

        public void DisplayYearlyLocationStatistics(string location)
        {
            LocationSeriesCollection.Clear();
            var yearlyStats = new ChartValues<int>();
            foreach (var y in _requestService.GetComboBoxData("years"))
            {
                yearlyStats.Add(_requestService.GetYearlyLocationStatistics(location, y));
            }
            LocationSeriesCollection.Add(new ColumnSeries { Values = yearlyStats, Title = "Broj zahteva po godinama" });
            Labels.Clear();
            foreach (var l in _requestService.GetComboBoxData("years"))
            {
                Labels.Add(l);
            }
            Values = value => value.ToString("D");
        }

        public RequestsLanguage GetTheMostRequestedLanguage()
        {
            return _requestService.GetTheMostRequestedLanguage();
        }

        public void DisplayTheMostRequestedLanguage()
        {
            var monthlyStats = new ChartValues<int>();
            for (int month = 1; month <= 12; month++)
            {
                monthlyStats.Add(_requestService.GetMonthlyLanguageStatistics(TheMostRequestedLanguage.ToString(), month, "2023"));
            }
            TheMostRequestedLanguageSeriesCollection.Add(new ColumnSeries { Values = monthlyStats, Title = "Broj zahteva u " + "2023" + ":" });
            Values = value => value.ToString("D");
        }

        public Location GetTheMostRequestedLocation()
        {
            return _requestService.GetTheMostRequestedLocation();
        }

        public void DisplayTheMostRequestedLocation()
        {
            var monthlyStats = new ChartValues<int>();
            for (int month = 1; month <= 12; month++)
            {
                monthlyStats.Add(_requestService.GetMonthlyLocationStatistics(TheMostRequestedLocationString, month, "2023"));
            }
            TheMostRequestedLocationSeriesCollection.Add(new ColumnSeries { Values = monthlyStats, Title = "Broj zahteva u " + "2023" + ":" });
            Values = value => value.ToString("D");
        }

        public List<TourRequest> FilterRequests(string locationSearchTerm, string guestNumberSearchTerm, string languageSearchTerm, string dateStartSearchTerm, string dateEndSearchTerm)
        {
            return _requestService.GetFiltered(locationSearchTerm, guestNumberSearchTerm, languageSearchTerm, dateStartSearchTerm, dateEndSearchTerm);
        }

        public void HandleRequest(bool accepted)
        {
            if (SelectedRequest != null)
            {
                var state = accepted ? RequestsState.Accepted : RequestsState.Invalid;
                _requestService.UpdateState(SelectedRequest, state);
                Update();
            }
        }

        public void Update()
        {
            RequestsToDisplay.Clear();
            foreach (TourRequest request in _requestService.GetOnHold())
            {
                RequestsToDisplay.Add(request);
            }
        }

        private void Executed_LanguageConfirmCommand(object obj)
        {
            CreateTourFromFrequentLanguageView createTourFromFrequentLanguageView = new(TheMostRequestedLanguage, _tourService, _locationService, _keyPointService, _tourReviewService, _requestService, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, LoggedInGuide, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(createTourFromFrequentLanguageView);
        }

        private bool CanExecute_LanguageConfirmCommand(object obj)
        {
            return true;
        }

        private void Executed_LocationConfirmCommand(object obj)
        {
            CreateTourFromFrequentLocationView createTourFromFrequentLocationView = new(TheMostRequestedLocation, _tourService, _locationService, _keyPointService, _tourReviewService, _requestService, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, LoggedInGuide, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(createTourFromFrequentLocationView);
        }

        private bool CanExecute_LocationConfirmCommand(object obj)
        {
            return true;
        }

        private void Executed_AcceptRequestCommand(object obj)
        {
            CreateTourFromRequestView createTourFromRequestView = new(SelectedRequest, _tourService, _locationService, _keyPointService, _tourReviewService, _requestService, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, LoggedInGuide, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(createTourFromRequestView);
            HandleRequest(true);
        }

        private bool CanExecute_AcceptRequestCommand(object obj)
        {
            return true;
        }

        private void Executed_DeclineRequestCommand(object obj)
        {
            HandleRequest(false);
        }

        private bool CanExecute_DeclineRequestCommand(object obj)
        {
            return true;
        }

        private void Executed_FilterCommand(object obj)
        {
            string locationSearchTerm = LocationTextBox == "Unesite lokaciju" ? "" : LocationTextBox;
            string guestNumberSearchTerm = GuestNumberTextBox == "Unesite broj gostiju" ? "" : GuestNumberTextBox;
            string languageSearchTerm = LanguageTextBox == "Unesite jezik" ? "" : LanguageTextBox;
            string dateStartSearchTerm = DateStartTextBox;
            string dateEndSearchTerm = DateEndTextBox;

            RequestsToDisplay.Clear();
            foreach(var filteredRequest in FilterRequests(locationSearchTerm, guestNumberSearchTerm, languageSearchTerm, dateStartSearchTerm, dateEndSearchTerm))
            {
                RequestsToDisplay.Add(filteredRequest);
            }
        }

        private bool CanExecute_FilterCommand(object obj)
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

        private void Executed_AccountPageNavigationCommand(object obj)
        {
            GuideAccountView guideAccountView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }

        private bool CanExecute_AccountPageNavigationCommand(object obj)
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
