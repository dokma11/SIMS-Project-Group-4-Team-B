using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Sims2023.Repository
{
    public class TourRepository
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

        public List<Tour> GetFinishedTours(User loggedInGuide)
        {
            return _tours.Where(t => (t.CurrentState == Tour.State.Finished || t.CurrentState == Tour.State.Interrupted)
                         && t.Guide.Id == loggedInGuide.Id).ToList();
        }

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
                return tours.Where(tour => tour.Start.Year.ToString() == year)
                             .OrderByDescending(tour => tour.AttendedGuestsNumber).FirstOrDefault();
            }
        }

        public string GetAgeStatistics(Tour selectedTour, string ageGroup)
        {
            List<TourReservation> reservations = new();
            reservations = _reservationFileHandler.Load();

            if (ageGroup == "young")
            {
                int young = reservations
                .Where(res => res.Tour.Id == selectedTour.Id && res.ConfirmedParticipation && res.User.Age <= 18)
                .Sum(res => res.GuestNumber);
                return young.ToString();
            }
            else if (ageGroup == "middleAged")
            {
                int middle = reservations
                .Where(res => res.Tour.Id == selectedTour.Id && res.ConfirmedParticipation && res.User.Age > 18 && res.User.Age <= 50)
                .Sum(res => res.GuestNumber);
                return middle.ToString();
            }
            else if(ageGroup == "old")
            {
                int old = reservations
                .Where(res => res.Tour.Id == selectedTour.Id && res.ConfirmedParticipation && res.User.Age > 50)
                .Sum(res => res.GuestNumber);
                return old.ToString();
            }
            else
            {
                return "ne znam";
            }
        }

        public string GetVoucherStatistics(Tour selectedTour, bool used)
        {
            List<TourReservation> reservations = new();
            reservations = _reservationFileHandler.Load();

            int usedCounter = reservations.Where(res => res.Tour.Id == selectedTour.Id)
                                          .Count(res => res.UsedVoucher && res.ConfirmedParticipation);
            int notUsedCounter = reservations.Where(res => res.Tour.Id == selectedTour.Id)
                                             .Count(res => !res.UsedVoucher && res.ConfirmedParticipation);

            double usedPercentage = (double)usedCounter / (usedCounter + notUsedCounter);
            double notUsedPercentage = (double)notUsedCounter / (usedCounter + notUsedCounter);

            if (used)
            {
                return usedPercentage.ToString("0.00");
            }
            else
            {
                return notUsedPercentage.ToString("0.00");
            }
        }

        public List<Tour> GetCreatedTours(User loggedInGuide)
        {
            List<Tour> ToursToDisplay = new();
            ToursToDisplay.AddRange(_tours.Where(tour => tour.CurrentState == Tour.State.Created && 
                                           tour.Guide.Id == loggedInGuide.Id));
            return ToursToDisplay;
        }

        public void ChangeToursState(Tour selectedTour, Tour.State state)
        {
            selectedTour.CurrentState = state;
        }
    }
}
