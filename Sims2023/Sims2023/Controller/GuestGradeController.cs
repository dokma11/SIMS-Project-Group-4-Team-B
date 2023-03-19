using Sims2023.Model;
using Sims2023.Model.DAO;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Controller
{
    internal class GuestGradeController
    {
        private GuestGradeDAO _grade;

        public GuestGradeController()
        {
            _grade = new GuestGradeDAO();
        }

        public List<GuestGrade> GetAllGrades()
        {
            return _grade.GetAll();
        }




        public void Create(GuestGrade grade)
        {
            _grade.Add(grade);
        }

        public void Delete(GuestGrade grade)
        {
            _grade.Remove(grade);
        }

        public void Update(GuestGrade grade)
        {
            _grade.Update(grade);
        }

        public void Subscribe(IObserver observer)
        {
            _grade.Subscribe(observer);
        }

        public bool IsValid(string cleaN, string Respectrules, string Communicationn, string comment)
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
