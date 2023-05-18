﻿using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class CreateTourFromRequestViewModel
    {
        public Tour Tour { get; set; }
        public KeyPoint KeyPoint { get; set; }
        public Request SelectedRequest { get; set; }
        private List<string> _keyPointsList;
        private List<DateTime> _dateTimeList;
        public User LoggedInGuide { get; set; }
        private TourService _tourService;
        private KeyPointService _keyPointService;
        private RequestService _requestService;
        private TourNotificationService _tourNotificationService;
        private TourReservationService _tourReservationService;
        public CreateTourFromRequestViewModel(Request selectedRequest, User loggedInGuide, TourService tourService, KeyPointService keyPointService, RequestService requestService, TourNotificationService tourNotificationService, TourReservationService tourReservationService)
        {
            SelectedRequest = selectedRequest;

            Tour = new()
            {
                Description = SelectedRequest.Description,
                MaxGuestNumber = SelectedRequest.GuestNumber,
                Location = SelectedRequest.Location,
                GuideLanguage = (ToursLanguage)SelectedRequest.Language
            };
            KeyPoint = new();

            _keyPointsList = new();
            _dateTimeList = new();
            LoggedInGuide = loggedInGuide;
            _tourService = tourService;
            _keyPointService = keyPointService;
            _requestService = requestService;
            _tourNotificationService = tourNotificationService;
            _tourReservationService = tourReservationService;
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

        public void ConfirmCreation()
        {
            _tourService.Create(Tour, _dateTimeList, SelectedRequest.Location, LoggedInGuide);
            int firstToursId = Tour.Id - _dateTimeList.Count + 1;
            _tourService.AddToursLocation(Tour, SelectedRequest.Location, _dateTimeList.Count);
            _keyPointService.Create(KeyPoint, _keyPointsList, firstToursId, _dateTimeList.Count);
            _tourService.AddToursKeyPoints(_keyPointsList, firstToursId);
            NotifyGuest();
        }

        public void NotifyGuest()
        {
            TourNotification tourNotification = new(Tour, SelectedRequest.Guest, NotificationType.AcceptedTourRequest);
            _tourNotificationService.Create(tourNotification);
            TourReservation tourReservation = new(0, Tour, SelectedRequest.Guest, SelectedRequest.GuestNumber, false, false);
            _tourReservationService.Create(tourReservation);
        }

        public void CancelCreation()
        {
            _requestService.UpdateState(SelectedRequest, RequestsState.OnHold);
        }
    }
}
