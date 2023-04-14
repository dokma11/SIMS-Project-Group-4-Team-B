using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Sims2023.WPF.ViewModels
{
    public partial class LiveTourTrackingViewModel
    {
        public Tour Tour { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        private KeyPointService _keyPointService;
        private TourService _tourService;
        public ObservableCollection<KeyPoint> KeyPointsToDisplay { get; set; }
        public int firstKeyPointId = -1;
        public int lastKeyPointId = -1;
        public int lastVisitedKeyPointId = -1;
        public bool LastKeyPointVisited;

        public LiveTourTrackingViewModel(Tour tour, KeyPointService keyPointService, TourService tourService)
        {
            _keyPointService = keyPointService;
            _tourService = tourService;

            Tour = tour;
            _tourService.ChangeToursState(Tour, Tour.State.Started);

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

            foreach (var keyPoint in KeyPointsToDisplay)
            {
                if (keyPoint.Id < firstKeyPoint.Id)
                {
                    firstKeyPoint = keyPoint;
                }
            }

            firstKeyPoint.CurrentState = KeyPoint.State.BeingVisited;

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
            if (SelectedKeyPoint != null && SelectedKeyPoint.CurrentState == KeyPoint.State.NotVisited && SelectedKeyPoint.Id == lastVisitedKeyPointId + 1) //the latter disables key point skipping
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
            else if (SelectedKeyPoint != null && SelectedKeyPoint.CurrentState == KeyPoint.State.BeingVisited)
            {
                MessageBox.Show("Ne mozete oznaciti tacku na kojoj se trenutno nalazite");
            }
            else if (SelectedKeyPoint != null && SelectedKeyPoint.CurrentState == KeyPoint.State.Visited)
            {
                MessageBox.Show("Ne mozete oznaciti tacku koju ste prosli");
            }
            else
            {
                MessageBox.Show("Izaberite kljucnu tacku koju zelite da oznacite");
            }
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
    }
}
