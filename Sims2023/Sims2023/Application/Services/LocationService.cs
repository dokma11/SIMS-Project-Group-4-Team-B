using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class LocationService
    {
        private readonly ILocationCSVRepository _location;
        public LocationService()
        {
            _location = new LocationCSVRepository();
            //_location = Injection.Injector.CreateInstance<ILocationRepository>();
        }

        public List<Location> GetAll()
        {
            return _location.GetAll();
        }

        public Location GetById(int id)
        {
            return _location.GetById(id);
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
