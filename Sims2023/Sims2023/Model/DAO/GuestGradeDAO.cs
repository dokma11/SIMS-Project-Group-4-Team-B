using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model.DAO
{
     class GuestGradeDAO : ISubject
    {
        private List<IObserver> _observers;

        private GuestGradeFileHandler _fileHandler;
        private List<GuestGrade> grades;



        public GuestGradeDAO()
        {
            _fileHandler = new GuestGradeFileHandler();
            grades = _fileHandler.Load();
            _observers = new List<IObserver>();
        }



        public int NextId()
        {
            return grades.Max(s => s.Id) + 1;
        }

        public void Add(GuestGrade grade)
        {
            grade.Id = NextId();
            grades.Add(grade);
            _fileHandler.Save(grades);
            NotifyObservers();
        }

        public void Remove(GuestGrade grade)
        {
            grades.Remove(grade);
            _fileHandler.Save(grades);
            NotifyObservers();
        }

        public void Update(GuestGrade AccLoc)
        {
            int index = grades.FindIndex(p => p.Id == AccLoc.Id);
            if (index != -1)
            {
                grades[index] = AccLoc;
            }

            _fileHandler.Save(grades);
            NotifyObservers();
        }

        public List<GuestGrade> GetAll()
        {
            return grades;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
