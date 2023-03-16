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

        private AccommodationReservationRepository _repository;
        private List<AccommodationReservation> _accommodationReservations;

        public AccommodationReservationDAO()
        {
            _repository = new AccommodationReservationRepository();
            _accommodationReservations = _repository.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            return _accommodationReservations.Max(s => s.Id) + 1;
        }

        public void Add(AccommodationReservation reservation)
        {
            reservation.Id = NextId();
            _accommodationReservations.Add(reservation);
            _repository.Save(_accommodationReservations);
            NotifyObservers();
        }



        private void addNameSurrnameToReservation(List<Guest> ListOfGuests, List<AccommodationReservation> reservatons)
        {
            foreach (var reservation in reservatons)
            {
                foreach (var guest in ListOfGuests)
                {
                    if (reservation.GuestId == guest.Id)
                    {
                        reservation.Name = guest.Name;
                        reservation.Surrname = guest.Surrname;
                    }
                }
            }
        }

        private void addReservationName(List<AccommodationReservation> reservatons, List<Accommodation> accommodations)
        {
            foreach (var reservation in reservatons)
            {
                foreach (var accommodation in accommodations)
                {
                    if (reservation.Id == accommodation.id)
                    {
                        reservation.AccommodationName = accommodation.name;

                    }
                }
            }
        }

        private void removeAlreadyGraded(List<AccommodationReservation> reservations, List<GuestGrade> grades)
        {
            for (int i = reservations.Count - 1; i >= 0; i--)
            {
                var reservation = reservations[i];
                foreach (var grade in grades)
                {
                    if (reservation.GuestId == grade.GuestId)
                    {
                        reservations.RemoveAt(i);
                        
                    }
                }
            }
        }

        private void findGuestsWhoRecentlyLeft(List<AccommodationReservation> reservatons)
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
               else if(lastDate > DateTime.Now)
                {
                    reservatons.RemoveAt(i);
                }

            }
        }


        public List<AccommodationReservation> findGradableGuests(List<Guest> ListOfGuests, List<Accommodation> _accommodations, List<AccommodationReservation> reservatons, List<GuestGrade> grades)
        {

            addNameSurrnameToReservation(ListOfGuests, reservatons);
            addReservationName(reservatons, _accommodations);
            removeAlreadyGraded(reservatons, grades);
            findGuestsWhoRecentlyLeft(reservatons);
            return reservatons;

        }






        public void Remove(AccommodationReservation reservation)
        {
            _accommodationReservations.Remove(reservation);
            _repository.Save(_accommodationReservations);
            NotifyObservers();
        }

        public void Update(AccommodationReservation reservation)
        {
            int index = _accommodationReservations.FindIndex(p => p.Id == reservation.Id);
            if (index != -1)
            {
                _accommodationReservations[index] = reservation;
            }

            _repository.Save(_accommodationReservations);
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
