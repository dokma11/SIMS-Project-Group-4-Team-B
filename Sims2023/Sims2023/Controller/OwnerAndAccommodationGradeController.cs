using Sims2023.DAO;
using Sims2023.Model;
using Sims2023.Model.DAO;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Controller
{
    public class OwnerAndAccommodationGradeController
    {
        private OwnerAndAccommodationGradeDAO _grade;

        public OwnerAndAccommodationGradeController()
        {
            _grade = new OwnerAndAccommodationGradeDAO();
        }

        public List<OwnerAndAccommodationGrade> GetAllGrades()
        {
            return _grade.GetAll();
        }

        public List<OwnerAndAccommodationGrade> FindAllGuestsWhoGraded(List<OwnerAndAccommodationGrade> grades, List<Guest> ListOfGuests)
        {
           return  _grade.GetAllGuestsWhoGraded(grades, ListOfGuests);
        }


        public void Create(OwnerAndAccommodationGrade accommodation)
        {
            _grade.Add(accommodation);
        }

        public void Delete(OwnerAndAccommodationGrade accommodation)
        {
            _grade.Remove(accommodation);
        }

        public void Update(OwnerAndAccommodationGrade accommodation)
        {
            _grade.Update(accommodation);
        }

        public void Subscribe(IObserver observer)
        {
            _grade.Subscribe(observer);
        }
    }
}
