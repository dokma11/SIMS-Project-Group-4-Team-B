using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.Repository;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class LocationService
    {
        private readonly LocationRepository _location;
        public LocationService()
        {
            _location = new LocationRepository();
        }

        public List<Location> GetAllLocations()
        {
            return _location.GetAll();
        }

        public void Create(Location location)
        {
            _location.CheckAdd(location);
        }

        public void Delete(Location location)
        {
            _location.Remove(location);
        }

        public void Subscribe(IObserver observer)
        {
            _location.Subscribe(observer);
        }
    }
}
