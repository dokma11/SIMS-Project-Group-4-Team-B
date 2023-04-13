using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.Repositories
{
    public class AccommodationCancellationRepository
    {
        private List<IObserver> _observers;

        private AccommodationCancellationFileHandler _fileHandler;
        private List<AccommodationCancellation> _accommodationCancellations;

        public AccommodationCancellationRepository()
        {
            _fileHandler = new AccommodationCancellationFileHandler();
            _accommodationCancellations = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public AccommodationCancellation GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public int NextId()
        {
            if (_accommodationCancellations.Count == 0) return 1;
            return _accommodationCancellations.Max(s => s.Id) + 1;
        }

        public void Add(AccommodationCancellation accommodationCancellation)
        {
            accommodationCancellation.Id = NextId();
            _accommodationCancellations.Add(accommodationCancellation);
            _fileHandler.Save(_accommodationCancellations);
            NotifyObservers();
        }

        public void Remove(AccommodationCancellation accommodationCancellation)
        {
            _accommodationCancellations.Remove(accommodationCancellation);
            _fileHandler.Save(_accommodationCancellations);
            NotifyObservers();
        }

        public void Update(AccommodationCancellation accommodationCancellation)
        {
            int index = _accommodationCancellations.FindIndex(p => p.Id == accommodationCancellation.Id);
            if (index != -1)
            {
                _accommodationCancellations[index] = accommodationCancellation;
            }

            _fileHandler.Save(_accommodationCancellations);
            NotifyObservers();
        }

        public List<AccommodationCancellation> GetAll()
        {
            return _accommodationCancellations;
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
