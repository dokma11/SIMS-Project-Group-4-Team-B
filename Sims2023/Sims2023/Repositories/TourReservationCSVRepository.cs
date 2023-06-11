using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class TourReservationCSVRepository : ITourReservationCSVRepository
    {
        private List<IObserver> _observers;
        private TourReservationFileHandler _fileHandler;
        private List<TourReservation> _tourReservations;

        public TourReservationCSVRepository()
        {
            _fileHandler = new TourReservationFileHandler();
            _tourReservations = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            return _tourReservations.Count == 0 ? 1 : _tourReservations.Max(t => t.Id) + 1;
        }

        public void Add(TourReservation reservation)
        {
            reservation.Id = NextId();
            _tourReservations.Add(reservation);
            _fileHandler.Save(_tourReservations);
            NotifyObservers();
        }

        public void Remove(TourReservation reservation)
        {
            _tourReservations.Remove(reservation);
            _fileHandler.Save(_tourReservations);
            NotifyObservers();
        }

        public void Update(TourReservation reservation)
        {
            int index = _tourReservations.FindIndex(p => p.Id == reservation.Id);
            if (index != -1)
            {
                _tourReservations[index] = reservation;
            }

            _fileHandler.Save(_tourReservations);
            NotifyObservers();
        }

        public List<TourReservation> GetAll()
        {
            return _tourReservations;
        }

        public List<TourReservation> GetNotConfirmedParticipation()//new method for guest2
        {
            return _tourReservations.Where(r => r.ShouldConfirmParticipation == true).ToList();
        }

        public void ConfirmReservation(TourReservation tourReservation, bool confirmed)//new method for guest2
        {
            tourReservation.ShouldConfirmParticipation = false;
            tourReservation.ConfirmedParticipation = confirmed;
            Update(tourReservation);
        }

        public bool CountReservationsByUser(TourReservation tourReservation)//new method for guest2
        {
            int countReservation = _tourReservations
                .Count(r => r.User.Id == tourReservation.User.Id && r.ReservationTime.Year == tourReservation.ReservationTime.Year);

            return countReservation > 0 && countReservation % 5 == 0;
        }

        public List<TourReservation> GetByUser(User user)//new method for guest2
        {
            return _tourReservations
                .Where(r => r.User.Id == user.Id && (r.Tour.CurrentState != ToursState.Started || r.ConfirmedParticipation))
                .ToList();
        }

        public void Save()
        {
            _fileHandler.Save(_tourReservations);
            NotifyObservers();
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

        public List<TourReservation> GetByToursId(int id)
        {
            return _tourReservations.Where(reservation => reservation.Tour.Id == id).ToList();
        }

        public int GetAgeStatistics(Tour selectedTour, string ageGroup)
        {
            if (ageGroup == "young")
            {
                return _tourReservations
                .Where(res => res.Tour.Id == selectedTour.Id && res.ConfirmedParticipation && res.User.Age <= 18)
                .Sum(res => res.GuestNumber);
            }
            else if (ageGroup == "middleAged")
            {
                return _tourReservations
                .Where(res => res.Tour.Id == selectedTour.Id && res.ConfirmedParticipation && res.User.Age > 18 && res.User.Age <= 50)
                .Sum(res => res.GuestNumber);
            }
            else if (ageGroup == "old")
            {
                return _tourReservations
                .Where(res => res.Tour.Id == selectedTour.Id && res.ConfirmedParticipation && res.User.Age > 50)
                .Sum(res => res.GuestNumber);
            }
            else
            {
                return 0;
            }
        }

        public int GetVoucherStatistics(Tour selectedTour, bool used)
        {
            return _tourReservations.Where(res => res.Tour.Id == selectedTour.Id && res.ConfirmedParticipation)
                                           .Sum(res => used ? (res.UsedVoucher ? 1 : 0) : (!res.UsedVoucher ? 1 : 0));
        }

        public void CalculateAttendedGuestsNumber(User loggedInGuide, List<Tour> tours)
        {
            foreach (var tour in tours.Where(t => (t.CurrentState == ToursState.Finished || t.CurrentState == ToursState.Interrupted)
                         && t.Guide.Id == loggedInGuide.Id).ToList())
            {
                tour.AttendedGuestsNumber = _tourReservations.Where(res => res.Tour.Id == tour.Id && res.ConfirmedParticipation)
                                                        .Sum(res => res.GuestNumber);
            }

            Save();
        }

        public List<User> GetGuestsWithReservations(KeyPoint keyPoint, List<User> markedGuests, List<User> users)
        {
            List<User?> guests = _tourReservations.Where(reservation => reservation.Tour.Id == keyPoint.Tour.Id)
                       .Select(reservation =>
                        {
                            User? user = users.FirstOrDefault(u => u.Id == reservation.User.Id);
                            if (user != null && CheckIfGuestMarked(user, keyPoint, markedGuests))
                            {
                                return user;
                            }
                            return null;
                        })
                       .Where(user => user != null)
                       .ToList();

            return guests.Where(user => user != null).Select(user => user!).ToList();
        }

        private bool CheckIfGuestMarked(User guest, KeyPoint keyPoint, List<User> markedGuests)
        {
            return !keyPoint.PresentGuestsIds.Contains(guest.Id) &&
                !markedGuests.Any(markedGuest => markedGuest.Id == guest.Id);
        }

        public List<TourReservation> GetUsersTours(User user)
        {
            List<TourReservation> allUserTours = new();
            foreach (var reservation in _tourReservations)
            {
                if (reservation.User.Id == user.Id && reservation.ConfirmedParticipation)
                {
                    allUserTours.Add(reservation);
                }
            }
            return allUserTours;
        }

        public List<TourReservation> GetReportsTourReservation(User user,DateTime start,DateTime end)
        {
            return _tourReservations.Where(t=> t.User.Id == user.Id && t.Tour.Start>start && t.Tour.Start < end).ToList();
        }
    }
}
