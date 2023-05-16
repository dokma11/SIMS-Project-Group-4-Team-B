using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class LiveTourTrackingViewModel
    {
        public Tour Tour { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        private KeyPointService _keyPointService;
        private TourService _tourService;
        private TourReservationService _tourReservationService;
        private UserService _userService;
        public ObservableCollection<KeyPoint> KeyPointsToDisplay { get; set; }
        public int firstKeyPointId = -1;
        public int lastKeyPointId = -1;
        public int lastVisitedKeyPointId = -1;
        public bool LastKeyPointVisited;
        public List<User> MarkedGuests;
        public ObservableCollection<User> GuestsToDisplay { get; set; }

        public LiveTourTrackingViewModel(Tour tour, KeyPointService keyPointService, TourService tourService, TourReservationService tourReservationService, List<User> markedGuests, UserService userService)
        {
            _keyPointService = keyPointService;
            _tourService = tourService;
            _tourReservationService = tourReservationService;
            _userService = userService;

            Tour = tour;
            _tourService.UpdateState(Tour, ToursState.Started);

            MarkedGuests = markedGuests;
            KeyPointsToDisplay = new ObservableCollection<KeyPoint>(_keyPointService.GetByToursId(Tour.Id));

            firstKeyPointId = FindAndMarkFirstKeyPoint();
            SelectedKeyPoint = _keyPointService.GetById(firstKeyPointId);
            GuestsToDisplay = new ObservableCollection<User>(_tourReservationService.GetGuestsWithReservations(SelectedKeyPoint, MarkedGuests));

            lastVisitedKeyPointId = firstKeyPointId;
            lastKeyPointId = FindLastKeyPoint();
            LastKeyPointVisited = false;
        }

        private int FindAndMarkFirstKeyPoint()
        {
            KeyPoint firstKeyPoint = new()
            {
                Id = int.MaxValue
            };

            firstKeyPoint = KeyPointsToDisplay.MinBy(keyPoint => keyPoint.Id);

            _keyPointService.ChangeKeyPointsState(firstKeyPoint, KeyPointsState.BeingVisited);

            return firstKeyPoint.Id;
        }

        private void MarkLastVisitedKeyPoint()
        {
            var keyPoint = KeyPointsToDisplay.FirstOrDefault(k => k.Id == lastVisitedKeyPointId);
            if (keyPoint != null)
            {
                _keyPointService.ChangeKeyPointsState(keyPoint, KeyPointsState.Visited);
            }
        }

        private int FindLastKeyPoint()
        {
            lastKeyPointId = KeyPointsToDisplay.Max(keyPoint => keyPoint.Id);
            return lastKeyPointId;
        }

        private void MarkLastKeyPoint()
        {
            KeyPointsToDisplay.Single(keyPoint => keyPoint.Id == lastKeyPointId).CurrentState = KeyPointsState.Visited;
        }

        public void MarkKeyPoint()
        {
            //mark previous key point as visited
            _keyPointService.ChangeKeyPointsState(KeyPointsToDisplay.First(k => k.Id == lastVisitedKeyPointId), KeyPointsState.Visited);

            _keyPointService.ChangeKeyPointsState(SelectedKeyPoint, KeyPointsState.BeingVisited);
            lastVisitedKeyPointId = SelectedKeyPoint.Id;

            if (SelectedKeyPoint.Id == lastKeyPointId)
            {
                UpdateKeyPointList();
                _tourService.UpdateState(Tour, ToursState.Finished);
                MarkLastKeyPoint();
                LastKeyPointVisited = true;
            }

            UpdateKeyPointList();
        }

        public void AddMarkedGuests(List<User> items)
        {
            foreach (User guest in items)
            {
                _keyPointService.AddGuestsId(SelectedKeyPoint, guest.Id);
                MarkedGuests.Add(guest);
                ShouldConfirmParticipation(guest);
                GuestsToDisplay.Remove(guest);
            }
            _keyPointService.Save();
        }

        private void ShouldConfirmParticipation(User user)
        {
            foreach (var tourReservation in _tourReservationService.GetAll())
            {
                if (tourReservation.User.Id == user.Id && tourReservation.Tour.Id == SelectedKeyPoint.Tour.Id)
                {
                    tourReservation.ShouldConfirmParticipation = true;
                    _tourReservationService.Save();
                }
            }
        }

        public void CancelTour()
        {
            _tourService.UpdateState(Tour, ToursState.Interrupted);
            MarkLastVisitedKeyPoint();
            _keyPointService.Save();
        }

        public bool IsKeyPointSelected()
        {
            return SelectedKeyPoint != null;
        }

        public bool IsKeyPointBeingVisited()
        {
            return SelectedKeyPoint.CurrentState == KeyPointsState.BeingVisited;
        }

        public bool IsKeyPointVisited()
        {
            return SelectedKeyPoint.CurrentState == KeyPointsState.Visited;
        }

        public bool IsKeyPointNextInLine()
        {
            return SelectedKeyPoint.Id == lastVisitedKeyPointId + 1;
        }

        public void UpdateKeyPointList()
        {
            KeyPointsToDisplay.Clear();
            foreach (var keyPoint in _keyPointService.GetAll())
            {
                if (keyPoint.Tour.Id == Tour.Id)
                {
                    KeyPointsToDisplay.Add(keyPoint);
                }
            }
            _keyPointService.Save();
        }

        public bool AreAllGuestsAreMarked()
        {
            return _tourReservationService.GetByToursid(Tour.Id).Count == MarkedGuests.Count;
        }
    }
}
