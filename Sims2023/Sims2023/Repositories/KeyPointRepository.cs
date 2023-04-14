using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repository
{
    public class KeyPointRepository
    {
        private List<IObserver> _observers;
        private List<KeyPoint> _keyPoints;
        private KeyPointFileHandler _fileHandler;
        private TourReviewRepository _tourReviews;
        public KeyPointRepository()
        {
            _fileHandler = new KeyPointFileHandler();
            _keyPoints = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_keyPoints.Count == 0) return 1;
            return _keyPoints.Max(k => k.Id) + 1;
        }

        public void Add(KeyPoint keyPoint, List<string> keyPointNames, int toursId, int newToursNumber)
        {
            for (int i = 1; i <= newToursNumber; i++)
            {
                foreach (string keyPointName in keyPointNames)
                {
                    _keyPoints = _fileHandler.Load();
                    keyPoint.Id = NextId();
                    TourService tourController = new TourService();
                    keyPoint.Tour = tourController.GetById(toursId);
                    keyPoint.Name = keyPointName;
                    keyPoint.CurrentState = KeyPoint.State.NotVisited;
                    _keyPoints.Add(keyPoint);
                    _fileHandler.Save(_keyPoints);
                    NotifyObservers();
                }
                toursId++;
            }
        }

        public void Remove(KeyPoint keyPoint)
        {
            _keyPoints.Remove(keyPoint);
            _fileHandler.Save(_keyPoints);
            NotifyObservers();
        }

        public List<KeyPoint> GetAll()
        {
            return _keyPoints;
        }

        public KeyPoint GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Save()
        {
            _fileHandler.Save(_keyPoints);
        }

        public void GetKeyPointWhereGuestJoined(Tour selectedTour)
        {
            _tourReviews = new();
            foreach(var tourReview in _tourReviews.GetAll())
            {
                var keyPoint = _keyPoints.Where(k => k.Tour.Id == selectedTour.Id)
                                         .FirstOrDefault(k => k.ShowedGuestsIds.Contains(tourReview.Guest.Id));
                if(keyPoint != null)
                {
                    tourReview.KeyPointJoined = keyPoint;
                    _tourReviews.Save();
                }
            }
        }

        public List<KeyPoint> GetByToursId(int id)
        {
            return _keyPoints.Where(k => k.Tour.Id == id).ToList();
        }

        public void ChangeKeyPointsState(KeyPoint keyPoint, KeyPoint.State state)
        {
            keyPoint.CurrentState = state;
        }
    }
}
