using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sims2023.Repository
{
    public class TourWriteToCSVRepository : ITourWriteToCSVRepository
    {
        private List<IObserver> _observers;
        private List<Tour> _tours;
        private TourFileHandler _fileHandler;
        private TourReservationCSVRepository _reservations;
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

        public void CheckAddLocation(Tour tour, Location location, int newToursNumber, List<Location> locations)
        {
            int toursId = tour.Id - newToursNumber + 1;
            if (locations.Count == 0)
            {
                Enumerable.Range(0, newToursNumber).ToList().ForEach(_ => AddLocation(toursId++, location));
            }
            else
            {
                for (int i = 0; i < newToursNumber; i++, toursId++)
                {
                    if (locations.Any(l => l.City == location.City && l.Country == location.Country))
                    {
                        AddLocation(toursId, locations.First(l => l.City == location.City && l.Country == location.Country));
                    }
                    else
                    {
                        AddLocation(toursId, location);
                    }
                }
            }
        }

        public void AddLocationsToTour(ObservableCollection<Location> locations, ObservableCollection<Tour> tours)//new,delete later
        {
            foreach (var tour in tours)
            {
                var location = locations.FirstOrDefault(l => l.Id == tour.Location.Id);
                if (location != null)
                {
                    tour.Location.City = location.City;
                    tour.Location.Country = location.Country;
                }
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

        public void CalculateAttendedGuestsNumber(User loggedInGuide)
        {
            _reservations = new TourReservationCSVRepository();
            List<TourReservation> reservations = _reservations.GetAll();

            _tours = _fileHandler.Load();

            foreach (var tour in _tours.Where(t => (t.CurrentState == ToursState.Finished || t.CurrentState == ToursState.Interrupted)
                         && t.Guide.Id == loggedInGuide.Id).ToList())
            {
                tour.AttendedGuestsNumber = reservations.Where(res => res.Tour.Id == tour.Id && res.ConfirmedParticipation)
                                                        .Sum(res => res.GuestNumber);
            }

            Save();
        }

        public void UpdateState(Tour selectedTour, ToursState state)
        {
            selectedTour.CurrentState = state;
            Save(); 
        }

        public void SetLanguage(Tour selectedTour, ToursLanguage language)
        {
            selectedTour.GuideLanguage = language;
        }
    }
}
