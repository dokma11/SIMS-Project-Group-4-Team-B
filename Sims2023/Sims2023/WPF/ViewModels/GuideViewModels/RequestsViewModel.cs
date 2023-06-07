using LiveCharts;
using LiveCharts.Wpf;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class RequestsViewModel : INotifyPropertyChanged
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
        public List<RequestsLanguage> LanguagesToDisplay { get; set; }
        public List<string> LocationsToDisplay { get; set; }
        public List<string> LanguageYearsToDisplay { get; set; }
        public List<string> LocationYearsToDisplay { get; set; }
        public TourRequest SelectedRequest { get; set; }
        public string LocationTextBox { get; set; }
        public string LanguageTextBox { get; set; }
        public string GuestNumberTextBox { get; set; }

        private string _dateStartTextBox = DateTime.Today.AddDays(1).ToString();
        public string DateStartTextBox
        {
            get { return _dateStartTextBox; }
            set
            {
                if (_dateStartTextBox != value)
                {
                    _dateStartTextBox = value;
                    OnPropertyChanged(nameof(DateStartTextBox));

                    UpdateDatePickerBlackoutDates();
                }
            }
        }
        private string _dateEndTextBox = DateTime.Today.AddDays(2).ToString();
        public string DateEndTextBox
        {
            get { return _dateEndTextBox; }
            set
            {
                if (_dateEndTextBox != value)
                {
                    _dateEndTextBox = value;
                    OnPropertyChanged(nameof(DateEndTextBox));

                    UpdateDatePickerBlackoutDates();
                }
            }
        }
        public DateTime DateStartDisplayDateStart { get; set; }
        public DateTime DateStartDisplayDateEnd { get; set; }
        public DateTime DateEndDisplayDateStart { get; set; }
        public RelayCommand LocationConfirmCommand { get; set; }
        public RelayCommand LanguageConfirmCommand { get; set; }
        public RelayCommand AcceptRequestCommand { get; set; }
        public RelayCommand DeclineRequestCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand HomePageNavigationCommand { get; set; }
        public RelayCommand ToursPageNavigationCommand { get; set; }
        public RelayCommand AccountPageNavigationCommand { get; set; }
        public RelayCommand ReviewsPageNavigationCommand { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _selectedLanguagesComboBoxItem;

        public string SelectedLanguagesComboBoxItem
        {
            get { return _selectedLanguagesComboBoxItem; }
            set
            {
                _selectedLanguagesComboBoxItem = value;
                OnPropertyChanged(nameof(SelectedLanguagesComboBoxItem));
                if (SelectedLanguagesComboBoxItem != null && SelectedLanguageYearsComboBoxItem != null)
                {
                    DisplayLanguageStatistics(SelectedLanguagesComboBoxItem.ToString(), SelectedLanguageYearsComboBoxItem.ToString());
                }
            }
        }

        private string _selectedLanguageYearsComboBoxItem;

        public string SelectedLanguageYearsComboBoxItem
        {
            get { return _selectedLanguageYearsComboBoxItem; }
            set
            {
                _selectedLanguageYearsComboBoxItem = value;
                OnPropertyChanged(nameof(SelectedLanguageYearsComboBoxItem));
                if (SelectedLanguagesComboBoxItem != null && SelectedLanguageYearsComboBoxItem != null)
                {
                    DisplayLanguageStatistics(SelectedLanguagesComboBoxItem.ToString(), SelectedLanguageYearsComboBoxItem.ToString());
                }
            }
        }

        private string _selectedLocationsComboBoxItem;

        public string SelectedLocationsComboBoxItem
        {
            get { return _selectedLocationsComboBoxItem; }
            set
            {
                _selectedLocationsComboBoxItem = value;
                OnPropertyChanged(nameof(SelectedLocationsComboBoxItem));
                if (SelectedLocationsComboBoxItem != null && SelectedLocationYearsComboBoxItem != null)
                {
                    DisplayLocationStatistics(SelectedLocationsComboBoxItem.ToString(), SelectedLocationYearsComboBoxItem.ToString());
                }
            }
        }

        private string _selectedLocationYearsComboBoxItem;

        public string SelectedLocationYearsComboBoxItem
        {
            get { return _selectedLocationYearsComboBoxItem; }
            set
            {
                _selectedLocationYearsComboBoxItem = value;
                OnPropertyChanged(nameof(SelectedLocationYearsComboBoxItem));
                if (SelectedLocationsComboBoxItem != null && SelectedLocationYearsComboBoxItem != null)
                {
                    DisplayLocationStatistics(SelectedLocationsComboBoxItem.ToString(), SelectedLocationYearsComboBoxItem.ToString());
                }
            }
        }
        private string _toolTipContent { get; set; }
        public string ToolTipContent
        {
            get { return _toolTipContent; }
            set
            {
                _toolTipContent = value;
                OnPropertyChanged(nameof(ToolTipContent));
            }
        }
        private int _selectedTabIndex;

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;
                OnPropertyChanged(nameof(SelectedTabIndex));
                UpdateToolTipContent();
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _tab1ToolTip = "Sa Vaše leve strane prikazani su svi zahtevi za kreiranje ture.\n\n"
                + "U tabeli sa leve strane možete prvo videti šest informacija:\n"
                + "• Korisničko ime korisnika koji je kreirao zahtev\n"
                + "• Lokaciju na kojoj korisnik želi da se tura održi\n"
                + "• Broj gostiju koji bi bio prisutan na turi\n"
                + "• Početak i kraj opsega za datum održavanja date ture. \n"
                + "Treba da odaberete jedan datum kad ste slobodni u datom opsegu\n"
                + "• Da biste videli opis ture koji je korisnik zadao, morate da pritisnete na taj zahtev\n"
                + "Jednim levim klikom red će se proširiti i Vi ćete moći da pročitate opis datog zahteva za turu.\n"
                + "• U koloni prihvati postoje dva dugmeta, jedno je za prihvatanje drugo za odbijanje zahteva:\n"
                + "Ako kliknete da dugme ,,Prihvati\" preći ćete na prozor za kreiranje ture preko prihvaćenog zahteva\n"
                + "Ako kliknete na dugme ,,Odbij\" odabrani zahtev će biti odbijen i samim time uklonjne iz liste.\n\n"
                + "Sa desne strane, ispod naslova ,,Filtriraj prema\" videćete više polja pomoću kojih možete da filtrirate zahteve: \n"
                + "• Kada filtrirate prema lokaciji, biće Vam ispisani samo zahtevi koji se nalaze na unetoj lokaciji \n"
                + "• Kada filtrirate prema broju gostiju, biće Vam ispisani samo zahtevi koji su predviđeni za uneti broj gostiju \n"
                + "• Kada filtrirate prema jeziku, biće Vam ispisani samo zahtevi koji su predviđeni da se održe na unetom jeziku \n"
                + "• Kada filtrirate prema datumima, unosite početni i krajnji opseg u kojem želite da vidite zahteve.\n"
                + "Biće prikazani oni zahtevi koji su zadovoljili uneti opseg \n"
                + "• Na samom kraju postoji dugme ,,Filtriraj\" na koje kad kliknete primenićete željene promene.\n";

        private string _tab2ToolTip = "Sa Vaše leve strane prikazana je statistika o zahtevima za ture na lokaciji.\n\n"
                + "Postoje dva padajuća menija:\n"
                + "• Levi Vam služi da odaberete lokaciju za koju želite da vidite statistiku.\n"
                + "• Desni Vam služi da odaberete godinu u kojoj želite da vidite statistiku za odabranu lokaciju.\n\n"
                + "Sa Vaše desne strane prikazana je najtraženija lokacija u proteklih godinu dana.\n"
                + "Ispod nje prikazana je statistika date lokacije, takođe postoji dugme sa natpisom ,,Da\".\n"
                + "Pritiskom na to dugme, preći ćete na prozor za kreiranje ture na najtraženijoj lokaciji.";

        private string _tab3ToolTip = "Sa Vaše leve strane prikazana je statistika o zahtevima za ture na jeziku.\n\n"
                + "Postoje dva padajuća menija:\n"
                + "• Levi Vam služi da odaberete jezik za koji želite da vidite statistiku.\n"
                + "• Desni Vam služi da odaberete godinu u kojoj želite da vidite statistiku za odabrani jezik.\n\n"
                + "Sa Vaše desne strane prikazana je najtraženiji jezik u proteklih godinu dana.\n"
                + "Ispod nje prikazana je statistika datog jezika, takođe postoji dugme sa natpisom ,,Da\".\n"
                + "Pritiskom na to dugme, preći ćete na prozor za kreiranje ture na najtraženijem jeziku.";

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
            LanguagesToDisplay = _requestService.GetComboBoxData("languages").Select(Enum.Parse<RequestsLanguage>).ToList();

            LocationsToDisplay = new();
            LocationsToDisplay = _requestService.GetComboBoxData("locations");

            LanguageYearsToDisplay = new();
            LanguageYearsToDisplay = _requestService.GetComboBoxData("years");
            LocationYearsToDisplay = new();
            LocationYearsToDisplay = _requestService.GetComboBoxData("years");

            ToolTipContent = _tab1ToolTip;

            DateStartDisplayDateStart = DateTime.Today.AddDays(1);
            DateEndDisplayDateStart = DateTime.Today.AddDays(2);
        }

        private void UpdateDatePickerBlackoutDates()
        {
            DateEndDisplayDateStart = DateStartDisplayDateStart.AddDays(1);
        }

        private void UpdateToolTipContent()
        {
            if (SelectedTabIndex == 0)
            {
                ToolTipContent = _tab1ToolTip;
            }
            else if (SelectedTabIndex == 1)
            {
                ToolTipContent = _tab2ToolTip;
            }
            else
            {
                ToolTipContent = _tab3ToolTip;
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
            foreach (var filteredRequest in FilterRequests(locationSearchTerm, guestNumberSearchTerm, languageSearchTerm, dateStartSearchTerm, dateEndSearchTerm))
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
