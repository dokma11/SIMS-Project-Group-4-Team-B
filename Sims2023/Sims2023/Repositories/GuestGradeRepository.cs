using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    class GuestGradeRepository : ISubject, IGuestGradeRepository
    {
        private List<IObserver> _observers;

        private GuestGradeFileHandler _fileHandler;
        private List<GuestGrade> _grades;

       public GuestGradeRepository()
        {
            _fileHandler = new GuestGradeFileHandler();
            _grades = _fileHandler.Load();
            _observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (_grades.Count == 0) return 1;
            return _grades.Max(s => s.Id) + 1;
        }

        public void Add(GuestGrade grade)
        {
            grade.Id = NextId();
            _grades.Add(grade);
            _fileHandler.Save(_grades);
            NotifyObservers();
        }



        public void Remove(GuestGrade grade)
        {
            _grades.Remove(grade);
            _fileHandler.Save(_grades);
            NotifyObservers();
        }

        public void Update(GuestGrade AccLoc)
        {
            int index = _grades.FindIndex(p => p.Id == AccLoc.Id);
            if (index != -1)
            {
                _grades[index] = AccLoc;
            }

            _fileHandler.Save(_grades);
            NotifyObservers();
        }

        public List<GuestGrade> GetAll()
        {
            return _grades;
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
