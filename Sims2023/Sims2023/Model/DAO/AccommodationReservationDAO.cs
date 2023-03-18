using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.DAO
{
    internal class AccommodationReservationDAO
    {
        private List<IObserver> _observers;

        private AccommodationReservationFileHandler _repository;
        private List<AccommodationReservation> _accommodationReservations;

        public AccommodationReservationDAO()
        {
            _repository = new AccommodationReservationFileHandler();
            _accommodationReservations = _repository.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_accommodationReservations.Count == 0) return 1;
            return _accommodationReservations.Max(s => s.Id) + 1;
        }

        public void Add(AccommodationReservation reservation)
        {
            reservation.Id = NextId();
            _accommodationReservations.Add(reservation);
            _repository.Save(_accommodationReservations);
            NotifyObservers();
        }

        public void Remove(AccommodationReservation reservation)
        {
            _accommodationReservations.Remove(reservation);
            _repository.Save(_accommodationReservations);
            NotifyObservers();
        }

        public void Update(AccommodationReservation reservation)
        {
            int index = _accommodationReservations.FindIndex(p => p.Id == reservation.Id);
            if (index != -1)
            {
                _accommodationReservations[index] = reservation;
            }

            _repository.Save(_accommodationReservations);
            NotifyObservers();
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservations;
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
