using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class ToursViewModel
    {
        public Tour SelectedCreatedTour { get; set; }
        public Tour SelectedFinishedTour { get; set; }
        public Tour NewTour { get; set; }
        public Location NewLocation { get; set; }
        public KeyPoint NewKeyPoint { get; set; }
        public ObservableCollection<Tour> CreatedToursToDisplay { get; set; }
        public ObservableCollection<Tour> FinishedToursToDisplay { get; set; }

        private TourService _tourService;
        private VoucherService _voucherService;
        private TourReservationService _tourReservationService;
        private UserService _userService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;

        public List<DateTime> DateTimeList;
        public List<string> KeyPointsList;
        public User LoggedInGuide { get; set; }

        public ToursViewModel(TourService tourService, VoucherService voucherService, TourReservationService tourReservationService, UserService userService, User loggedInGuide, CountriesAndCitiesService countriesAndCitiesService, LocationService locationService, KeyPointService keyPointService)
        {
            _tourService = tourService;
            _voucherService = voucherService;
            _tourReservationService = tourReservationService;
            _userService = userService;
            _countriesAndCitiesService = countriesAndCitiesService;
            _locationService = locationService;
            _keyPointService = keyPointService;

            LoggedInGuide = loggedInGuide;

            NewTour = new();
            NewLocation = new();
            NewKeyPoint = new();

            CreatedToursToDisplay = new ObservableCollection<Tour>(_tourService.GetGuidesCreated(LoggedInGuide));
            FinishedToursToDisplay = new ObservableCollection<Tour>(_tourService.GetFinished(LoggedInGuide));

            DateTimeList = new();
            KeyPointsList = new();
        }

        public void CancelTour(string additionalComment)
        {
            _tourService.UpdateState(SelectedCreatedTour, ToursState.Cancelled);
            CreateVouchersForCancelledTour(SelectedCreatedTour.Id, additionalComment);
            Update();
        }

        public void CreateVouchersForCancelledTour(int toursId, string additionalComment)
        {
            foreach (var reservation in _tourReservationService.GetReservationsByToursid(toursId))
            {
                Voucher voucher = new(0, Voucher.VoucherType.CancelingTour, _userService.GetById(reservation.User.Id), _tourService.GetById(toursId), DateTime.Now, DateTime.Today.AddYears(1), additionalComment, false);
                _voucherService.Create(voucher);
            }
        }

        public bool IsTourSelected()
        {
            return SelectedCreatedTour != null;
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

        public void SetToursLanguage(string languageString)
        {
            if (Enum.TryParse(languageString, out ToursLanguage language))
            {
                _tourService.SetLanguage(NewTour, language);
            }
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

        public void Update()
        {
            CreatedToursToDisplay.Clear();
            foreach(var tour in _tourService.GetGuidesCreated(LoggedInGuide))
            {
                CreatedToursToDisplay.Add(tour);
            }
        }
    }
}
