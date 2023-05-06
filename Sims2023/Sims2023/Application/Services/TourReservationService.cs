using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class TourReservationService
    {
        private ITourReservationCSVRepository _tourReservations;

        public TourReservationService()
        {
            _tourReservations = new TourReservationCSVRepository();
            //_tourReservations = Injection.Injector.CreateInstance<ITourReservationRepository>();
            
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

        public int GetAgeStatistics(Tour selectedTour, string ageGroup)
        {
            return _tourReservations.GetAgeStatistics(selectedTour, ageGroup);
        }

        public int GetVoucherStatistics(Tour selectedTour, bool used)
        {
            return _tourReservations.GetVoucherStatistics(selectedTour, used);
        }
    }
}
