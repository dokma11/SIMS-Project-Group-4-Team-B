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
    class AccommodationDAO : ISubject
    {

        private List<IObserver> _observers;

        private AccommodationRepository _repository;
        private List<Accommodation> _accommodations;



        public AccommodationDAO()
        {
            _repository = new AccommodationRepository();
            _accommodations = _repository.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            return _accommodations.Max(s => s.Id) + 1;
        }

        public void Add(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations.Add(accommodation);
            _repository.Save(_accommodations);
            NotifyObservers();
        }

        public void Remove(Accommodation accommodation)
        {
            _accommodations.Remove(accommodation);
            _repository.Save(_accommodations);
            NotifyObservers();
        }

        public void Update(Accommodation accommodation)
        {
            int index = _accommodations.FindIndex(p => p.Id == accommodation.Id);
            if (index != -1)
            {
                _accommodations[index] = accommodation;
            }

            _repository.Save(_accommodations);
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
