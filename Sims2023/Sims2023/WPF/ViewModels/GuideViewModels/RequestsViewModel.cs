using LiveCharts;
using LiveCharts.Wpf;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;

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
        private ComplexTourRequestService _complexTourRequestService;
        private SubTourRequestService _subTourRequestService;
        public User LoggedInGuide { get; set; }
        public SeriesCollection LanguageSeriesCollection { get; set; }
        public SeriesCollection TheMostRequestedLanguageSeriesCollection { get; set; }
        public SeriesCollection LocationSeriesCollection { get; set; }
        public SeriesCollection TheMostRequestedLocationSeriesCollection { get; set; }
        public RequestsLanguage TheMostRequestedLanguage { get; set; }
        public Location TheMostRequestedLocation { get; set; }
        public string TheMostRequestedLocationString { get; set; }
        public ObservableCollection<TourRequest> RequestsToDisplay { get; set; }
        public ObservableCollection<ComplexTourRequest> ComplexTourRequestsToDisplay { get; set; }
        public ObservableCollection<SubTourRequest> SubTourRequestsToDisplay { get; set; }
        public List<RequestsLanguage> LanguagesToDisplay { get; set; }
        public List<string> LocationsToDisplay { get; set; }
        public List<string> LanguageYearsToDisplay { get; set; }
        public List<string> LocationYearsToDisplay { get; set; }
        public TourRequest SelectedRequest { get; set; }
        public ComplexTourRequest SelectedComplexTourRequest { get; set; }
        public SubTourRequest SelectedSubTourRequest { get; set; }

        private string _locationTextBox;
        public string LocationTextBox
        {
            get { return _locationTextBox; }
            set
            {
                if (_locationTextBox != value)
                {
                    _locationTextBox = value;
                    OnPropertyChanged(nameof(LocationTextBox));
                }
            }
        }

        private string _languageTextBox;
        public string LanguageTextBox
        {
            get { return _languageTextBox; }
            set
            {
                if (_languageTextBox != value)
                {
                    _languageTextBox = value;
                    OnPropertyChanged(nameof(LanguageTextBox));
                }
            }
        }

        private string _guestNumberTextBox;
        public string GuestNumberTextBox
        {
            get { return _guestNumberTextBox; }
            set
            {
                if (_guestNumberTextBox != value)
                {
                    _guestNumberTextBox = value;
                    OnPropertyChanged(nameof(GuestNumberTextBox));
                }
            }
        }

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

        private DateTime _dateStartDisplayDateStart;
        public DateTime DateStartDisplayDateStart
        {
            get { return _dateStartDisplayDateStart; }
            set
            {
                if (_dateStartDisplayDateStart != value)
                {
                    _dateStartDisplayDateStart = value;
                    OnPropertyChanged(nameof(DateStartDisplayDateStart));
                }
            }
        }

        private DateTime _dateStartDisplayDateEnd;
        public DateTime DateStartDisplayDateEnd
        {
            get { return _dateStartDisplayDateEnd; }
            set
            {
                if (_dateStartDisplayDateEnd != value)
                {
                    _dateStartDisplayDateEnd = value;
                    OnPropertyChanged(nameof(DateStartDisplayDateEnd));
                }
            }
        }

        private DateTime _dateEndDisplayDateStart;
        public DateTime DateEndDisplayDateStart
        {
            get { return _dateEndDisplayDateStart; }
            set
            {
                if (_dateEndDisplayDateStart != value)
                {
                    _dateEndDisplayDateStart = value;
                    OnPropertyChanged(nameof(DateEndDisplayDateStart));
                }
            }
        }

        public RelayCommand LocationConfirmCommand { get; set; }
        public RelayCommand LanguageConfirmCommand { get; set; }
        public RelayCommand AcceptRequestCommand { get; set; }
        public RelayCommand AcceptSubTourRequestCommand { get; set; }
        public RelayCommand DeclineRequestCommand { get; set; }
        public RelayCommand DeclineSubTourRequestCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand DisplaySubTourRequestsCommand { get; set; }
        public RelayCommand LocationStatisticsReportCommand { get; set; }
        public RelayCommand LanguageStatisticsReportCommand { get; set; }
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

        private bool _isLabelVisible;
        public bool IsLabelVisible
        {
            get { return _isLabelVisible; }
            set
            {
                if (_isLabelVisible != value)
                {
                    _isLabelVisible = value;
                    OnPropertyChanged(nameof(IsLabelVisible));
                }
            }
        }

        private bool _filteringEnabled;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _tab1ToolTip = "Dobrodošli na stranicu Zahtevi! \n"
                + "Sa Vaše leve strane prikazani su svi zahtevi za kreiranje ture.\n\n"
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

        private string _tab2ToolTip = "U tabeli sa Vaše leve strane prikazani su svi zahtevi za složene ture.\n\n"
                + "U njoj možete da vidite nazic složene ture i korisničko ime korisnika koji ju je kreirao:\n"
                + "Da biste videli sve delove te složene ture, morate levim klikom da izaberete željenu turu i da pritisnete dugme ,,Prikaži delove ture\"\n"
                + "Nakon što ste pritisnuli dugme, u tabeli sa Vaše desne strane biće izlistani svi delove prethodno izabrane ture\n\n"
                + "U njoj možete videti informacije o datom delu kao što su:\n"
                + "• Lokacija - predstavlja lokaciju na kojoj se dati deo treba održati.\n"
                + "• Broj gostiju - predstavlja broj gostiju koji bi bio prisutan na datoj turi.\n"
                + "• Početak i kraj opsega - predstavljaju opseg datuma u kojem bi željena tura trebala da se održi,\n"
                + "na Vama je da odaberete jedan datum unutar tog opsega. Naravno treba imati u vidu da tog datuma budete slobodni \n"
                + "• Opis - pritiskom na ovo polje biće Vam prikazan opis izabranog dela ture tako što će se red rasširiti \n"
                + "• Prihvati - predstavlja polje u kojem se nalaze dva dugmeta ,,Da\" i ,,Ne\". Pritiskom na dugme ,,Da\", \n"
                + "bićete odvedeni na sledeći prozor u kom ćete moći da popunite ostale informacije vezane za potencijalno novu turu, \n"
                + "dok pritiskom na dugme ,,Ne\" odbićete zahtev za dati deo ture i on će biti samim time uklonjen iz liste\n";

        private string _tab3ToolTip = "Sa Vaše leve strane prikazana je statistika o zahtevima za ture na lokaciji.\n\n"
                + "Postoje dva padajuća menija:\n"
                + "• Levi Vam služi da odaberete lokaciju za koju želite da vidite statistiku.\n"
                + "• Desni Vam služi da odaberete godinu u kojoj želite da vidite statistiku za odabranu lokaciju.\n\n"
                + "Sa Vaše desne strane prikazana je najtraženija lokacija u proteklih godinu dana.\n"
                + "Ispod nje prikazana je statistika date lokacije, takođe postoji dugme sa natpisom ,,Da\".\n"
                + "Pritiskom na to dugme, preći ćete na prozor za kreiranje ture na najtraženijoj lokaciji.";

        private string _tab4ToolTip = "Sa Vaše leve strane prikazana je statistika o zahtevima za ture na jeziku.\n\n"
                + "Postoje dva padajuća menija:\n"
                + "• Levi Vam služi da odaberete jezik za koji želite da vidite statistiku.\n"
                + "• Desni Vam služi da odaberete godinu u kojoj želite da vidite statistiku za odabrani jezik.\n\n"
                + "Sa Vaše desne strane prikazana je najtraženiji jezik u proteklih godinu dana.\n"
                + "Ispod nje prikazana je statistika datog jezika, takođe postoji dugme sa natpisom ,,Da\".\n"
                + "Pritiskom na to dugme, preći ćete na prozor za kreiranje ture na najtraženijem jeziku.";

        public RequestsViewModel(RequestService requestService, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, User loggedInGuide, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, TourNotificationService tourNotificationService, ComplexTourRequestService complexTourRequestService, SubTourRequestService subTourRequestService)
        {
            LocationConfirmCommand = new RelayCommand(Executed_LocationConfirmCommand, CanExecute_LocationConfirmCommand);
            LanguageConfirmCommand = new RelayCommand(Executed_LanguageConfirmCommand, CanExecute_LanguageConfirmCommand);
            AcceptRequestCommand = new RelayCommand(Executed_AcceptRequestCommand, CanExecute_AcceptRequestCommand);
            AcceptSubTourRequestCommand = new RelayCommand(Executed_AcceptSubTourRequestCommand, CanExecute_AcceptSubTourRequestCommand);
            DeclineRequestCommand = new RelayCommand(Executed_DeclineRequestCommand, CanExecute_DeclineRequestCommand);
            DeclineSubTourRequestCommand = new RelayCommand(Executed_DeclineSubTourRequestCommand, CanExecute_DeclineSubTourRequestCommand);
            DisplaySubTourRequestsCommand = new RelayCommand(Executed_DisplaySubTourRequestsCommand, CanExecute_DisplaySubTourRequestsCommand);
            FilterCommand = new RelayCommand(Executed_FilterCommand, CanExecute_FilterCommand);
            LocationStatisticsReportCommand = new RelayCommand(Executed_LocationStatisticsReportCommand, CanExecute_LocationStatisticsReportCommand);
            LanguageStatisticsReportCommand = new RelayCommand(Executed_LanguageStatisticsReportCommand, CanExecute_LanguageStatisticsReportCommand);
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
            _complexTourRequestService = complexTourRequestService;
            _subTourRequestService = subTourRequestService;

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
            ComplexTourRequestsToDisplay = new ObservableCollection<ComplexTourRequest>(_complexTourRequestService.GetOnHold());
            SubTourRequestsToDisplay = new ObservableCollection<SubTourRequest>();

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
            DateStartDisplayDateEnd = DateTime.MaxValue;
            DateEndDisplayDateStart = DateTime.Today.AddDays(2);

            _filteringEnabled = true;
        }

        private void UpdateDatePickerBlackoutDates()
        {
            DateTime dateStart, dateEnd;
            if (DateTime.TryParse(DateStartTextBox, out dateStart) && DateTime.TryParse(DateEndTextBox, out dateEnd))
            {
                if (dateStart > dateEnd)
                {
                    LabelEvent();
                    _filteringEnabled = false;
                }
                else
                {
                    _filteringEnabled = true;
                }
            }
        }

        private void LabelEvent()
        {
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
            else if (SelectedTabIndex == 2)
            {
                ToolTipContent = _tab3ToolTip;
            }
            else
            {
                ToolTipContent = _tab4ToolTip;
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

        public void HandleSubTourRequest(bool accepted)
        {
            if (SelectedSubTourRequest != null)
            {
                var state = accepted ? RequestsState.Accepted : RequestsState.Invalid;
                _requestService.UpdateState(SelectedSubTourRequest.TourRequest, state);
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
            CreateTourFromFrequentLanguageView createTourFromFrequentLanguageView = new(TheMostRequestedLanguage, _tourService, _locationService, _keyPointService, _tourReviewService, _requestService, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, LoggedInGuide, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(createTourFromFrequentLanguageView);
        }

        private bool CanExecute_LanguageConfirmCommand(object obj)
        {
            return true;
        }

        private void Executed_LocationConfirmCommand(object obj)
        {
            CreateTourFromFrequentLocationView createTourFromFrequentLocationView = new(TheMostRequestedLocation, _tourService, _locationService, _keyPointService, _tourReviewService, _requestService, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, LoggedInGuide, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(createTourFromFrequentLocationView);
        }

        private bool CanExecute_LocationConfirmCommand(object obj)
        {
            return true;
        }

        private void Executed_AcceptRequestCommand(object obj)
        {
            CreateTourFromRequestView createTourFromRequestView = new(SelectedRequest, _tourService, _locationService, _keyPointService, _tourReviewService, _requestService, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, LoggedInGuide, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(createTourFromRequestView);
            HandleRequest(true);
        }

        private bool CanExecute_AcceptRequestCommand(object obj)
        {
            return true;
        }

        private void Executed_AcceptSubTourRequestCommand(object obj)
        {
            CreateTourFromRequestView createTourFromRequestView = new(SelectedSubTourRequest.TourRequest, _tourService, _locationService, _keyPointService, _tourReviewService, _requestService, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, LoggedInGuide, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(createTourFromRequestView);
            HandleSubTourRequest(true);
        }

        private bool CanExecute_AcceptSubTourRequestCommand(object obj)
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

        private void Executed_DeclineSubTourRequestCommand(object obj)
        {
            HandleSubTourRequest(false);
            SubTourRequestsToDisplay.Clear();
            foreach (var subTour in _subTourRequestService.GetByComplexTourId(SelectedComplexTourRequest.Id))
            {
                if (subTour.TourRequest.State == RequestsState.OnHold)
                {
                    SubTourRequestsToDisplay.Add(subTour);
                }
            }
        }

        private bool CanExecute_DeclineSubTourRequestCommand(object obj)
        {
            return true;
        }

        private void Executed_DisplaySubTourRequestsCommand(object obj)
        {
            SubTourRequestsToDisplay.Clear();
            foreach (var subTour in _subTourRequestService.GetByComplexTourId(SelectedComplexTourRequest.Id))
            {
                if (subTour.TourRequest.State == RequestsState.OnHold)
                {
                    SubTourRequestsToDisplay.Add(subTour);
                }
            }
        }

        private bool CanExecute_DisplaySubTourRequestsCommand(object obj)
        {
            return SelectedComplexTourRequest != null;
        }

        private void Executed_FilterCommand(object obj)
        {
            string locationSearchTerm = LocationTextBox ?? "";
            string guestNumberSearchTerm = GuestNumberTextBox ?? "";
            string languageSearchTerm = LanguageTextBox ?? "";
            string dateStartSearchTerm = DateStartTextBox ?? "";
            string dateEndSearchTerm = DateEndTextBox ?? "";

            RequestsToDisplay.Clear();
            foreach (var filteredRequest in FilterRequests(locationSearchTerm, guestNumberSearchTerm, languageSearchTerm, dateStartSearchTerm, dateEndSearchTerm))
            {
                RequestsToDisplay.Add(filteredRequest);
            }
        }

        private bool CanExecute_FilterCommand(object obj)
        {
            return _filteringEnabled;
        }

        private void Executed_LanguageStatisticsReportCommand(object obj)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont fontTitle = new("Arial", 14, XFontStyle.Bold);
            XFont font = new("Arial", 10);
            XFont fontHeader = new("Arial", 10, XFontStyle.Bold);

            gfx.DrawString("Statistika o svim/prihvaćenim/neprihvaćenim zahtevima za ture", fontTitle, XBrushes.Black,
                new XRect(0, 10, page.Width, page.Height),
                XStringFormats.TopCenter);

            gfx.DrawString("Datum izdavanja izveštaja: " + DateTime.Today.ToString("dd-MM-yyyy"), font, XBrushes.Black, new XPoint(20, 60));
            gfx.DrawString("Primalac izveštaja: " + LoggedInGuide.Name + " " + LoggedInGuide.Surname, font, XBrushes.Black, new XPoint(20, 75));
            gfx.DrawString("Turistička agencija koja izdaje izveštaj: Horizont", font, XBrushes.Black, new XPoint(20, 90));

            if (SelectedLanguageYearsComboBoxItem.ToString() == "Svih vremena")
            {
                gfx.DrawString("Za pregled statistike odabrane su sve godine: ", font, XBrushes.Black, new XPoint(20, 105));
            }
            else
            {
                gfx.DrawString("Odabrana godina za pregled statistike: " + SelectedLanguageYearsComboBoxItem.ToString(), font, XBrushes.Black, new XPoint(20, 105));
            }

            //Table header
            gfx.DrawString("Jezik", fontHeader, XBrushes.Black, new XPoint(20, 150));
            gfx.DrawString("Broj zahteva", fontHeader, XBrushes.Black, new XPoint(120, 150));
            gfx.DrawString("Procenat prihvacenih zahteva", fontHeader, XBrushes.Black, new XPoint(220, 150));
            gfx.DrawString("Procenat neprihvacenih zahteva", fontHeader, XBrushes.Black, new XPoint(420, 150));

            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, 160), new XPoint(570, 160));

            gfx.DrawString(SelectedLanguagesComboBoxItem.ToString(), font, XBrushes.Black, new XPoint(20, 180));
            gfx.DrawString(_requestService.GetByLanguageCount(SelectedLanguageYearsComboBoxItem.ToString(), SelectedLanguagesComboBoxItem.ToString()).ToString(), font, XBrushes.Black, new XPoint(120, 180));
            gfx.DrawString(_requestService.GetUsersAcceptedPercentageByYearAndLanguage(SelectedLanguageYearsComboBoxItem.ToString(), SelectedLanguagesComboBoxItem.ToString()).ToString("0.00") + "%", font, XBrushes.Black, new XPoint(220, 180));
            gfx.DrawString(_requestService.GetUsersDeclinedPercentageByYearAndLanguage(SelectedLanguageYearsComboBoxItem.ToString(), SelectedLanguagesComboBoxItem.ToString()).ToString("0.00") + "%", font, XBrushes.Black, new XPoint(420, 180));

            document.Save("C:\\Users\\HP Pavilion\\Documents\\GitHub\\SIMS-Project\\Sims2023\\Sims2023\\Resources\\GuideResources\\LanguageRequestStatistics.pdf");
        }

        private bool CanExecute_LanguageStatisticsReportCommand(object obj)
        {
            return SelectedLanguagesComboBoxItem != null && SelectedLanguageYearsComboBoxItem != null;
        }

        private void Executed_LocationStatisticsReportCommand(object obj)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont fontTitle = new("Arial", 14, XFontStyle.Bold);
            XFont font = new("Arial", 10);
            XFont fontHeader = new("Arial", 10, XFontStyle.Bold);

            gfx.DrawString("Statistika o svim/prihvaćenim/neprihvaćenim zahtevima za ture", fontTitle, XBrushes.Black,
                new XRect(0, 10, page.Width, page.Height),
                XStringFormats.TopCenter);

            gfx.DrawString("Datum izdavanja izveštaja: " + DateTime.Today.ToString("dd-MM-yyyy"), font, XBrushes.Black, new XPoint(20, 60));
            gfx.DrawString("Primalac izveštaja: " + LoggedInGuide.Name + " " + LoggedInGuide.Surname, font, XBrushes.Black, new XPoint(20, 75));
            gfx.DrawString("Turistička agencija koja izdaje izveštaj: Horizont", font, XBrushes.Black, new XPoint(20, 90));

            if (SelectedLocationYearsComboBoxItem.ToString() == "Svih vremena")
            {
                gfx.DrawString("Za pregled statistike odabrane su sve godine: ", font, XBrushes.Black, new XPoint(20, 105));
            }
            else
            {
                gfx.DrawString("Odabrana godina za pregled statistike: " + SelectedLocationYearsComboBoxItem.ToString(), font, XBrushes.Black, new XPoint(20, 105));
            }

            //Table header
            gfx.DrawString("Lokacija", fontHeader, XBrushes.Black, new XPoint(20, 150));
            gfx.DrawString("Broj zahteva", fontHeader, XBrushes.Black, new XPoint(120, 150));
            gfx.DrawString("Procenat prihvacenih zahteva", fontHeader, XBrushes.Black, new XPoint(220, 150));
            gfx.DrawString("Procenat neprihvacenih zahteva", fontHeader, XBrushes.Black, new XPoint(420, 150));

            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, 160), new XPoint(570, 160));

            gfx.DrawString(SelectedLocationsComboBoxItem.ToString(), font, XBrushes.Black, new XPoint(20, 180));
            gfx.DrawString(_requestService.GetByLocationCount(SelectedLocationYearsComboBoxItem.ToString(), SelectedLocationsComboBoxItem.ToString()).ToString(), font, XBrushes.Black, new XPoint(120, 180));
            gfx.DrawString(_requestService.GetUsersAcceptedPercentageByYearAndLocation(SelectedLocationYearsComboBoxItem.ToString(), SelectedLocationsComboBoxItem.ToString()).ToString("0.00") + "%", font, XBrushes.Black, new XPoint(220, 180));
            gfx.DrawString(_requestService.GetUsersDeclinedPercentageByYearAndLocation(SelectedLocationYearsComboBoxItem.ToString(), SelectedLocationsComboBoxItem.ToString()).ToString("0.00") + "%", font, XBrushes.Black, new XPoint(420, 180));

            document.Save("C:\\Users\\HP Pavilion\\Documents\\GitHub\\SIMS-Project\\Sims2023\\Sims2023\\Resources\\GuideResources\\LocationRequestStatistics.pdf");
        }

        private bool CanExecute_LocationStatisticsReportCommand(object obj)
        {
            return SelectedLocationsComboBoxItem != null && SelectedLocationYearsComboBoxItem != null;
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

        private void Executed_AccountPageNavigationCommand(object obj)
        {
            GuideAccountView guideAccountView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }

        private bool CanExecute_AccountPageNavigationCommand(object obj)
        {
            return true;
        }

        private void Executed_ReviewsPageNavigationCommand(object obj)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        private bool CanExecute_ReviewsPageNavigationCommand(object obj)
        {
            return true;
        }
    }
}
