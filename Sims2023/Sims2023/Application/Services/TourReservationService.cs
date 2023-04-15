using Sims2023.Repositories;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.RepositoryInterfaces;

namespace Sims2023.Application.Services
{
    public class TourReservationService
    {
        private ITourReservationRepository _tourReservations;
        private VoucherRepository _vouchers;
        private UserRepository _users;


        public TourReservationService()
        {
            _tourReservations = new TourReservationRepository();
            _vouchers = new VoucherRepository();
            _users = new UserRepository();
        }

        public List<TourReservation> GetAll()
        {
            return _tourReservations.GetAll();
        }

        public List<TourReservation> GetNotConfirmedParticipation()//new method for guest2
        {
            return _tourReservations.GetNotConfirmedParticipation();
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

        public void ConfirmReservation(TourReservation tourReservation, bool confirmed)//new method for guest2
        {
            _tourReservations.ConfirmReservation(tourReservation, confirmed);
        }

        public List<Tour> GetByUser(User user)//new method for guest2
        {
            return _tourReservations.GetByUser(user);

        }

        public bool CheckVouchers(TourReservation tourReservation)//new method for guest2
        {
           return _tourReservations.CountReservationsByUser(tourReservation);

            
        }

        /*public void Subscribe(IObserver observer)
        {
            _tourReservations.Subscribe(observer);
        }*/

        public void Save()
        {
            _tourReservations.Save();
        }
    }
}
