using Sims2023.FileHandler;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Linq;
using Sims2023.Domain.Models;

namespace Sims2023.Repositories
{
    public class TourReservationRepository
    {
        private List<IObserver> _observers;

        private TourReservationFileHandler _fileHandler;
        private List<TourReservation> _tourReservations;

        public TourReservationRepository()
        {
            _fileHandler = new TourReservationFileHandler();
            _tourReservations = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if(_tourReservations.Count == 0)
            {
                return 1;
            }
            return _tourReservations.Max(s => s.Id) + 1;
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
            return _tourReservations.Where(r=>r.ShouldConfirmParticipation==true).ToList();
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
                .Where(r => r.User.Id == user.Id && (r.Tour.CurrentState != Tour.State.Started || r.ConfirmedParticipation))
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
    }
}
