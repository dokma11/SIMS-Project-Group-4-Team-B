using Sims2023.Model.DAO;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Controller
{
    public class AccommodationReservationController
    {
        private AccommodationReservationDAO _accommodationReservation;

        public AccommodationReservationController()
        {
            _accommodationReservation = new AccommodationReservationDAO();
        }

        public List<AccommodationReservation> GetGradableGuests(List<AccommodationReservation> reservatons, List<GuestGrade> grades)
        {
            return _accommodationReservation.findGradableGuests(reservatons, grades);
        }

        public string GetAllUngradedNames(List<AccommodationReservation> ungradedGuests)
        {
            return _accommodationReservation.UngradedGuestsNameAndSurrname(ungradedGuests);
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
