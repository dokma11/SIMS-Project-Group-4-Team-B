using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourReservationRepository
    {
        public int NextId();
        public void Add(TourReservation reservation);
        public void Remove(TourReservation reservation);
        public void Update(TourReservation reservation);
        public List<TourReservation> GetAll();
        public List<TourReservation> GetNotConfirmedParticipation();//new
        public void ConfirmReservation(TourReservation tourReservation, bool confirmed);//new
        public bool CountReservationsByUser(TourReservation tourReservation);//new
        public List<Tour> GetByUser(User user);//new
        public void Save();



    }
}
