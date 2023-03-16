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
        private AccommodationReservationDAO _accommodationReservations;

        public AccommodationReservationController()
        {
            _accommodationReservations = new AccommodationReservationDAO();
        }

        public List<AccommodationReservation> GetAllReservations()
        {
            return _accommodationReservations.GetAll();
        }

        public void Create(AccommodationReservation reservation)
        {
            _accommodationReservations.Add(reservation);
        }

        public void Delete(AccommodationReservation reservation)
        {
            _accommodationReservations.Remove(reservation);
        }

        public void Update(AccommodationReservation reservation)
        {
            _accommodationReservations.Update(reservation);
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationReservations.Subscribe(observer);
        }
    }
}
