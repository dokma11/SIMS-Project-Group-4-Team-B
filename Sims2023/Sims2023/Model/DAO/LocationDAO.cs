using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model.DAO
{
    public class LocationDAO
    {
        private List<IObserver> _observers;
        private List<Location> _locations;
        private LocationRepository _repository;
        public LocationDAO()
        {
            _repository = new LocationRepository();
            _locations = _repository.Load();
            _observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (_locations.Count == 0) return 1;
            return _locations.Max(l => l.Id) + 1;
        }
        public void Add(Location location)
        {
            location.Id = NextId();
            _locations.Add(location);
            _repository.Save(_locations);
            NotifyObservers();
        }
        public void Remove(Location location)
        { 
            _locations.Remove(location);
            _repository.Save(_locations);
            NotifyObservers();
        }
        public List<Location> GetAll()
        {
            return _locations;
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
