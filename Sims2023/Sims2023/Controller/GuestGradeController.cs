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

        public bool isValid(string cleaN, string Respectrules, string Communicationn, string comment)
        {
            int clean;
            bool isCleanValid = int.TryParse(cleaN, out clean);
            int RespectRules;
            bool isRulesValid = int.TryParse(Respectrules, out RespectRules);
            int Communication;
            bool isCommunicationValid = int.TryParse(Communicationn, out Communication);

            if (!isCleanValid)
            {
               
                return false;
            }


            else if (!isRulesValid)
            {
              
                return false;
            }

            else if (!isCommunicationValid)
            {
              
                return false;
            }

            else if (string.IsNullOrEmpty(comment)) return false;

            else return true;


        }

    }
}
