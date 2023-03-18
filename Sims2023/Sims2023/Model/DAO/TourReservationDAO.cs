using Sims2023.FileHandler;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Model.DAO
{
    public class TourReservationDAO
    {
        private List<IObserver> _observers;

        private TourReservationFileHandler _fileHandler;
        private List<TourReservation> _tourReservations;

        public TourReservationDAO()
        {
            _fileHandler = new TourReservationFileHandler();
            _tourReservations = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
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
