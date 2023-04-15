using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class GuestGradeService
    {
        private IGuestGradeRepository _grade;

        public GuestGradeService()
        {
            _grade = new GuestGradeRepository();
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

    }
}
