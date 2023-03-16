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
    internal class GuestGradeController
    {
        private GuestGradeDAO grades;

        public GuestGradeController()
        {
            grades = new GuestGradeDAO();
        }

        public List<GuestGrade> GetAllGrades()
        {
            return grades.GetAll();
        }




        public void Create(GuestGrade grade)
        {
            grades.Add(grade);
        }

        public void Delete(GuestGrade grade)
        {
            grades.Remove(grade);
        }

        public void Update(GuestGrade grade)
        {
            grades.Update(grade);
        }

        public void Subscribe(IObserver observer)
        {
            grades.Subscribe(observer);
        }

    }
}
