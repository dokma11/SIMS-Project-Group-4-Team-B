using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    class AccommodationReservationCSVRepository : ISubject, IAccommodationReservationCSVRepository
    {
        private List<IObserver> _observers;

        private AccommodationReservationFileHandler _fileHandler;
        private List<AccommodationReservation> _accommodationReservations;

        public AccommodationReservationCSVRepository()
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

        public void Save()
        {
            _fileHandler.Save(_accommodationReservations);
        }
        public void Add(AccommodationReservation reservation)
        {
            reservation.Id = NextId();
            _accommodationReservations.Add(reservation);
            _fileHandler.Save(_accommodationReservations);
            NotifyObservers();
        }

        public void RemoveAlreadyGraded(List<AccommodationReservation> reservations, List<GuestGrade> grades)
        {
            for (int i = reservations.Count - 1; i >= 0; i--)
            {
                var reservation = reservations[i];
                if (isGuestAlreadyGraded(reservation, grades))
                {
                    reservations.RemoveAt(i);
                }
            }
        }

        public bool isGuestAlreadyGraded(AccommodationReservation reservation, List<GuestGrade> grades)
        {
            foreach (var grade in grades)
            {
                if (reservation.Guest.Id == grade.Guest.Id) return true;
            }
            return false;
        }

        public void GetGuestsWhoRecentlyLeft(List<AccommodationReservation> reservatons)
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

        public void GetGuestsParticularOwner(List<AccommodationReservation> reservatons, User user)
        {
            reservatons.RemoveAll(r => r.Accommodation.Owner.Id != user.Id);
        }

        public List<AccommodationReservation> GetGradableGuests(User user, List<AccommodationReservation> reservatons, List<GuestGrade> grades)
        {
            GetGuestsParticularOwner(reservatons, user);
            RemoveAlreadyGraded(reservatons, grades);
            GetGuestsWhoRecentlyLeft(reservatons);
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
        public List<AccommodationReservation> FindSuitableUpcomingReservations(User guest1)
        {
            List<AccommodationReservation> FilteredReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in _accommodationReservations)
            {
                if (FilterdDataSelection(accommodationReservation, guest1))
                {
                    FilteredReservations.Add(accommodationReservation);
                }
            }
            return FilteredReservations;
        }

        public bool FilterdDataSelection(AccommodationReservation accommodationReservation, User guest1)
        {
            TimeSpan difference = accommodationReservation.StartDate - DateTime.Today;
            return difference.TotalDays >= 0 && accommodationReservation.Guest.Id == guest1.Id;
        }

        public int CheckDates(Accommodation selectedAccommodation, DateTime startDateSelected, DateTime endDateSelected, int stayLength, List<DateTime> datesList)
        {
            DateTime endDate = startDateSelected.AddDays(stayLength);
            bool isAvailable;

            for (DateTime startDate = startDateSelected; startDate <= endDateSelected.AddDays(-stayLength); startDate = startDate.AddDays(1), endDate = endDate.AddDays(1))
            {
                isAvailable = IsDateSpanAvailable(selectedAccommodation, startDate, endDate);
                if (isAvailable)
                {
                    if (datesList.Count == 0)
                    {
                        datesList.Add(startDate);
                    }
                    else
                    {
                        if (!datesList.Contains(startDate))
                        {
                            datesList.Add(startDate);
                        }

                    }
                }
            }
            return datesList.Count;
        }

        public bool IsDateSpanAvailable(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate)
        {
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                if (reservation.Accommodation.Id == selectedAccommodation.Id)
                {
                    for (DateTime i = reservation.StartDate; i <= reservation.EndDate; i = i.AddDays(1))
                    {
                        for (DateTime j = startDate; j <= endDate; j = j.AddDays(1))
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

        public List<AccommodationReservation> FindSuitablePastReservations(User guest1)
        {
            List<AccommodationReservation> FilteredReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in _accommodationReservations)
            {
                if (CheckReschedulingReservation(accommodationReservation, guest1))
                {
                    FilteredReservations.Add(accommodationReservation);
                }
            }
            return FilteredReservations;
        }

        public bool CheckReschedulingReservation(AccommodationReservation accommodationReservation, User guest1)
        {
            TimeSpan difference = DateTime.Today - accommodationReservation.EndDate;
            return difference.TotalDays >= 0 && accommodationReservation.Guest.Id == guest1.Id;

        }

        public void DeleteAccommodationReservation(AccommodationReservation selectedAccommodationReservation)
        {
            foreach (AccommodationReservation accommodationResrvation in _accommodationReservations)
            {
                if (accommodationResrvation.Id == selectedAccommodationReservation.Id)
                {
                    Remove(selectedAccommodationReservation);
                    return;
                }
            }
        }

        public List<GuestGrade> FindSuitableGrades(User user, List<GuestGrade> guestGrades)
        {
            List<GuestGrade> SuitableGrades=new List<GuestGrade>();
            foreach (GuestGrade grade in guestGrades)
            {
                AccommodationReservation accommodationReservation = FindReservation(user, grade);
                if (accommodationReservation != null && accommodationReservation.Graded)
                {
                    SuitableGrades.Add(grade);
                }
            }
            
            return SuitableGrades;
        }

        private AccommodationReservation FindReservation(User user, GuestGrade grade)
        {
            foreach (AccommodationReservation accommodationReservation in _accommodationReservations)
            {
                if(user.Id == accommodationReservation.Guest.Id && grade.StartDate == accommodationReservation.StartDate && grade.Accommodation.Id == accommodationReservation.Accommodation.Id)
                {
                    return accommodationReservation;
                }
            }
            return null;
        }

        public List<AccommodationReservation> FindAllGuestsReservations(User user)
        {
            List<AccommodationReservation> AllGuestsReservations = new();
            var lastYearStartDate = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day);
            var lastYearEndDate = DateTime.Today;
            foreach (AccommodationReservation accommodationReservation in _accommodationReservations)
            {
                if(accommodationReservation.Guest.Id == user.Id && accommodationReservation.StartDate>lastYearStartDate && accommodationReservation.EndDate<lastYearEndDate)
                {
                    AllGuestsReservations.Add(accommodationReservation);
                }
            }
            return AllGuestsReservations;
        }
    }
}