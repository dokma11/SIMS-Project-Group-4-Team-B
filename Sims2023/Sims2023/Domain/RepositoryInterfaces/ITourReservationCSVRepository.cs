using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourReservationCSVRepository
    {
        public int NextId();
        public void Add(TourReservation reservation);
        public void Remove(TourReservation reservation);
        public void Update(TourReservation reservation);
        public List<TourReservation> GetAll();
        public void Save();
        public List<TourReservation> GetReservationsByToursId(int id);
        public string GetAgeStatistics(Tour selectedTour, string ageGroup);
        public string GetVoucherStatistics(Tour selectedTour, bool used);
        public List<TourReservation> GetNotConfirmedParticipation();//new
        public void ConfirmReservation(TourReservation tourReservation, bool confirmed);//new
        public bool CountReservationsByUser(TourReservation tourReservation);//new
        public List<Tour> GetByUser(User user);//new
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
    }
}
