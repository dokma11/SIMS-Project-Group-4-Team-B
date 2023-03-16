﻿using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.DAO
{
    public class AccommodationLocationsDAO: ISubject
    {
        private List<IObserver> _observers;

        private AccommodationLocationRepository _repository;
        private List<AccommodationLocation> _accommodationLocations;



        public AccommodationLocationsDAO()
        {
            _repository = new AccommodationLocationRepository();
            _accommodationLocations = _repository.Load();
            _observers = new List<IObserver>();
        }

        

        public int NextId()
        {
            return _accommodationLocations.Max(s => s.id) + 1;
        }

        public void Add(AccommodationLocation AccLoc)
        {
            AccLoc.id = NextId();
            _accommodationLocations.Add(AccLoc);
            _repository.Save(_accommodationLocations);
            NotifyObservers();
        }

        public void Remove(AccommodationLocation AccLoc)
        {
            _accommodationLocations.Remove(AccLoc);
            _repository.Save(_accommodationLocations);
            NotifyObservers();
        }

        public void Update(AccommodationLocation AccLoc)
        {
            int index = _accommodationLocations.FindIndex(p => p.id == AccLoc.id);
            if (index != -1)
            {
                _accommodationLocations[index] = AccLoc;
            }

            _repository.Save(_accommodationLocations);
            NotifyObservers();
        }

        public List<AccommodationLocation> GetAll()
        {
            return _accommodationLocations;
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
