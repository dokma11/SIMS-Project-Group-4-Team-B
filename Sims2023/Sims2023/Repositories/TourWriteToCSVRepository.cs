using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repository
{
    public class TourWriteToCSVRepository : ITourWriteToCSVRepository
    {
        private List<IObserver> _observers;
        private List<Tour> _tours;
        private TourFileHandler _fileHandler;

        public TourWriteToCSVRepository()
        {
            _fileHandler = new TourFileHandler();
            _tours = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            return _tours.Count == 0 ? 1 : _tours.Max(t => t.Id) + 1;
        }

        public void Add(Tour tour, List<DateTime> dateTimes, Location location, User loggedInGuide)
        {
            foreach (var date in dateTimes)
            {
                _tours = _fileHandler.Load();
                tour.Id = NextId();
                tour.Guide = loggedInGuide;
                tour.Start = date;
                AddLocation(tour.Id, location);
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


        public void AddLocation(int toursId, Location location)
        {
            var tour = _tours.FirstOrDefault(t => t.Id == toursId);
            if (tour != null)
            {
                tour.Location.Id = location.Id;
                _fileHandler.Save(_tours);
                NotifyObservers();
            }
        }

        public void DecideLocationToAdd(Tour tour, Location location, int newToursNumber, List<Location> locations)
        {
            int toursId = tour.Id - newToursNumber + 1;

            for (int i = 0; i < newToursNumber; i++, toursId++)
            {
                Location selectedLocation = locations.FirstOrDefault(l => l.City == location.City && l.Country == location.Country);
                AddLocation(toursId, selectedLocation ?? location);
            }
        }

        public void AddKeyPoints(string keyPointsString, int firstToursId)
        {
            foreach (var tourInstance in _tours)
            {
                if (tourInstance.Id == firstToursId)
                {
                    tourInstance.KeyPoints = keyPointsString;
                    firstToursId++;
                    _fileHandler.Save(_tours);
                    NotifyObservers();
                }
            }
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

        public void UpdateState(Tour selectedTour, ToursState state)
        {
            _tours = _fileHandler.Load();
            var tourToUpdate = _tours.FirstOrDefault(t => t.Id == selectedTour.Id);
            if (tourToUpdate != null)
            {
                tourToUpdate.CurrentState = state;
                Save();
            }
        }

        public void SetLanguage(Tour selectedTour, ToursLanguage language)
        {
            selectedTour.GuideLanguage = language;
        }

        public void CancelAll(User loggedInGuide)
        {
            _tours.Where(t => t.CurrentState == ToursState.Created && t.Guide.Id == loggedInGuide.Id).ToList()
                  .ForEach(t => t.CurrentState = ToursState.Cancelled);
            Save();
        }
    }
}
