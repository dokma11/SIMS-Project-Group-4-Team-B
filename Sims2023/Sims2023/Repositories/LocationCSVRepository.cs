using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class LocationCSVRepository : ILocationCSVRepository
    {
        private readonly List<IObserver> _observers;
        private readonly List<Location> _locations;
        private readonly LocationFileHandler _fileHandler;

        public LocationCSVRepository()
        {
            _fileHandler = new LocationFileHandler();
            _locations = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public Location GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public int GetIdByLocation(string city, string country)
        {
            foreach (Location location in GetAll())
            {
                if (location.Country == country && location.City == city) return location.Id;
            }
            return 0;
        }

        public int NextId()
        {
            return _locations.Count == 0 ? 1 : _locations.Max(t => t.Id) + 1;
        }

        public void CheckAdd(Location location)
        {
            if (!_locations.Contains(location))
            {
                Add(location);
            }
        }

        public bool LocationExists(Location location, List<Location> locations)
        {
            var matchingLocation = locations.FirstOrDefault(l => l.City == location.City && l.Country == location.Country);
            if (matchingLocation != null)
            {
                location.Id = matchingLocation.Id;
                return true;
            }
            return false;
        }

        public void Add(Location location)
        {
            location.Id = NextId();
            _locations.Add(location);
            _fileHandler.Save(_locations);
            NotifyObservers();
        }

        public void Remove(Location location)
        {
            _locations.Remove(location);
            _fileHandler.Save(_locations);
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
