using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sims2023.Domain.RepositoryInterfaces;
using System.Threading.Tasks;
using Sims2023.Application.Injection;
using System.Windows;

namespace Sims2023.Application.Services
{
    public class AccommodationService
    {
        private IAccommodationCSVRepository _accomodation;
        private IUserCSVRepository _users;
        private ILocationCSVRepository _locations;

        public AccommodationService()
        {
            _users = Injector.CreateInstance<IUserCSVRepository>();
            _locations = Injector.CreateInstance<ILocationCSVRepository>();
            _accomodation = Injector.CreateInstance<IAccommodationCSVRepository>();
            FindForeignAtributes();
        }

        private void FindForeignAtributes()
        {
            foreach (var item in GetAllAccommodations())
            {
                var user = _users.GetById(item.Owner.Id);
                var loc = _locations.GetById(item.Location.Id);
                if (user != null && loc != null)
                {
                    item.Owner = user;
                    item.Location = loc;
                }
            }
        }

        public List<Location> GetOwnerLocations(List<Accommodation> accommodations)
        {
            Save();
            return _accomodation.GetOwnerLocations(accommodations);
        }
        public void Save()
        {
            _accomodation.Save();
            FindForeignAtributes();
        }
        public List<Accommodation> GetAllAccommodations()
        {
            return _accomodation.GetAll();
        }

        public void Create(Accommodation accommodation)
        {
            _accomodation.Add(accommodation);
            Save();
        }

        public void Delete(Accommodation accommodation)
        {
            _accomodation.Remove(accommodation);
            Save();
        }

        public void Update(Accommodation accommodation)
        {
            _accomodation.Update(accommodation);
            Save();
        }

        public List<Accommodation> GetOwnerAccommodations(List<Accommodation> accommodations, User user)
        {
            Save();
            return _accomodation.GetOwnerAccommodations(accommodations, user);
        }

        public void MarkRenovated(List<AccommodationRenovation> renovations)
        {
            Save();
            _accomodation.MarkRenovated(renovations);
        }

        public void Subscribe(IObserver observer)
        {
            _accomodation.Subscribe(observer);
        }
        public Accommodation GetById(int id)
        {
            return _accomodation.GetById(id);
        }
        public void CheckSearchTermConditions(List<Accommodation> FilteredData, string nameSearchTerm, string citySearchTerm, string countrySearchTerm, string typeSearchTerm, int maxGuests, int minDays)
        {
            _accomodation.CheckSearchTermConditions(FilteredData, nameSearchTerm, citySearchTerm, countrySearchTerm, typeSearchTerm, maxGuests, minDays);
        }
    }
}
