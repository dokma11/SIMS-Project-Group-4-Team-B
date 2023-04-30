using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.Repository;
using System.Collections.Generic;
using System.Linq;
using Sims2023.Domain.RepositoryInterfaces;
using System;

namespace Sims2023.Repositories
{
    class AccommodationCSVRepository : ISubject, IAccommodationCSVRepository
    {

        private List<IObserver> _observers;
        private AccommodationFileHandler _fileHandler;
        private List<Accommodation> _accommodations;


        public AccommodationCSVRepository()
        {
            _fileHandler = new AccommodationFileHandler();
            _accommodations = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public Accommodation GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public int NextId()
        {
            if (_accommodations.Count == 0) return 1;
            return _accommodations.Max(s => s.Id) + 1;
        }

        public void Add(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations.Add(accommodation);
            _fileHandler.Save(_accommodations);
            NotifyObservers();
        }

        public void Remove(Accommodation accommodation)
        {
            _accommodations.Remove(accommodation);
            _fileHandler.Save(_accommodations);
            NotifyObservers();
        }

        public void Update(Accommodation accommodation)
        {
            int index = _accommodations.FindIndex(p => p.Id == accommodation.Id);
            if (index != -1)
            {
                _accommodations[index] = accommodation;
            }

            _fileHandler.Save(_accommodations);
            NotifyObservers();
        }
        public List<Accommodation> GetAll()
        {
            return _accommodations;
        }


        public List<Accommodation> GetOwnerAccommodations(List<Accommodation> accommodations, User user)
        {
            List<Accommodation> ownerAccommodations = accommodations.Where(a => a.Owner.Id == user.Id).ToList();
            return ownerAccommodations;
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

        public void CheckSearchTermConditions(List<Accommodation> FilteredData, string nameSearchTerm, string citySearchTerm, string countrySearchTerm, string typeSearchTerm, int maxGuests, int minDays)
        {
            foreach (Accommodation accommodation in _accommodations)
            {
                if (CheckSearchTerm(accommodation, nameSearchTerm, citySearchTerm, countrySearchTerm, typeSearchTerm, maxGuests, minDays))
                {
                    FilteredData.Add(accommodation);

                }

            }
        }

        public void MarkRenovated(List<AccommodationRenovation> renovations)
        {
            List<Accommodation> accommodationsToUpdate = new List<Accommodation>();

            foreach (Accommodation accommodation in _accommodations)
            {
                if (AccommodationNeedsRenovation(accommodation, renovations))
                {
                    accommodation.Renovated = true;
                    accommodationsToUpdate.Add(accommodation);
                }
            }

            foreach (Accommodation accommodation in accommodationsToUpdate)
            {
                Update(accommodation);
            }
        }
        private bool AccommodationNeedsRenovation(Accommodation accommodation, List<AccommodationRenovation> renovations)
        {
            foreach (AccommodationRenovation renovation in renovations)
            {
                if (accommodation.Id == renovation.Accommodation.Id && renovation.EndDate < DateTime.Today)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckSearchTerm(Accommodation accommodation, string nameSearchTerm, string citySearchTerm, string countrySearchTerm, string typeSearchTerm, int maxGuestsSearchTerm, int minDaysSearchTerm)
        {
            bool nameCondition = true;
            bool cityCondition = true;
            bool countryCondition = true;
            bool typeCondition = true;
            bool maxGuestsCondition = true;
            bool minDaysCondition = true;

            if (!string.IsNullOrEmpty(nameSearchTerm))
            {
                if (!accommodation.Name.ToLower().Contains(nameSearchTerm.ToLower()))
                {
                    nameCondition = false;
                }
            }

            if (!string.IsNullOrEmpty(citySearchTerm))
            {
                if (!accommodation.Location.City.ToLower().Contains(citySearchTerm.ToLower()))
                {
                    cityCondition = false;
                }
            }
            if (!string.IsNullOrEmpty(countrySearchTerm))
            {
                if (!accommodation.Location.Country.ToLower().Contains(countrySearchTerm.ToLower()))
                {
                    countryCondition = false;
                }
            }
            if (!string.IsNullOrEmpty(typeSearchTerm))
            {
                if (!accommodation.Type.ToLower().Contains(typeSearchTerm.ToLower()))
                {
                    typeCondition = false;
                }
            }
            if (maxGuestsSearchTerm > 0)
            {
                if (accommodation.MaxGuests < maxGuestsSearchTerm)
                {
                    maxGuestsCondition = false;
                }
            }
            if (minDaysSearchTerm > 0)
            {
                if (accommodation.MinDays > minDaysSearchTerm)
                {
                    minDaysCondition = false;
                }
            }

            if (nameCondition && cityCondition && countryCondition && typeCondition && maxGuestsCondition && minDaysCondition)
            {
                return true;

            }
            return false;
        }
    }
}
