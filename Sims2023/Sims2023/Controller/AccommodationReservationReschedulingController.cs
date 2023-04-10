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
    public class AccommodationReservationReschedulingController
    {
        private AccommodationReservationReschedulingDAO _accommodationReservationRescheduling;

        public AccommodationReservationReschedulingController()
        {
            _accommodationReservationRescheduling = new AccommodationReservationReschedulingDAO();
        }

        public AccommodationReservationRescheduling GetById(int id)
        {
            return _accommodationReservationRescheduling.GetById(id);
        }

        public List<AccommodationReservationRescheduling> GetAllReservationReschedulings()
        {
            return _accommodationReservationRescheduling.GetAll();
        }

        public void Create(AccommodationReservationRescheduling reservationRescheduling)
        {
            _accommodationReservationRescheduling.Add(reservationRescheduling);
        }

        public void Delete(AccommodationReservationRescheduling reservationRescheduling)
        {
            _accommodationReservationRescheduling.Remove(reservationRescheduling);
        }

        public void Update(AccommodationReservationRescheduling reservationRescheduling)
        {
            _accommodationReservationRescheduling.Update(reservationRescheduling);
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationReservationRescheduling.Subscribe(observer);
        }
    }
}
