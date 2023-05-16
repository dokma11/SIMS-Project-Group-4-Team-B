﻿using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;

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

        public Location GetById(int id)
        {
            return _location.GetById(id);
        }

        public void Create(Location location)
        {
            _location.CheckIdItShouldBeAdded(location);
        }

        public void CheckExistance(Location location)
        {
            _location.CheckExistance(location);
        }
        public void Subscribe(IObserver observer)
        {
            _location.Subscribe(observer);
        }
    }
}
