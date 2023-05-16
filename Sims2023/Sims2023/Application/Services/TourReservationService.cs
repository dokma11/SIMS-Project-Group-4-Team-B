using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class TourReservationService
    {
        private ITourReservationCSVRepository _tourReservations;
        private ITourReadFromCSVRepository _tours;
        private IUserCSVRepository _users;

        public TourReservationService()
        {
            _tourReservations = new TourReservationCSVRepository();
            //_tourReservations = Injection.Injector.CreateInstance<ITourReservationRepository>();
            _tours = new TourReadFromCSVRepository();
            //_tours = Injection.Injector.CreateInstance<ITourReadFromCSVRepository>();
            _users = new UserCSVRepository();
            //_users = Injection.Injector.CreateInstance<IUserCSVRepository>();
        }

        public List<TourReservation> GetAll()
        {
            return _tourReservations.GetAll();
        }

        public List<TourReservation> GetNotConfirmedParticipation()//new method for guest2
        {
            return _tourReservations.GetNotConfirmedParticipation();
        }

        public void Create(TourReservation reservation)
        {
            _tourReservations.Add(reservation);
        }

        public void Update(TourReservation reservation)
        {
            _tourReservations.Update(reservation);
        }

        public void ConfirmReservation(TourReservation tourReservation, bool confirmed)//new method for guest2
        {
            _tourReservations.ConfirmReservation(tourReservation, confirmed);
        }

        public List<TourReservation> GetByUser(User user)//new method for guest2
        {
            return _tourReservations.GetByUser(user);
        }

        public bool CheckVouchers(TourReservation tourReservation)//new method for guest2
        {
            return _tourReservations.CountReservationsByUser(tourReservation);
        }

        public void Subscribe(IObserver observer)
        {
            _tourReservations.Subscribe(observer);
        }

        public void Save()
        {
            _tourReservations.Save();
        }

        public List<TourReservation> GetByToursid(int id)
        {
            return _tourReservations.GetByToursId(id);
        }

        public int GetAgeStatistics(Tour selectedTour, string ageGroup)
        {
            return _tourReservations.GetAgeStatistics(selectedTour, ageGroup);
        }

        public int GetVoucherStatistics(Tour selectedTour, bool used)
        {
            return _tourReservations.GetVoucherStatistics(selectedTour, used);
        }

        public void GetAttendedGuestsNumber(User loggedInGuide)
        {
            _tourReservations.CalculateAttendedGuestsNumber(loggedInGuide, _tours.GetFinished(loggedInGuide));
        }

        public List<User> GetGuestsWithReservations(KeyPoint keyPoint, List<User> markedGuests)
        {
            return _tourReservations.GetGuestsWithReservations(keyPoint, markedGuests, _users.GetAll());
        }
    }
}
