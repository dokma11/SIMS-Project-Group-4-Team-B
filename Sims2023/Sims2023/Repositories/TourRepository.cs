using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repository
{
    public class TourRepository: ITourRepository
    {
        private List<IObserver> _observers;
        private List<Tour> _tours;
        private TourFileHandler _fileHandler;
        private TourReservationFileHandler _reservationFileHandler;
        public TourRepository()
        {
            _fileHandler = new TourFileHandler();
            _reservationFileHandler = new TourReservationFileHandler();
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
            var tour = _tours.FirstOrDefault(t => t.Id == toursId);
            if (tour != null)
            {
                tour.LocationId = location.Id;
                _fileHandler.Save(_tours);
                NotifyObservers();
            }
        }

        public void CheckAddToursLocation(Tour tour, Location location, int newToursNumber, List<Location> locations)
        {
            int toursId = tour.Id - newToursNumber + 1;
            if (locations.Count == 0)
            {
                Enumerable.Range(0, newToursNumber).ToList().ForEach(_ => AddToursLocation(toursId++, location));
            }
            else
            {
                for (int i = 0; i < newToursNumber; i++, toursId++)
                {
                    if (locations.Any(l => l.City == location.City && l.Country == location.Country))
                    {
                        AddToursLocation(toursId, locations.First(l => l.City == location.City && l.Country == location.Country));
                    }
                    else
                    {
                        AddToursLocation(toursId, location);
                    }
                }
            }
        }

        public void AddToursKeyPoints(string keyPointsString, int firstToursId)
        {
            var tour = _tours.FirstOrDefault(t => t.Id == firstToursId);
            if (tour != null)
            {
                tour.KeyPointsString = keyPointsString;
                _fileHandler.Save(_tours);
                NotifyObservers();
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

        public List<Tour> GetFinishedTours(User loggedInGuide)
        {
            return _tours.Where(t => (t.CurrentState == Tour.State.Finished || t.CurrentState == Tour.State.Interrupted)
                         && t.Guide.Id == loggedInGuide.Id).ToList();
        }

        //ovo mozda premestiti u rezervacije
        public void GetAttendedGuestsNumber(User loggedInGuide)
        {
            List<TourReservation> reservations = new();
            reservations = _reservationFileHandler.Load();

            foreach (var tour in GetFinishedTours(loggedInGuide))
            {
                tour.AttendedGuestsNumber = reservations.Where(res => res.Tour.Id == tour.Id && res.ConfirmedParticipation)
                                                        .Sum(res => res.GuestNumber);
            }
        }

        public Tour GetTheMostVisitedTour(User loggedInGuide, string year)
        {
            Tour ret = new();
            var tours = _tours.Where(tour => tour.Guide.Id == loggedInGuide.Id &&
                        (tour.CurrentState == Tour.State.Finished || tour.CurrentState == Tour.State.Interrupted));
            if (year == "Svih vremena")
            {
                return tours.OrderByDescending(tour => tour.AttendedGuestsNumber).FirstOrDefault();
            }
            else
            {
                if (tours.Where(tour => tour.Start.Year.ToString() == year) != null)
                {
                    return tours.Where(tour => tour.Start.Year.ToString() == year)
                             .OrderByDescending(tour => tour.AttendedGuestsNumber).FirstOrDefault();
                }
                else
                {
                    return ret;
                }
            }
        }

        public List<Tour> GetCreatedTours(User loggedInGuide)
        {
            return _tours.Where(tour => tour.CurrentState == Tour.State.Created && tour.Guide.Id == loggedInGuide.Id).ToList();
        }

        public void ChangeToursState(Tour selectedTour, Tour.State state)
        {
            selectedTour.CurrentState = state;
        }

        public void SetToursLanguage(Tour selectedTour, Tour.Language language)
        {
            selectedTour.GuideLanguage = language;
        }
    }
}
