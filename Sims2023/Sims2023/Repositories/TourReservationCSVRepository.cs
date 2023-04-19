using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
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

        public List<Tour> GetByUser(User user)//new method for guest2
        {
            return _tourReservations
                .Where(r => r.User.Id == user.Id && (r.Tour.CurrentState != ToursState.Started || r.ConfirmedParticipation))
                .Select(r => r.Tour)
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

        public List<TourReservation> GetReservationsByToursId(int id)
        {
            return _tourReservations.Where(reservation => reservation.Tour.Id == id).ToList();
        }

        public string GetAgeStatistics(Tour selectedTour, string ageGroup)
        {
            if (ageGroup == "young")
            {
                int young = _tourReservations
                .Where(res => res.Tour.Id == selectedTour.Id && res.ConfirmedParticipation && res.User.Age <= 18)
                .Sum(res => res.GuestNumber);
                return young.ToString();
            }
            else if (ageGroup == "middleAged")
            {
                int middle = _tourReservations
                .Where(res => res.Tour.Id == selectedTour.Id && res.ConfirmedParticipation && res.User.Age > 18 && res.User.Age <= 50)
                .Sum(res => res.GuestNumber);
                return middle.ToString();
            }
            else if (ageGroup == "old")
            {
                int old = _tourReservations
                .Where(res => res.Tour.Id == selectedTour.Id && res.ConfirmedParticipation && res.User.Age > 50)
                .Sum(res => res.GuestNumber);
                return old.ToString();
            }
            else
            {
                return "Nije dobra godina unesena";
            }
        }

        public string GetVoucherStatistics(Tour selectedTour, bool used)
        {
            int usedCounter = _tourReservations.Where(res => res.Tour.Id == selectedTour.Id)
                                          .Count(res => res.UsedVoucher && res.ConfirmedParticipation);
            int notUsedCounter = _tourReservations.Where(res => res.Tour.Id == selectedTour.Id)
                                             .Count(res => !res.UsedVoucher && res.ConfirmedParticipation);

            double usedPercentage = (double)usedCounter / (usedCounter + notUsedCounter);
            double notUsedPercentage = (double)notUsedCounter / (usedCounter + notUsedCounter);

            if (used)
            {
                return usedPercentage.ToString("P1");
            }
            else
            {
                return notUsedPercentage.ToString("P1");
            }
        }
    }
}
