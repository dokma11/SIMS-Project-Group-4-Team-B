using Sims2023.Repositories;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Application.Services
{
    public class TourReservationService
    {
        private TourReservationRepository _tourReservations;

        public TourReservationService()
        {
            _tourReservations = new TourReservationRepository();
        }

        public List<TourReservation> GetAll()
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

        public void Save()
        {
            _tourReservations.Save();
        }

        public List<TourReservation> GetReservationsByToursid(int id)
        {
            return _tourReservations.GetReservationsByToursId(id);
        }
    }
}
