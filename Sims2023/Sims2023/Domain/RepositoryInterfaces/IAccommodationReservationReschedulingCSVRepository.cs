using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Sims2023.Domain.Models.AccommodationReservationRescheduling;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IAccommodationReservationReschedulingCSVRepository
    {
        public AccommodationReservationRescheduling GetById(int id);
        public List<AccommodationReservationRescheduling> GetGuestsForOwner(User owner, List<AccommodationReservationRescheduling> guests);
        public bool IsDateSpanAvailable(AccommodationReservationRescheduling request);
        public int NextId();
        public void Save();
        public void Add(AccommodationReservationRescheduling reservationRescheduling);
        public void Update(AccommodationReservationRescheduling reservationRescheduling);
        public void Remove(AccommodationReservationRescheduling reservationRescheduling);
        public List<AccommodationReservationRescheduling> GetAll();
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
        public ObservableCollection<AccommodationReservationRescheduling> FindSuitableReservationReschedulings(User guest1, ObservableCollection<AccommodationReservationRescheduling> accommodationReservationReschedulings);
        public bool FilterdDataSelection(AccommodationReservationRescheduling accommodationReservationRescheduling, User guest1);
        public bool CheckForActiveRequest(AccommodationReservation selectedAccommodationReservation);
    }
}
