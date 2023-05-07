using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IAccommodationGradeCSVRepository
    {
        public List<AccommodationGrade> GetAllGuestsWhoGraded(List<AccommodationGrade> people, List<GuestGrade> ListOfGuests, User owner);
        public void FindGradesForOwner(List<AccommodationGrade> people, User owner);
        public void RemoveUngradedGuests(List<AccommodationGrade> people, List<GuestGrade> guestGrades);
        public double FindAverage(AccommodationGrade grade);
        public int NextId();
        public void Add(AccommodationGrade accommodationGrade);
        public void Remove(AccommodationGrade accommodationGrade);
        public void Update(AccommodationGrade accommodationGrade);
        public List<AccommodationGrade> GetAll();
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
        AccommodationGrade FindGrade(AccommodationReservation selectedAccommodationReservation);
    }
}
