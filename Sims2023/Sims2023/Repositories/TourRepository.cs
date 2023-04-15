using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public void Add(Tour tour, List<DateTime> dateTimes, Location location, User loggedInGuide)
        {
            foreach (var date in dateTimes)
            {
                _tours = _fileHandler.Load();
                tour.Id = NextId();
                tour.Guide = loggedInGuide;
                tour.Start = date;
                AddToursLocation(tour.Id, location);
                tour.AvailableSpace = tour.MaxGuestNumber;
                _tours.Add(tour);
                _fileHandler.Save(_tours);
                NotifyObservers();
            }
        }
        public void Update(Tour tour)//new method and deleted addedited
        {
            int index = _tours.FindIndex(p => p.Id == tour.Id);
            if (index != -1)
            {
                _tours[index] = tour;
            }

            _fileHandler.Save(_tours);
            NotifyObservers();
        }
        

        public bool CanRateTour(Tour tour)//new for guest2
        {
            return tour.CurrentState == Tour.State.Finished;
        }

        public bool CanSeeTour(Tour tour)//new for guest2
        {
            return tour.CurrentState == Tour.State.Started;
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

        public void CheckIfLocationExists(int newToursNumber, List<Location> locations, Location location, int toursId)
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

        public void AddLocationsToTour(ObservableCollection<Location> locations, ObservableCollection<Tour> tours)//new,delete later
        {
            foreach (var tour in tours)
            {
                var location = locations.FirstOrDefault(l => l.Id == tour.LocationId);
                if (location != null)
                {
                    tour.City = location.City;
                    tour.Country = location.Country;
                }
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

        public List<Tour> GetAvailable()//new method for guest2
        {
            List<Tour> available = new List<Tour>();
            foreach(var tourInstance in _tours)
            {
                if (tourInstance.CurrentState == Tour.State.Created)
                {
                    available.Add(tourInstance);
                }
            }
            return available;
        }

        public List<Tour> GetAlternative(int reserveSpace, Tour tour)//new method for guest2
        {
            var alternativeTours = _tours
                .Where(tour => tour.LocationId == tour.LocationId && tour.AvailableSpace >= reserveSpace && tour.CurrentState==Tour.State.Created)
                .ToList();

            return alternativeTours;
        }

        public Tour GetById(int id)
        {
            return _fileHandler.GetById(id);
        }
        public void UpdateAvailableSpace(int reservedSpace, Tour tour)//new method for guest2
        {
            tour.AvailableSpace -= reservedSpace;
            Update(tour);
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
