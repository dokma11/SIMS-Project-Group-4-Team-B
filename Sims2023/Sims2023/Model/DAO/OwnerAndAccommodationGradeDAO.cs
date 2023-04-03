using Sims2023.Controller;
using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model.DAO
{
    public class OwnerAndAccommodationGradeDAO : ISubject
    {
        private List<IObserver> _observers;

        private GuestGradeController gradeController;
        private OwnerAndAccommodationGradeFileHandler _fileHandler;
        private List<OwnerAndAccommodationGrade> _grades;
        private GuestFileHandler fh;



        public OwnerAndAccommodationGradeDAO()
        {
            _fileHandler = new OwnerAndAccommodationGradeFileHandler();
            _grades = _fileHandler.Load();
            _observers = new List<IObserver>();
         
            fh = new GuestFileHandler();
            gradeController = new GuestGradeController();
        }







        public int NextId()
        {
            if (_grades.Count == 0) return 1;
            return _grades.Max(s => s.Id) + 1;
        }

        public void Add(OwnerAndAccommodationGrade grade)
        {
            grade.Id = NextId();
            _grades.Add(grade);
            _fileHandler.Save(_grades);
            NotifyObservers();
        }

        public void Remove(OwnerAndAccommodationGrade accommodation)
        {
            _grades.Remove(accommodation);
            _fileHandler.Save(_grades);
            NotifyObservers();
        }

        private void AddNameSurrnameToReservation(List<Guest> ListOfGuests, List<OwnerAndAccommodationGrade> grades)
        {
            foreach (var grade in grades)
            {
                foreach (var guest in ListOfGuests)
                {
                    if (grade.GuestId == guest.Id)
                    {
                        grade.Name = guest.Name;
                        grade.Surrname = guest.Surrname;
                    }
                }
            }
        }

        public List<OwnerAndAccommodationGrade> GetAllGuestsWhoGraded(List<OwnerAndAccommodationGrade> people, List<Guest> ListOfGuests)
        {
            AddNameSurrnameToReservation(ListOfGuests, people);
            RemoveUngradedGuests(people, gradeController.GetAllGrades());
            return people;

        }

        private void RemoveUngradedGuests(List<OwnerAndAccommodationGrade> people, List<GuestGrade> guestGrades)
        {
            for (int i = people.Count - 1; i >= 0; i--)
            {
                var person = people[i];
                if (!guestGrades.Any(g => g.GuestId == person.GuestId))
                {
                    people.RemoveAt(i);
                }
            }
        }


 

        public void Update(OwnerAndAccommodationGrade accommodation)
        {
            int index = _grades.FindIndex(p => p.Id == accommodation.Id);
            if (index != -1)
            {
                _grades[index] = accommodation;
            }

            _fileHandler.Save(_grades);
            NotifyObservers();
        }

        public List<OwnerAndAccommodationGrade> GetAll()
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
