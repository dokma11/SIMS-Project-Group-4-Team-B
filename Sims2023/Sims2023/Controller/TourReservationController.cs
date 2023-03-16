using Sims2023.DAO;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Controller
{
    internal class TourReservationController
    {
        private TourReservationDAO _tourReservations;

        public TourReservationController()
        {
            _tourReservations = new TourReservationDAO();
        }

        public List<TourReservation> GetAllReservations()//mozda treba samo get all
        {
            return _tourReservations.GetAll();
        }

        public void Create(TourReservation reservation)
        {
            _tourReservations.Add(reservation);
        }

        public void Delete(TourReservation reservation)
        {
            _tourReservations.Remove(reservation);
        }

        public void Update(TourReservation reservation)
        {
            _tourReservations.Update(reservation);
        }

        public void Subscribe(IObserver observer)
        {
            _tourReservations.Subscribe(observer);
        }
    }
}
