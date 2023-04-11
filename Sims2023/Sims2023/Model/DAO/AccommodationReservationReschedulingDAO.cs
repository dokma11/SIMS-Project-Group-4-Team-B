using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repositories;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sims2023.Model.AccommodationReservationRescheduling;

namespace Sims2023.Model.DAO
{
    public class AccommodationReservationReschedulingDAO
    {
        private List<IObserver> _observers;
        private AccommodationReservationRepository reservations { get; set; }
        private AccommodationReservationReschedulingFileHandler _fileHandler;
        private List<AccommodationReservationRescheduling> _accommodationReservationReschedulings;

        public AccommodationReservationReschedulingDAO()
        {
            _fileHandler = new AccommodationReservationReschedulingFileHandler();
            _accommodationReservationReschedulings = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public AccommodationReservationRescheduling GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public List<AccommodationReservationRescheduling> FindGuestsForOwner(User owner, List<AccommodationReservationRescheduling> guests)
        {
            return guests.Where(g => g.AccommodationReservation.Accommodation.Owner.Id == owner.Id && g.Status == RequestStatus.Pending).ToList();
        }

        public bool IsDateSpanAvailable(AccommodationReservationRescheduling request)
        {
            reservations = new AccommodationReservationRepository();
            foreach (AccommodationReservation reservation in reservations.GetAll())
            {
                if (reservation.Accommodation.Id == request.AccommodationReservation.Accommodation.Id)
                {
                    for (DateTime i = reservation.StartDate; i <= reservation.EndDate; i = i.AddDays(1))
                    {
                        for (DateTime j = request.NewStartDate; j <= request.NewEndDate; j = j.AddDays(1))
                        {
                            if (i == j)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public int NextId()
        {
            if (_accommodationReservationReschedulings.Count == 0) return 1;
            return _accommodationReservationReschedulings.Max(s => s.Id) + 1;
        }

        public void Add(AccommodationReservationRescheduling reservationRescheduling)
        {
            reservationRescheduling.Id = NextId();
            _accommodationReservationReschedulings.Add(reservationRescheduling);
            _fileHandler.Save(_accommodationReservationReschedulings);
            NotifyObservers();
        }

        public void Update(AccommodationReservationRescheduling reservationRescheduling)
        {
            int index = _accommodationReservationReschedulings.FindIndex(p => p.Id == reservationRescheduling.Id);
            if (index != -1)
            {
                _accommodationReservationReschedulings[index] = reservationRescheduling;
            }

            _fileHandler.Save(_accommodationReservationReschedulings);
            NotifyObservers();
        }

        public void Remove(AccommodationReservationRescheduling reservationRescheduling)
        {
            _accommodationReservationReschedulings.Remove(reservationRescheduling);
            _fileHandler.Save(_accommodationReservationReschedulings);
            NotifyObservers();
        }

        public List<AccommodationReservationRescheduling> GetAll()
        {
            return _accommodationReservationReschedulings;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
