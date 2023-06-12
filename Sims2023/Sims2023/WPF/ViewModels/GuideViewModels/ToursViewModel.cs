using LiveCharts;
using LiveCharts.Defaults;
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
    public partial class ToursViewModel: INotifyPropertyChanged
    {
        public Tour SelectedCreatedTour { get; set; }
        public Tour StartedTour { get; set; }
        public Tour SelectedFinishedTour { get; set; }
        private Tour _newTour;

        public Tour NewTour
        {
            get { return _newTour; }
            set
            {
                _newTour = value;
                OnPropertyChanged(nameof(NewTour));
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

        public Location NewLocation { get; set; }
        public KeyPoint NewKeyPoint { get; set; }
        public ObservableCollection<Tour> CreatedToursToDisplay { get; set; }
        public ObservableCollection<Tour> FinishedToursToDisplay { get; set; }
        public ObservableCollection<Tour> TheMostVisitedTour { get; set; }
        public ObservableCollection<string> LabelsForTheMostVisitedTour { get; set; }
        public SeriesCollection TheMostVisitedTourSeriesCollection { get; set; }
        public SeriesCollection PieSeries { get; set; }
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

        public List<DateTime> DateTimeList;
        public List<string> KeyPointsList;
        public User LoggedInGuide { get; set; }
        public ObservableCollection<string> ComboBoxItems { get; set; }

        public RelayCommand HomePageNavigationCommand { get; set; }
        public RelayCommand RequestsPageNavigationCommand { get; set; }
        public RelayCommand ReviewsPageNavigationCommand { get; set; }
        public RelayCommand AccountPageNavigationCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ToursViewModel(TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, RequestService requestService, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, User loggedInGuide, TourNotificationService tourNotificationService, ComplexTourRequestService complexTourRequestService, SubTourRequestService subTourRequestService)
        {
            HomePageNavigationCommand = new RelayCommand(Executed_HomePageNavigationCommand, CanExecute_HomePageNavigationCommand);
            RequestsPageNavigationCommand = new RelayCommand(Executed_RequestsPageNavigationCommand, CanExecute_RequestsPageNavigationCommand);
            ReviewsPageNavigationCommand = new RelayCommand(Executed_ReviewsPageNavigationCommand, CanExecute_ReviewsPageNavigationCommand);
            AccountPageNavigationCommand = new RelayCommand(Executed_AccountPageNavigationCommand, CanExecute_AccountPageNavigationCommand);

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
            _complexTourRequestService = complexTourRequestService;
            _subTourRequestService = subTourRequestService;

            LoggedInGuide = loggedInGuide;

            NewTour = new();
            NewLocation = new();
            NewKeyPoint = new();
            StartedTour = new();

            GetAttendedGuestsNumber();

            CreatedToursToDisplay = new ObservableCollection<Tour>(_tourService.GetGuidesCreated(LoggedInGuide));
            FinishedToursToDisplay = new ObservableCollection<Tour>(_tourService.GetFinished(LoggedInGuide));
            TheMostVisitedTour = new ObservableCollection<Tour>
            {
                _tourService.GetTheMostVisitedTour(LoggedInGuide, "Svih vremena")
            };

            DateTimeList = new();
            KeyPointsList = new();

            LabelsForTheMostVisitedTour = new()
            {
                "Ispod 18",
                "Izmedju 18 i 50",
                "Vise of 50"
            };
            TheMostVisitedTourSeriesCollection = new();
            GetAgeStatistics();

            GetVoucherStatistics();

            ComboBoxItems = new ObservableCollection<string>();
            GetYearsForComboBox();

            IsLabelVisible = _tourService.GetAll().Any(tour => tour.CurrentState == ToursState.Started);
            
            if(IsLabelVisible)
            {
                StartedTour = _tourService.GetAll().FirstOrDefault(tour => tour.CurrentState == ToursState.Started);
            }
        }

        public void CancelTour(string additionalComment)
        {
            _tourService.UpdateState(SelectedCreatedTour, ToursState.Cancelled);
            CreateVouchersForCancelledTour(SelectedCreatedTour.Id, additionalComment);
            Update();
        }

        public void CreateVouchersForCancelledTour(int toursId, string additionalComment)
        {
            foreach (var reservation in _tourReservationService.GetByToursid(toursId))
            {
                Voucher voucher = new(0, Voucher.VoucherType.CancelingTour, _userService.GetById(reservation.User.Id), _tourService.GetById(toursId), DateTime.Now, DateTime.Today.AddYears(1), additionalComment, false);
                _voucherService.Create(voucher);
            }
        }

        public bool IsCreatedTourSelected()
        {
            return SelectedCreatedTour != null;
        }

        public bool IsFinishedTourSelected()
        {
            return SelectedFinishedTour != null;
        }

        public bool IsTourEligibleForCancellation()
        {
            return SelectedCreatedTour.Start >= DateTime.Now.AddHours(48);
        }

        //KREIRANJE TURE

        public List<CountriesAndCities> GetCitiesAndCountries()
        {
            return _countriesAndCitiesService.GetAllLocations();
        }

        public void AddKeyPointsToList(string inputText)
        {
            if (!KeyPointsList.Contains(inputText))
            {
                KeyPointsList.Add(inputText);
            }
        }

        public void AddDatesToList(string inputText)
        {
            if (DateTime.TryParse(inputText, out DateTime dateTime))
            {
                if (!DateTimeList.Contains(dateTime))
                {
                    DateTimeList.Add(dateTime);
                }
            }
        }

        public void ConfirmCreation(string country, string city)
        {
            NewLocation.City = city;
            NewLocation.Country = country;
            NewTour.Location = NewLocation;
            _locationService.Create(NewLocation);
            _tourService.Create(NewTour, DateTimeList, NewLocation, LoggedInGuide);
            int firstToursId = NewTour.Id - DateTimeList.Count + 1;
            _tourService.AddToursLocation(NewTour, NewLocation, DateTimeList.Count);
            _keyPointService.Create(NewKeyPoint, KeyPointsList, firstToursId, DateTimeList.Count);
            _tourService.AddToursKeyPoints(KeyPointsList, firstToursId);
        }

        //STATISTIKA TURE

        public void GetAttendedGuestsNumber()
        {
            _tourReservationService.GetAttendedGuestsNumber(LoggedInGuide);
        }

        public void GetAgeStatistics()
        {
            var ageStats = new ChartValues<int>
            {
                _tourReservationService.GetAgeStatistics(_tourService.GetTheMostVisitedTour(LoggedInGuide, "Svih vremena"), "young"),
                _tourReservationService.GetAgeStatistics(_tourService.GetTheMostVisitedTour(LoggedInGuide, "Svih vremena"), "middleAged"),
                _tourReservationService.GetAgeStatistics(_tourService.GetTheMostVisitedTour(LoggedInGuide, "Svih vremena"), "old")
            };
            TheMostVisitedTourSeriesCollection.Add(new ColumnSeries { Values = ageStats, Title = "Statistika ljudi po uzrastu" });
            Values = value => value.ToString("D");
        }

        public void GetVoucherStatistics()
        {
            PieSeries = new SeriesCollection()
            {
                new PieSeries
                {
                    Title = "Sa vaučerom",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_tourReservationService.GetVoucherStatistics(_tourService.GetTheMostVisitedTour(LoggedInGuide, "Svih vremena"), true)) },
                    DataLabels = true
                },

                new PieSeries
                {
                    Title = "Bez vaučera",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(_tourReservationService.GetVoucherStatistics(_tourService.GetTheMostVisitedTour(LoggedInGuide, "Svih vremena"), false)) },
                    DataLabels = true
                }
            };
        }

        public void GetYearsForComboBox()
        {
            foreach (var tour in _tourService.GetFinished(LoggedInGuide))
            {
                if (!ComboBoxItems.Contains(tour.Start.Year.ToString()))
                {
                    ComboBoxItems.Add(tour.Start.Year.ToString());
                }
            }
            ComboBoxItems.Add("Svih vremena");
        }

        public void UpdateTheMostVisitedTour(User loggedInGuide, string year)
        {
            TheMostVisitedTour.Clear();
            TheMostVisitedTour.Add(_tourService.GetTheMostVisitedTour(loggedInGuide, year));

            UpdateTheMostVistiedTourAgeStatistics(year);
            UpdateTheMostVistiedTourVoucherStatistics(year);
        }

        public void UpdateTheMostVistiedTourAgeStatistics(string year)
        {
            TheMostVisitedTourSeriesCollection.Clear();
            var ageStats = new ChartValues<int>
            {
                _tourReservationService.GetAgeStatistics(_tourService.GetTheMostVisitedTour(LoggedInGuide, year), "young"),
                _tourReservationService.GetAgeStatistics(_tourService.GetTheMostVisitedTour(LoggedInGuide, year), "middleAged"),
                _tourReservationService.GetAgeStatistics(_tourService.GetTheMostVisitedTour(LoggedInGuide, year), "old")
            };
            TheMostVisitedTourSeriesCollection.Add(new ColumnSeries { Values = ageStats, Title = "Statistika ljudi po uzrastu" });
            Values = value => value.ToString("D");
        }

        public void UpdateTheMostVistiedTourVoucherStatistics(string year)
        {
            PieSeries.Clear();

            var withVoucher = new PieSeries()
            {
                Title = "Sa vaučerom",
                Values = new ChartValues<ObservableValue> { new ObservableValue(_tourReservationService.GetVoucherStatistics(_tourService.GetTheMostVisitedTour(LoggedInGuide, year), true)) },
                DataLabels = true
            };
            PieSeries.Add(withVoucher);

            var withoutVoucher = new PieSeries()
            {
                Title = "Bez vaučerom",
                Values = new ChartValues<ObservableValue> { new ObservableValue(_tourReservationService.GetVoucherStatistics(_tourService.GetTheMostVisitedTour(LoggedInGuide, year), false)) },
                DataLabels = true
            };
            PieSeries.Add(withoutVoucher);
        }

        public void DisplayStatistics()
        {
            TheMostVisitedTourSeriesCollection.Clear();
            var ageStats = new ChartValues<int>
            {
                _tourReservationService.GetAgeStatistics(SelectedFinishedTour, "young"),
                _tourReservationService.GetAgeStatistics(SelectedFinishedTour, "middleAged"),
                _tourReservationService.GetAgeStatistics(SelectedFinishedTour, "old")
            };
            TheMostVisitedTourSeriesCollection.Add(new ColumnSeries { Values = ageStats, Title = "Statistika ljudi po uzrastu" });
            Values = value => value.ToString("D");

            PieSeries.Clear();

            var withVoucher = new PieSeries()
            {
                Title = "Sa vaučerom",
                Values = new ChartValues<ObservableValue> { new ObservableValue(_tourReservationService.GetVoucherStatistics(SelectedFinishedTour, true)) },
                DataLabels = true
            };
            PieSeries.Add(withVoucher);

            var withoutVoucher = new PieSeries()
            {
                Title = "Bez vaučerom",
                Values = new ChartValues<ObservableValue> { new ObservableValue(_tourReservationService.GetVoucherStatistics(SelectedFinishedTour, false)) },
                DataLabels = true
            };
            PieSeries.Add(withoutVoucher);
        }

        public void Update()
        {
            CreatedToursToDisplay.Clear();
            foreach (var tour in _tourService.GetGuidesCreated(LoggedInGuide))
            {
                CreatedToursToDisplay.Add(tour);
            }
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

        private void Executed_RequestsPageNavigationCommand(object obj)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        private bool CanExecute_RequestsPageNavigationCommand(object obj)
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
