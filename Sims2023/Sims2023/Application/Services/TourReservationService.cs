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
            tourReservation.ShouldConfirmParticipation = false;
            tourReservation.ConfirmedParticipation = confirmed;
            _tourReservations.Update(tourReservation);
        }

        public List<Tour> GetGuestsAll(User user)//new method for guest2
        {
            List<Tour> Tours = new List<Tour>();
            foreach (TourReservation reservation in _tourReservations.GetAll())
            {
                if (reservation.User.Id == user.Id && (reservation.Tour.CurrentState != Tour.State.Started || reservation.ConfirmedParticipation == true))
                {
                    Tours.Add(reservation.Tour);
                }
            }

            return Tours;

        }

        public void CheckVouchers(TourReservation tourReservation, Tour tour)//new method for guest2
        {
            int countReservation = 0;

            foreach (var reservation in GetAll())
            {
                if (tourReservation.User.Id == reservation.User.Id && tourReservation.ReservationTime.Year == reservation.ReservationTime.Year)
                {
                    countReservation++;
                }
            }

            if (countReservation > 0 && countReservation % 5 == 0)
            {
                var user = _users.GetById(tourReservation.User.Id);
                var voucher = new Voucher(Voucher.VoucherType.FiveReservations, user, tour);
                _vouchers.Add(voucher);

            }
        }

        public void Subscribe(IObserver observer)
        {
            _tourReservations.Subscribe(observer);
        }

        public void Save()
        {
            _tourReservations.Save();
        }
    }
}
