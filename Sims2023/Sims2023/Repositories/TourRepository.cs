using Sims2023.Domain.Models;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repository
{
    public class TourRepository
    {
        private List<IObserver> _observers;
        private List<Tour> _tours;
        private TourFileHandler _fileHandler;
        public TourRepository()
        {
            _fileHandler = new TourFileHandler();
            _tours = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_tours.Count == 0) return 1;
            return _tours.Max(t => t.Id) + 1;
        }

        public void Add(Tour tour, List<DateTime> dateTimes, Location location)
        {
            foreach (var date in dateTimes)
            {
                _tours = _fileHandler.Load();
                tour.Id = NextId();
                tour.Start = date;
                AddToursLocation(tour.Id, location);
                tour.AvailableSpace = tour.MaxGuestNumber;
                _tours.Add(tour);
                _fileHandler.Save(_tours);
                NotifyObservers();
            }
        }
        public void AddEdited(Tour tour)
        {
            _tours.Add(tour);
            _fileHandler.Save(_tours); ;
            NotifyObservers();
        }

        public void AddToursLocation(int toursId, Location location)
        {
            foreach (var tour in _tours)
            {
                if (tour.Id == toursId)
                {
                    tour.LocationId = location.Id;
                    _fileHandler.Save(_tours);
                    NotifyObservers();
                    break;
                }
            }
        }

        private void CheckIfLocationExists(int newToursNumber, List<Location> locations, Location location, int toursId)
        {
            int counter = 0;
            for (int i = 0; i < newToursNumber; i++)
            {
                foreach (var locationInstance in locations)
                {
                    //if location exists just add the already existing one
                    if (location.City == locationInstance.City && location.Country == locationInstance.Country)
                    {
                        counter++;
                        AddToursLocation(toursId, locationInstance);
                        break;
                    }
                }
                //if it doesn't exist add the newly created one
                if (counter == 0)
                {
                    AddToursLocation(toursId, location);
                }
                toursId++;
            }
        }

        public void CheckAddToursLocation(Tour tour, Location location, int newToursNumber, List<Location> locations)
        {
            int toursId = tour.Id - newToursNumber + 1;
            if (locations.Count == 0)
            {
                for (int i = 0; i < newToursNumber; i++)
                {
                    AddToursLocation(toursId, location);
                    toursId++;
                }
            }
            else
            {
                CheckIfLocationExists(newToursNumber, locations, location, toursId);
            }
        }

        public void AddToursKeyPoints(string keyPointsString, int firstToursId)
        {
            foreach (var tourInstance in _tours)
            {
                if (tourInstance.Id == firstToursId)
                {
                    tourInstance.KeyPointsString = keyPointsString;
                    firstToursId++;
                    _fileHandler.Save(_tours);
                    NotifyObservers();
                }
            }
        }

        public void Remove(Tour tour)
        {
            _tours.Remove(tour);
            _fileHandler.Save(_tours);
            NotifyObservers();
        }

        public List<Tour> GetAll()
        {
            return _tours;
        }

        public Tour GetById(int id)
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
            _fileHandler.Save(_tours);
        }
    }
}
