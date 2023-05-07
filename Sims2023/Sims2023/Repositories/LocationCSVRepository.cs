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

        public void CheckIdItShouldBeAdded(Location location)
        {
            if (!_locations.Contains(location))
            {
                Add(location);
            }
        }

        public void CheckExistance(Location location)
        {
            foreach(Location Location in _locations)
            {
                if(Location.City==location.City && Location.Country == location.Country)
                {
                    location.Id = Location.Id;
                    return;
                }
            }
            Add(location);
        }

        
        public void Add(Location location)
        {
            location.Id = NextId();
            _locations.Add(location);
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

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
