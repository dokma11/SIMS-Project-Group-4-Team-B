using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class TourReservationService
    {
        private ITourReservationRepository _tourReservations;

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

        public string GetAgeStatistics(Tour selectedTour, string ageGroup)
        {
            return _tourReservations.GetAgeStatistics(selectedTour, ageGroup);
        }

        public string GetVoucherStatistics(Tour selectedTour, bool used)
        {
            return _tourReservations.GetVoucherStatistics(selectedTour, used);
        }
    }
}
