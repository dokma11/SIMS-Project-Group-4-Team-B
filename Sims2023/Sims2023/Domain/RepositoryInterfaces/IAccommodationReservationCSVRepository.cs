using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IAccommodationReservationCSVRepository
    {
        public AccommodationReservation GetById(int id);
        public int NextId();
        public void Add(AccommodationReservation reservation);
        public void RemoveAlreadyGraded(List<AccommodationReservation> reservations, List<GuestGrade> grades);
        public bool isGuestAlreadyGraded(AccommodationReservation reservation, List<GuestGrade> grades);
        public void GetGuestsWhoRecentlyLeft(List<AccommodationReservation> reservatons);
        public void GetGuestsParticularOwner(List<AccommodationReservation> reservatons, User user);
        public void Save();
        public List<AccommodationReservation> GetGradableGuests(User user, List<AccommodationReservation> reservatons, List<GuestGrade> grades);
        public void Remove(AccommodationReservation reservation);
        public void Update(AccommodationReservation reservation);
        public List<AccommodationReservation> GetAll();
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
        public List<AccommodationReservation> FindSuitableUpcomingReservations(User guest1);
        public bool FilterdDataSelection(AccommodationReservation accommodationReservation, User guest1);
        public int CheckDates(Accommodation selectedAccommodation, DateTime startDateSelected, DateTime endDateSelected, int stayLength, List<DateTime> datesList, List<AccommodationRenovation> accommodationRenovations);
        public bool IsDateSpanAvailable(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, List<AccommodationRenovation> accommodationRenovations);
        public bool IsDateSpanForReservationAvailable(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate);
        public bool IsDateSpanForRenovationAvailable(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, List<AccommodationRenovation> accommodationRenovations);
        public List<AccommodationReservation> FindSuitablePastReservations(User guest1);
        public bool CheckReschedulingReservation(AccommodationReservation accommodationReservation, User guest1);
        public void DeleteAccommodationReservation(AccommodationReservation selectedAccommodationReservation);
        public List<GuestGrade> FindSuitableGrades(User user, List<GuestGrade> guestGrades);
        public List<AccommodationReservation> FindAllGuestsReservations(User user);
    }
}