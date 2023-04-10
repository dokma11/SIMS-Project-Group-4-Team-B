using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model.DAO
{
    public class AccommodationReservationReschedulingDAO
    {
        private List<IObserver> _observers;

        private AccommodationReservationReschedulingFileHandler _fileHandler;
        private List<AccommodationReservationRescheduling> _accommodationReservationReschedulings;

        public AccommodationReservationReschedulingDAO()
        {
            _fileHandler = new AccommodationReservationReschedulingFileHandler();
            _accommodationReservationReschedulings = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public AccommodationReservationRescheduling GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public int NextId()
        {
            if (_accommodationReservationReschedulings.Count == 0) return 1;
            return _accommodationReservationReschedulings.Max(s => s.Id) + 1;
        }

        public void Add(AccommodationReservationRescheduling reservationRescheduling)
        {
            reservationRescheduling.Id = NextId();
            _accommodationReservationReschedulings.Add(reservationRescheduling);
            _fileHandler.Save(_accommodationReservationReschedulings);
            NotifyObservers();
        }

        public void Update(AccommodationReservationRescheduling reservationRescheduling)
        {
            int index = _accommodationReservationReschedulings.FindIndex(p => p.Id == reservationRescheduling.Id);
            if (index != -1)
            {
                _accommodationReservationReschedulings[index] = reservationRescheduling;
            }

            _fileHandler.Save(_accommodationReservationReschedulings);
            NotifyObservers();
        }

        public void Remove(AccommodationReservationRescheduling reservationRescheduling)
        {
            _accommodationReservationReschedulings.Remove(reservationRescheduling);
            _fileHandler.Save(_accommodationReservationReschedulings);
            NotifyObservers();
        }

        public List<AccommodationReservationRescheduling> GetAll()
        {
            return _accommodationReservationReschedulings;
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
