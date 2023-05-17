using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;

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
        private CountriesAndCitiesService _countriesAndCitiesService;
        private RequestService _requestService;
        private TourNotificationService _tourNotificationService;
        public RequestsLanguage SelectedLanguage { get; set; }
        private List<DateTime> _dateTimeList;
        private List<string> _keyPointsList;
        public User LoggedInGuide { get; set; }
        
        public CreateTourFromFrequentLanguageViewModel(RequestsLanguage selectedLanguage, TourService tourService, LocationService locationService, KeyPointService keyPointService, User loggedInGuide, RequestService requestService, TourNotificationService tourNotificationService)
        {
            SelectedLanguage = selectedLanguage;

            NewTour = new();
            NewLocation = new();
            NewKeyPoint = new();

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _requestService = requestService;
            _tourNotificationService = tourNotificationService;
            _countriesAndCitiesService = new CountriesAndCitiesService();

            _dateTimeList = new List<DateTime>();
            _keyPointsList = new List<string>();
            
            LoggedInGuide = loggedInGuide;
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

        public void ConfirmCreation(string country, string city)
        {
            NewLocation.City = city;
            NewLocation.Country = country;
            NewTour.Location = NewLocation;
            NewTour.GuideLanguage = (ToursLanguage)SelectedLanguage;
            _locationService.Create(NewLocation);
            _tourService.Create(NewTour, _dateTimeList, NewLocation, LoggedInGuide);
            int firstToursId = NewTour.Id - _dateTimeList.Count + 1;
            _tourService.AddToursLocation(NewTour, NewLocation, _dateTimeList.Count);
            _keyPointService.Create(NewKeyPoint, _keyPointsList, firstToursId, _dateTimeList.Count);
            _tourService.AddToursKeyPoints(_keyPointsList, firstToursId);
            NotifyGuests();
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
