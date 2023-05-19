using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class CreateTourFromFrequentLocationViewModel
    {
        public Tour NewTour { get; set; }
        public KeyPoint NewKeyPoint { get; set; }
        private TourService _tourService;
        private KeyPointService _keyPointService;
        private RequestService _requestService;
        private TourNotificationService _tourNotificationService;
        public Location SelectedLocation { get; set; }
        private List<DateTime> _dateTimeList;
        private List<string> _keyPointsList;
        public User LoggedInGuide { get; set; }
        public CreateTourFromFrequentLocationViewModel(Location selectedLocation, TourService tourService, KeyPointService keyPointService, User loggedInGuide, RequestService requestService, TourNotificationService tourNotificationService)
        {
            SelectedLocation = selectedLocation;

            NewTour = new();
            NewKeyPoint = new();

            _tourService = tourService;
            _keyPointService = keyPointService;
            _requestService = requestService;
            _tourNotificationService = tourNotificationService;

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
                _tourService.SetLanguage(NewTour, language);
            }
        }

        public void ConfirmCreation()
        {
            NewTour.Location = SelectedLocation;
            _tourService.Create(NewTour, _dateTimeList, SelectedLocation, LoggedInGuide);
            int firstToursId = NewTour.Id - _dateTimeList.Count + 1;
            _tourService.AddToursLocation(NewTour, SelectedLocation, _dateTimeList.Count);
            _keyPointService.Create(NewKeyPoint, _keyPointsList, firstToursId, _dateTimeList.Count);
            _tourService.AddToursKeyPoints(_keyPointsList, firstToursId);
            NotifyGuests();
        }

        public void NotifyGuests()
        {
            foreach (var req in _requestService.GetByLocation(SelectedLocation))
            {
                TourNotification tourNotification = new(NewTour, req.Guest, NotificationType.MatchedTourRequestsLocation);
                _tourNotificationService.Create(tourNotification);
            }
        }
    }
}
