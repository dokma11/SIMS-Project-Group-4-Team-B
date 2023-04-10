using Sims2023.Model.DAO;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;

namespace Sims2023.Controller
{
    public class AccommodationGradeController
    {
        private AccommodationGradeDAO _accommodationGrade;

        public AccommodationGradeController()
        {
            _accommodationGrade = new AccommodationGradeDAO();
        }

        public List<AccommodationGrade> GetAllAccommodationGrades()
        {
            return _accommodationGrade.GetAll();
        }

        public List<AccommodationGrade> FindAllGuestsWhoGraded(List<AccommodationGrade> grades, List<GuestGrade> gradedGuests,User user)
        {
            return _accommodationGrade.GetAllGuestsWhoGraded(grades, gradedGuests,user);
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
    }
}
