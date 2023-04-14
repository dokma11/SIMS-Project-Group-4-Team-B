using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Linq;
using Sims2023.Domain.RepositoryInterfaces;

namespace Sims2023.Repository
{
    public class LocationRepository: ILocationRepository
    {
        private readonly List<IObserver> _observers;
        private readonly List<Location> _locations;
        private readonly LocationFileHandler _fileHandler;
        public LocationRepository()
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
            if (_locations.Count == 0) return 1;
            return _locations.Max(l => l.Id) + 1;
        }

        public void CheckAdd(Location location)
        {
            List<Location> locations = _locations;

            if (locations.Count == 0)
            {
                Add(location);
            }
            else
            {
                if (!LocationExists(location, locations))
                {
                    Add(location);
                }
            }
        }

        public bool LocationExists(Location location, List<Location> locations)
        {
            foreach (var locationInstance in locations)
            {
                if (location.City == locationInstance.City && location.Country == locationInstance.Country)
                {
                    location.Id = locationInstance.Id;
                    return true;
                }
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
