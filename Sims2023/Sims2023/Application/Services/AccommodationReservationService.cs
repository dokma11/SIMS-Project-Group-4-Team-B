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

namespace Sims2023.Application.Services
{
    public class AccommodationReservationService
    {
        private IAccommodationReservationRepository _accommodationReservation;

        public AccommodationReservationService()
        {
            _accommodationReservation = new AccommodationReservationRepository();
            //_accommodationReservation = Injection.Injector.CreateInstance<IAccommodationReservationRepository>();
        }

        public List<AccommodationReservation> GetGradableGuests(User user, List<AccommodationReservation> reservatons, List<GuestGrade> grades)
        {
            return _accommodationReservation.findGradableGuests(user, reservatons, grades);
        }


        public List<AccommodationReservation> GetAllReservations()
        {
            return _accommodationReservation.GetAll();
        }

        public void Create(AccommodationReservation reservation)
        {
            _accommodationReservation.Add(reservation);
        }

        public void Delete(AccommodationReservation reservation)
        {
            _accommodationReservation.Remove(reservation);
        }

        public AccommodationReservation GetById(int Id)
        {
            return _accommodationReservation.GetById(Id);
        }
        public void Update(AccommodationReservation reservation)
        {
            _accommodationReservation.Update(reservation);
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

        public int CheckDates(Accommodation selectedAccommodation, DateTime startDateSelected, DateTime endDateSelected, int stayLength, List<DateTime> datesList)
        {
            return _accommodationReservation.CheckDates(selectedAccommodation,startDateSelected, endDateSelected, stayLength, datesList);
        }

        public void DeleteAccommodationReservation(AccommodationReservation selectedAccommodationReservation)
        {
            _accommodationReservation.DeleteAccommodationReservation(selectedAccommodationReservation);
        }
    }
}
