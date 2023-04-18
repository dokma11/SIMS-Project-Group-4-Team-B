using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class CreateTourViewModel
    {
        public Tour Tour { get; set; }
        public Location Location { get; set; }
        public KeyPoint KeyPoint { get; set; }
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        private List<DateTime> _dateTimeList;
        private List<string> _keyPointsList;
        public User LoggedInGuide { get; set; }
        public string Country;
        public string City;
        public CreateTourViewModel(TourService tourService, LocationService locationService, KeyPointService keyPointService, User loggedInGuide)
        {
            Tour = new Tour();
            Location = new Location();
            KeyPoint = new KeyPoint();

            _dateTimeList = new List<DateTime>();
            _keyPointsList = new List<string>();

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;

            LoggedInGuide = loggedInGuide;

            _countriesAndCitiesService = new CountriesAndCitiesService();  
        }

        public List<CountriesAndCities> GetCitiesAndCountries()
        {
            return _countriesAndCitiesService.GetAllLocations();
        }

        public void SetToursLanguage(string languageString)
        {
            if (Enum.TryParse(languageString, out ToursLanguage language))
            {
                _tourService.SetToursLanguage(Tour, language);
            }
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

        public void ConfirmCreation(string city, string country)
        {
            Location.City = city;
            Location.Country = country;
            MessageBox.Show(Location.City);
            MessageBox.Show(Location.Country);
            Tour.Location = Location;
            _locationService.Create(Location);
            _tourService.Create(Tour, _dateTimeList, Location, LoggedInGuide);
            int firstToursId = Tour.Id - _dateTimeList.Count + 1;
            _tourService.AddToursLocation(Tour, Location, _dateTimeList.Count);
            _keyPointService.Create(KeyPoint, _keyPointsList, firstToursId, _dateTimeList.Count);
            _tourService.AddToursKeyPoints(_keyPointsList, firstToursId);
        }
    }
}