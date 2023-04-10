using Sims2023.Model.DAO;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Repositories;

namespace Sims2023.Application.Services
{
    public class AccommodationReservationService
    {
        private AccommodationReservationRepository _accommodationReservation;

        public AccommodationReservationService()
        {
            _accommodationReservation = new AccommodationReservationRepository();
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

        public void Update(AccommodationReservation reservation)
        {
            _accommodationReservation.Update(reservation);
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationReservation.Subscribe(observer);
        }
    }
}
