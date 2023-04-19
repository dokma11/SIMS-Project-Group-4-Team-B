using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.RepositoryInterfaces;

namespace Sims2023.Repositories
{
    internal class AccommodationGradeCSVRepository : ISubject, IAccommodationGradeCSVRepository
    {

        private List<IObserver> _observers;

        private AccommodationGradeFileHandler _fileHandler;
        private List<AccommodationGrade> _accommodationGrades;

        public AccommodationGradeCSVRepository()
        {
            _fileHandler = new AccommodationGradeFileHandler();
            _accommodationGrades = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public List<AccommodationGrade> GetAllGuestsWhoGraded(List<AccommodationGrade> people, List<GuestGrade> ListOfGuests, User owner)
        {
            FindGradesForOwner(people, owner);
            RemoveUngradedGuests(people, ListOfGuests);
            return people;

        }
        public void FindGradesForOwner(List<AccommodationGrade> people, User owner)
        {
            people.RemoveAll(r => r.Accommodation.Owner.Id != owner.Id);
        }


        public void RemoveUngradedGuests(List<AccommodationGrade> people, List<GuestGrade> guestGrades)
        {
            for (int i = people.Count - 1; i >= 0; i--)
            {
                var person = people[i];
                if (!guestGrades.Any(g => g.Guest.Id == person.Guest.Id))
                {
                    people.RemoveAt(i);
                }
            }
        }

        public double FindAverage(AccommodationGrade grade)
        {
            double prosjek;
            prosjek = (grade.Cleanliness + grade.Comfort + grade.Location + grade.Owner + grade.ValueForMoney) / 5;
            return prosjek;
        }
        public int NextId()
        {
            if (_accommodationGrades.Count == 0) return 1;
            return _accommodationGrades.Max(s => s.Id) + 1;
        }

        public void Add(AccommodationGrade accommodationGrade)
        {
            accommodationGrade.Id = NextId();
            _accommodationGrades.Add(accommodationGrade);
            _fileHandler.Save(_accommodationGrades);
            NotifyObservers();
        }

        public void Remove(AccommodationGrade accommodationGrade)
        {
            _accommodationGrades.Remove(accommodationGrade);
            _fileHandler.Save(_accommodationGrades);
            NotifyObservers();
        }

        public void Update(AccommodationGrade accommodationGrade)
        {
            int index = _accommodationGrades.FindIndex(p => p.Id == accommodationGrade.Id);
            if (index != -1)
            {
                _accommodationGrades[index] = accommodationGrade;
            }

            _fileHandler.Save(_accommodationGrades);
            NotifyObservers();
        }
        public List<AccommodationGrade> GetAll()
        {
            return _accommodationGrades;
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
