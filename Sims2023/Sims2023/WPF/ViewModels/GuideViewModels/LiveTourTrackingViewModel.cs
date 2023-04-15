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
        public ObservableCollection<KeyPoint> KeyPointsToDisplay { get; set; }
        public int firstKeyPointId = -1;
        public int lastKeyPointId = -1;
        public int lastVisitedKeyPointId = -1;
        public bool LastKeyPointVisited;
        List<User> MarkedGuests;

        public LiveTourTrackingViewModel(Tour tour, KeyPointService keyPointService, TourService tourService, TourReservationService tourReservationService, List<User> markedGuests)
        {
            _keyPointService = keyPointService;
            _tourService = tourService;
            _tourReservationService = tourReservationService;

            Tour = tour;
            _tourService.ChangeToursState(Tour, Tour.State.Started);

            MarkedGuests = markedGuests;
            KeyPointsToDisplay = new ObservableCollection<KeyPoint>(_keyPointService.GetByToursId(Tour.Id));

            firstKeyPointId = FindAndMarkFirstKeyPoint();
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

            _keyPointService.ChangeKeyPointsState(firstKeyPoint, KeyPoint.State.BeingVisited);

            return firstKeyPoint.Id;
        }

        private void MarkLastVisitedKeyPoint()
        {
            var keyPoint = KeyPointsToDisplay.FirstOrDefault(k => k.Id == lastVisitedKeyPointId);
            if (keyPoint != null)
            {
                _keyPointService.ChangeKeyPointsState(keyPoint, KeyPoint.State.Visited);
            }
        }

        private int FindLastKeyPoint()
        {
            lastKeyPointId = KeyPointsToDisplay.Max(keyPoint => keyPoint.Id);
            return lastKeyPointId;
        }

        private void MarkLastKeyPoint()
        {
            KeyPointsToDisplay.Single(keyPoint => keyPoint.Id == lastKeyPointId).CurrentState = KeyPoint.State.Visited;
        }

        public void MarkKeyPoint()
        {
            //mark previous key point as visited
            _keyPointService.ChangeKeyPointsState(KeyPointsToDisplay.First(k => k.Id == lastVisitedKeyPointId), KeyPoint.State.Visited);

            _keyPointService.ChangeKeyPointsState(SelectedKeyPoint, KeyPoint.State.BeingVisited);
            lastVisitedKeyPointId = SelectedKeyPoint.Id;

            if (SelectedKeyPoint.Id == lastKeyPointId)
            {
                UpdateKeyPointList();
                _tourService.ChangeToursState(Tour, Tour.State.Finished);
                MarkLastKeyPoint();
                LastKeyPointVisited = true;
            }

            UpdateKeyPointList();
        }

        public void MarkGuestsPresent()
        {
            _keyPointService.Save();
        }

        public void CancelTour()
        {
            _tourService.ChangeToursState(Tour, Tour.State.Interrupted);
            MarkLastVisitedKeyPoint();
            _keyPointService.Save();
        }

        public bool IsKeyPointSelected()
        {
            return SelectedKeyPoint != null;
        }

        public bool IsKeyPointBeingVisited()
        {
            return SelectedKeyPoint.CurrentState == KeyPoint.State.BeingVisited;
        }

        public bool IsKeyPointVisited()
        {
            return SelectedKeyPoint.CurrentState == KeyPoint.State.Visited;
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
            return _tourReservationService.GetReservationsByToursid(Tour.Id).Count == MarkedGuests.Count;
        }
    }
}
