﻿using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class LocationService
    {
        private readonly ILocationCSVRepository _location;
        public LocationService()
        {
            _location = Injection.Injector.CreateInstance<ILocationCSVRepository>();
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

        public List<Location> GetPopularLocations(List<AccommodationReservation> reservations)
        {
             return _location.GetPopularLocations(reservations);
        }

        public List<Location> GetUnpopularLocations(List<AccommodationReservation> reservations, List<Location> locations)
        {
            return _location.GetUnpopularLocations(reservations, locations);
        }
    }
}
