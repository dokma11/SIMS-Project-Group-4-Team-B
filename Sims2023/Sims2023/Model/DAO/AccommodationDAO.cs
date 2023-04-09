using Sims2023.Model;
using Sims2023.Model.DAO;
using Sims2023.Observer;
using Sims2023.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.DAO
{
    class AccommodationDAO : ISubject
    {

        private List<IObserver> _observers;
        private AccommodationFileHandler _fileHandler;
        private List<Accommodation> _accommodations;
        
     
        public AccommodationDAO()
        {
            _fileHandler = new AccommodationFileHandler();
            _accommodations = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public Accommodation GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public int NextId()
        {
            if (_accommodations.Count == 0) return 1;
            return _accommodations.Max(s => s.Id) + 1;
        }

        public void Add(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations.Add(accommodation);
            _fileHandler.Save(_accommodations);
            NotifyObservers();
        }

        public void Remove(Accommodation accommodation)
        {
            _accommodations.Remove(accommodation);
            _fileHandler.Save(_accommodations);
            NotifyObservers();
        }

        public void Update(Accommodation accommodation)
        {
            int index = _accommodations.FindIndex(p => p.Id == accommodation.Id);
            if (index != -1)
            {
                _accommodations[index] = accommodation;
            }

            _fileHandler.Save(_accommodations);
            NotifyObservers();
        }
       public List<Accommodation> GetAll()
        {
            return _accommodations;
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
