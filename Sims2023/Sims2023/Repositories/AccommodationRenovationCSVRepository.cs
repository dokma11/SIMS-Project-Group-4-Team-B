using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repositories
{
    internal class AccommodationRenovationCSVRepository : ISubject
    {
        private List<IObserver> _observers;

        private AccommodationRenovationFileHandler _fileHandler;
        private List<AccommodationRenovation> _renovations;


        public AccommodationRenovationCSVRepository()
        {
            _fileHandler = new AccommodationRenovationFileHandler();
            _renovations = _fileHandler.Load();
            _observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (_renovations.Count == 0) return 1;
            return _renovations.Max(s => s.Id) + 1;
        }

        public void Add(AccommodationRenovation grade)
        {
            grade.Id = NextId();
            _renovations.Add(grade);
            _fileHandler.Save(_renovations);
            NotifyObservers();
        }



        public void Remove(AccommodationRenovation renovation)
        {
            _renovations.Remove(renovation);
            _fileHandler.Save(_renovations);
            NotifyObservers();
        }

        public void Update(AccommodationRenovation renovation)
        {
            int index = _renovations.FindIndex(p => p.Id == renovation.Id);
            if (index != -1)
            {
                _renovations[index] = renovation;
            }

            _fileHandler.Save(_renovations);
            NotifyObservers();
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _renovations;
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
