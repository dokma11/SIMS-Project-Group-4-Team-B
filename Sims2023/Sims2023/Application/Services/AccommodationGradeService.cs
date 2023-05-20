using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Repositories;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Application.Injection;

namespace Sims2023.Application.Services
{
    public class AccommodationGradeService
    {
        private IUserCSVRepository _user;
        private IAccommodationCSVRepository _accommodation;
        private ILocationCSVRepository _location;
        private IAccommodationGradeCSVRepository _accommodationGrade;

        public AccommodationGradeService()
        {
            _location = Injector.CreateInstance<ILocationCSVRepository>();
            _user = Injector.CreateInstance<IUserCSVRepository>();
            _accommodation = Injector.CreateInstance<IAccommodationCSVRepository>();
            _accommodationGrade = Injector.CreateInstance<IAccommodationGradeCSVRepository>();
            GetReservationReferences();
        }


        public void GetReservationReferences()
        {
            foreach (var item in GetAllAccommodationGrades())
            {
                var accommodation = _accommodation.GetById(item.Accommodation.Id);
                var owner = _user.GetById(accommodation.Owner.Id);
                var location = _location.GetById(accommodation.Location.Id);
                var user = _user.GetById(item.Guest.Id);
                if (accommodation != null)
                {
                    item.Guest = user;
                    item.Accommodation = accommodation;
                    item.Accommodation.Owner = owner;
                    item.Accommodation.Location = location;

                }
            }
        }
        public List<AccommodationGrade> GetAllAccommodationGrades()
        {
            return _accommodationGrade.GetAll();
        }

        public List<AccommodationGrade> FindAllGuestsWhoGraded(List<AccommodationGrade> grades, List<GuestGrade> gradedGuests, User user)
        {
            return _accommodationGrade.GetAllGuestsWhoGraded(grades, gradedGuests, user);
        }

        public void Create(AccommodationGrade accommodationGrade)
        {
            _accommodationGrade.Add(accommodationGrade);
        }

        public void Delete(AccommodationGrade accommodationGrade)
        {
            _accommodationGrade.Remove(accommodationGrade);
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationGrade.Subscribe(observer);
        }

        internal void Update(AccommodationGrade grade)
        {
            _accommodationGrade.Update(grade);
        }

        public AccommodationGrade FindGrade(AccommodationReservation selectedAccommodationReservation)
        {
            return _accommodationGrade.FindGrade(selectedAccommodationReservation);
        }
    }
}
