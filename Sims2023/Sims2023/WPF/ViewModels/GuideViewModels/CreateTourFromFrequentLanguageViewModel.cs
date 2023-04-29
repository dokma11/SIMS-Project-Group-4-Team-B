using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class CreateTourFromFrequentLanguageViewModel
    {
        public Tour Tour { get; set; }
        public Location Location { get; set; }
        public KeyPoint KeyPoint { get; set; }
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        public RequestsLanguage SelectedLanguage { get; set; }
        private List<DateTime> _dateTimeList;
        private List<string> _keyPointsList;
        public User LoggedInGuide { get; set; }
        
        public CreateTourFromFrequentLanguageViewModel(RequestsLanguage selectedLanguage, TourService tourService, LocationService locationService, KeyPointService keyPointService, User loggedInGuide)
        {
            SelectedLanguage = selectedLanguage;

            Tour = new();
            Location = new();
            KeyPoint = new();

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;
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
            Location.City = city;
            Location.Country = country;
            Tour.Location = Location;
            Tour.GuideLanguage = (ToursLanguage)SelectedLanguage;
            _locationService.Create(Location);
            _tourService.Create(Tour, _dateTimeList, Location, LoggedInGuide);
            int firstToursId = Tour.Id - _dateTimeList.Count + 1;
            _tourService.AddToursLocation(Tour, Location, _dateTimeList.Count);
            _keyPointService.Create(KeyPoint, _keyPointsList, firstToursId, _dateTimeList.Count);
            _tourService.AddToursKeyPoints(_keyPointsList, firstToursId);
        }
    }
}
