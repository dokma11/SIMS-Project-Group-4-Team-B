using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class TourReservationService
    {
        private ITourReservationCSVRepository _tourReservation;
        private ITourReadFromCSVRepository _tour;
        private IUserCSVRepository _user;

        public TourReservationService()
        {
            _tourReservation = Injection.Injector.CreateInstance<ITourReservationCSVRepository>();
            _tour = Injection.Injector.CreateInstance<ITourReadFromCSVRepository>();
            _user = Injection.Injector.CreateInstance<IUserCSVRepository>();

            GetUserReferences();
            GetTourReferences();
        }

        public List<TourReservation> GetAll()
        {
            return _tourReservation.GetAll();
        }

        public List<TourReservation> GetNotConfirmedParticipation()//new method for guest2
        {
            return _tourReservation.GetNotConfirmedParticipation();
        }

        public void Create(TourReservation reservation)
        {
            _tourReservation.Add(reservation);
            Save();
        }

        public void Update(TourReservation reservation)
        {
            _tourReservation.Update(reservation);
            Save();
        }

        public void ConfirmReservation(TourReservation tourReservation, bool confirmed)//new method for guest2
        {
            _tourReservation.ConfirmReservation(tourReservation, confirmed);
        }

        public List<TourReservation> GetByUser(User user)//new method for guest2
        {
            return _tourReservation.GetByUser(user);
        }

        public bool CheckVouchers(TourReservation tourReservation)//new method for guest2
        {
            return _tourReservation.CountReservationsByUser(tourReservation);
        }

        public void Subscribe(IObserver observer)
        {
            _tourReservation.Subscribe(observer);
        }

        public void Save()
        {
            _tourReservation.Save();
            GetUserReferences();
            GetTourReferences();
        }

        public List<TourReservation> GetByToursid(int id)
        {
            return _tourReservation.GetByToursId(id);
        }

        public int GetAgeStatistics(Tour selectedTour, string ageGroup)
        {
            return _tourReservation.GetAgeStatistics(selectedTour, ageGroup);
        }

        public int GetVoucherStatistics(Tour selectedTour, bool used)
        {
            return _tourReservation.GetVoucherStatistics(selectedTour, used);
        }

        public void GetAttendedGuestsNumber(User loggedInGuide)
        {
            _tourReservation.CalculateAttendedGuestsNumber(loggedInGuide, _tour.GetFinished(loggedInGuide));
        }

        public List<User> GetGuestsWithReservations(KeyPoint keyPoint, List<User> markedGuests)
        {
            return _tourReservation.GetGuestsWithReservations(keyPoint, markedGuests, _user.GetAll());
        }

        public void GetTourReferences()
        {
            foreach (var reservation in GetAll())
            {
                reservation.Tour = _tour.GetById(reservation.Tour.Id) ?? reservation.Tour;
            }
        }

        public void GetUserReferences()
        {
            foreach (var reservation in GetAll())
            {
                reservation.User = _user.GetById(reservation.User.Id) ?? reservation.User;
            }
        }
    }
}
