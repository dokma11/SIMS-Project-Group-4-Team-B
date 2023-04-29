using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class CreateTourFromFrequentLocationViewModel
    {
        public Tour Tour { get; set; }
        public KeyPoint KeyPoint { get; set; }
        private TourService _tourService;
        private KeyPointService _keyPointService;
        public Location SelectedLocation { get; set; }
        private List<DateTime> _dateTimeList;
        private List<string> _keyPointsList;
        public User LoggedInGuide { get; set; }
        public CreateTourFromFrequentLocationViewModel(Location selectedLocation, TourService tourService, KeyPointService keyPointService, User loggedInGuide)
        {
            SelectedLocation = selectedLocation;

            Tour = new();
            KeyPoint = new();

            _tourService = tourService;
            _keyPointService = keyPointService;

            _dateTimeList = new List<DateTime>();
            _keyPointsList = new List<string>();

            LoggedInGuide = loggedInGuide;
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

        public void SetToursLanguage(string languageString)
        {
            if (Enum.TryParse(languageString, out ToursLanguage language))
            {
                _tourService.SetLanguage(Tour, language);
            }
        }

        public void ConfirmCreation()
        {
            Tour.Location = SelectedLocation;
            _tourService.Create(Tour, _dateTimeList, SelectedLocation, LoggedInGuide);
            int firstToursId = Tour.Id - _dateTimeList.Count + 1;
            _tourService.AddToursLocation(Tour, SelectedLocation, _dateTimeList.Count);
            _keyPointService.Create(KeyPoint, _keyPointsList, firstToursId, _dateTimeList.Count);
            _tourService.AddToursKeyPoints(_keyPointsList, firstToursId);
        }
    }
}
