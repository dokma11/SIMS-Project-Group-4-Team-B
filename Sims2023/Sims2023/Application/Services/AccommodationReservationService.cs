using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Repositories;
using System.Collections.ObjectModel;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Application.Injection;
using System.Windows;

namespace Sims2023.Application.Services
{
    public class AccommodationReservationService
    {
        private IUserCSVRepository _user;
        private IAccommodationCSVRepository _accommodation;
        private IAccommodationReservationCSVRepository _accommodationReservation;
        private ILocationCSVRepository _location;
        public AccommodationReservationService()
        {
            _user = Injector.CreateInstance<IUserCSVRepository>();
            _accommodation = Injector.CreateInstance<IAccommodationCSVRepository>();
            _accommodationReservation = Injector.CreateInstance<IAccommodationReservationCSVRepository>();
            _location = Injector.CreateInstance<ILocationCSVRepository>();
            GetReservationReferences();
        }

        public List<AccommodationReservation> GetGradableGuests(User user, List<AccommodationReservation> reservatons, List<GuestGrade> grades)
        {
            return _accommodationReservation.GetGradableGuests(user, reservatons, grades);
        }

        public void GetReservationReferences()
        {
            foreach (var item in GetAllReservations())
            {
                var accommodation = _accommodation.GetById(item.Accommodation.Id);
                var owner = _user.GetById(accommodation.Owner.Id);
                var location = _location.GetById(accommodation.Location.Id);
                var user = _user.GetById(item.Guest.Id);
                if (accommodation != null)
                {
                    item.Guest = user;
                    item.Accommodation = accommodation;
                    item.Accommodation.Owner = owner;
                    item.Accommodation.Location = location;

                }
            }
        }

        public List<Location> GetUnvisitedLocations(List<Location> locations)
        {
            Save();
            return _accommodationReservation.GetUnvisitedLocations(locations);
        }

        public List<AccommodationReservation> GetReservationsForOwner(User owner)
        {
            Save();
            return _accommodationReservation.GetReservationsForOwner(owner);
        }

        public void Save()
        {
            _accommodationReservation.Save();
            GetReservationReferences();
        }

        public List<AccommodationReservation> GetAllReservations()
        {
            return _accommodationReservation.GetAll();
        }
        public void Create(AccommodationReservation reservation)
        {
            _accommodationReservation.Add(reservation);
            Save();
        }

        public void Delete(AccommodationReservation reservation)
        {
            _accommodationReservation.Remove(reservation);
            Save();
        }

        public AccommodationReservation GetById(int Id)
        {
            return _accommodationReservation.GetById(Id);
        }
        public void Update(AccommodationReservation reservation)
        {
            _accommodationReservation.Update(reservation);
            Save();
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationReservation.Subscribe(observer);
        }

        public List<AccommodationReservation> FindSuitableUpcomingReservations(User guest1)
        {
            return _accommodationReservation.FindSuitableUpcomingReservations(guest1);
        }

        public List<AccommodationReservation> FindSuitablePastReservations(User guest1)
        {
            return _accommodationReservation.FindSuitablePastReservations(guest1);
        }

        public int CheckDates(Accommodation selectedAccommodation, DateTime startDateSelected, DateTime endDateSelected, int stayLength, List<DateTime> datesList, List<AccommodationRenovation> accommodationRenovations)
        {
            return _accommodationReservation.CheckDates(selectedAccommodation,startDateSelected, endDateSelected, stayLength, datesList, accommodationRenovations);
        }

        public void DeleteAccommodationReservation(AccommodationReservation selectedAccommodationReservation)
        {
            _accommodationReservation.DeleteAccommodationReservation(selectedAccommodationReservation);
        }

        public List<GuestGrade> FindSuitableGrades(User user, List<GuestGrade> guestGrades)
        {
            return _accommodationReservation.FindSuitableGrades(user, guestGrades);
        }

        public List<AccommodationReservation> FindAllGuestsReservations(User user)
        {
            return _accommodationReservation.FindAllGuestsReservations(user);
        }
    }
}
