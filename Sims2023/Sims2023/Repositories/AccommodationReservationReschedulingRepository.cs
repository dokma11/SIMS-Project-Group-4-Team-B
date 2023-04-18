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
using Sims2023.Domain.RepositoryInterfaces;

namespace Sims2023.Repositories
{
    public class AccommodationReservationReschedulingRepository: IAccommodationReservationReschedulingRepository
    {
        private List<IObserver> _observers;
        private AccommodationReservationRepository reservations { get; set; }
        private AccommodationReservationReschedulingFileHandler _fileHandler;
        private List<AccommodationReservationRescheduling> _accommodationReservationReschedulings;

        public AccommodationReservationReschedulingRepository()
        {
            _fileHandler = new AccommodationReservationReschedulingFileHandler();
            _accommodationReservationReschedulings = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public AccommodationReservationRescheduling GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public List<AccommodationReservationRescheduling> GetGuestsForOwner(User owner, List<AccommodationReservationRescheduling> guests)
        {
            DateTime today = DateTime.Today;
            return guests.Where(g => g.AccommodationReservation.Accommodation.Owner.Id == owner.Id && g.Status == RequestStatus.Pending && g.AccommodationReservation.StartDate > today).ToList();
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

        public List<AccommodationReservationRescheduling> FindSuitableReservationReschedulings(User guest1)
        {
            List<AccommodationReservationRescheduling> FilteredReservationReschedulings = new List<AccommodationReservationRescheduling>();
            foreach (AccommodationReservationRescheduling accommodationReservationRescheduling in _accommodationReservationReschedulings)
            {
                if (FilterdDataSelection(accommodationReservationRescheduling, guest1))
                {
                    FilteredReservationReschedulings.Add(accommodationReservationRescheduling);
                }
            }
            return FilteredReservationReschedulings;
        }

        public bool FilterdDataSelection(AccommodationReservationRescheduling accommodationReservationRescheduling, User guest1)
        {
            TimeSpan difference = accommodationReservationRescheduling.AccommodationReservation.StartDate - DateTime.Today;
            if (difference.TotalDays >= 0 && accommodationReservationRescheduling.AccommodationReservation.Guest.Id == guest1.Id)
            {
                return true;
            }
            return false;
        }

        public void checkForNotifications(User guest1)
        {
            foreach (AccommodationReservationRescheduling accommodationReservationRescheduling in _accommodationReservationReschedulings)
            {
                if (Notify(accommodationReservationRescheduling, guest1))
                {
                    MessageBox.Show($" Vlasnik smestaja {accommodationReservationRescheduling.AccommodationReservation.Accommodation.Name} je promienio status vaseg zahteva za pomeranje rezervacije. Vas zahtev je {accommodationReservationRescheduling.Status}!");
                    accommodationReservationRescheduling.Notified = true;
                    Update(accommodationReservationRescheduling);
                }
            }
        }

        public bool Notify(AccommodationReservationRescheduling accommodationReservationRescheduling,User guest1)
        {
            if (accommodationReservationRescheduling.Notified == false && accommodationReservationRescheduling.AccommodationReservation.Guest.Id == guest1.Id && accommodationReservationRescheduling.Status.ToString() != "Pending")
            {
                return true;
            }
            return false;
        }

        public bool CheckForActiveRequest(AccommodationReservation selectedAccommodationReservation)
        {
            foreach (AccommodationReservationRescheduling accommodationReservationRescheduling in _accommodationReservationReschedulings)
            {
                if (accommodationReservationRescheduling.AccommodationReservation.Id == selectedAccommodationReservation.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
