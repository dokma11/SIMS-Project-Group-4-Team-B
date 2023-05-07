using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class KeyPointCSVRepository : IKeyPointCSVRepository
    {
        private List<IObserver> _observers;
        private List<KeyPoint> _keyPoints;
        private KeyPointFileHandler _fileHandler;

        public KeyPointCSVRepository()
        {
            _fileHandler = new KeyPointFileHandler();
            _keyPoints = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            return _keyPoints.Count == 0 ? 1 : _keyPoints.Max(t => t.Id) + 1;
        }

        public void Add(KeyPoint keyPoint, List<string> keyPointNames, int toursId, int newToursNumber)
        {
            for (int i = 1; i <= newToursNumber; i++)
            {
                foreach (string keyPointName in keyPointNames)
                {
                    _keyPoints = _fileHandler.Load();
                    keyPoint.Id = NextId();
                    TourService tourService = new();
                    keyPoint.Tour = tourService.GetById(toursId);
                    keyPoint.Name = keyPointName;
                    keyPoint.CurrentState = KeyPointsState.NotVisited;
                    _keyPoints.Add(keyPoint);
                    _fileHandler.Save(_keyPoints);
                    NotifyObservers();
                }
                toursId++;
            }
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

        public KeyPoint GetCurrentKeyPoint(Tour tour)//new method to guest2 display current key point
        {
            foreach (var keyPoint in _keyPoints)
            {
                if (keyPoint.CurrentState == KeyPointsState.BeingVisited && keyPoint.Tour.Id == tour.Id)
                {
                    return keyPoint;
                }
            }

            return null;
        }


        public void Save()
        {
            _fileHandler.Save(_keyPoints);
        }

        public List<KeyPoint> GetByToursId(int id)
        {
            return _keyPoints.Where(k => k.Tour.Id == id).ToList();
        }

        public void ChangeState(KeyPoint keyPoint, KeyPointsState state)
        {
            keyPoint.CurrentState = state;
        }

        public void AddGuestsId(KeyPoint selectedKeyPoint, int guestsId)
        {
            var keyPoint = _keyPoints.FirstOrDefault(k => k.Id == selectedKeyPoint.Id);
            keyPoint?.PresentGuestsIds.Add(guestsId);
            //initialize the start of the string if necessary
            if (keyPoint?.PresentGuestsIdsString.Length == 0)
            {
                keyPoint.PresentGuestsIdsString = guestsId.ToString();
            }
            else
            {
                keyPoint.PresentGuestsIdsString += "," + guestsId.ToString();
            }
            keyPoint.PresentGuestsNumber++;
        }

        public KeyPoint GetWhereGuestJoined(Tour selectedTour, User loggedInGuest)
        {
            return _keyPoints.FirstOrDefault(keyPoint => keyPoint.Tour.Id == selectedTour.Id &&
                              keyPoint.PresentGuestsIds.Contains(loggedInGuest.Id));
        }
    }
}
