using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model.DAO
{
    class AccommodationReservationDAO : ISubject
    {
        private List<IObserver> _observers;

        private AccommodationReservationFileHandler _fileHandler;
        private List<AccommodationReservation> _accommodationReservations;

        public AccommodationReservationDAO()
        {
            _fileHandler = new AccommodationReservationFileHandler();
            _accommodationReservations = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public AccommodationReservation GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public int NextId()
        {
            if (_accommodationReservations.Count == 0) return 1;
            return _accommodationReservations.Max(s => s.Id) + 1;
        }

        public void Add(AccommodationReservation reservation)
        {
            reservation.Id = NextId();
            _accommodationReservations.Add(reservation);
            _fileHandler.Save(_accommodationReservations);
            NotifyObservers();
        }

        // guests who are already graded should not appear in a list
        private void RemoveAlreadyGraded(List<AccommodationReservation> reservations, List<GuestGrade> grades)
        {
            for (int i = reservations.Count - 1; i >= 0; i--)
            {
                var reservation = reservations[i];
                foreach (var grade in grades)
                {
                    if (reservation.Guest.Id == grade.Guest.Id)
                    {
                        reservations.RemoveAt(i);

                    }
                }
            }
        }

        // searching for the guests who left not more than 5 days ago
       private void FindGuestsWhoRecentlyLeft(List<AccommodationReservation> reservatons)
        {
            for (int i = reservatons.Count - 1; i >= 0; i--)
            {
                
                    DateTime lastDate = reservatons[i].StartDate.AddDays(reservatons[i].NumberOfDays);

                    TimeSpan elapsed = DateTime.Now - lastDate;
                    int totalDays = (int)elapsed.TotalDays;

                    if (lastDate < DateTime.Now)
                    {
                        if (totalDays > 5)
                            reservatons.RemoveAt(i);
                    }
                    else if (lastDate > DateTime.Now)
                    {
                        reservatons.RemoveAt(i);
                    }               
            }
        }

        private void FindGuestsParticularOwner(List<AccommodationReservation> reservatons, User user)
        {
            reservatons.RemoveAll(r => r.Accommodation.Owner.Id != user.Id);
        }


        // finds all guests who owner can grade
        public List<AccommodationReservation> findGradableGuests(User user,List<AccommodationReservation> reservatons, List<GuestGrade> grades)
        {
            FindGuestsParticularOwner(reservatons, user);   
            RemoveAlreadyGraded(reservatons, grades);
            FindGuestsWhoRecentlyLeft(reservatons);
            return reservatons;

        }
        public void Remove(AccommodationReservation reservation)
        {
            _accommodationReservations.Remove(reservation);
            _fileHandler.Save(_accommodationReservations);
            NotifyObservers();
        }

        public void Update(AccommodationReservation reservation)
        {
            int index = _accommodationReservations.FindIndex(p => p.Id == reservation.Id);
            if (index != -1)
            {
                _accommodationReservations[index] = reservation;
            }

            _fileHandler.Save(_accommodationReservations);
            NotifyObservers();
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservations;
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