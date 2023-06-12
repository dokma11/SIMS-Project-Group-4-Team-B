using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IAccommodationCancellationCSVRepository
    {
        public AccommodationCancellation GetById(int id);
        public int NextId();
        public void Add(AccommodationCancellation accommodationCancellation);
        public void Remove(AccommodationCancellation accommodationCancellation);
        public void Update(AccommodationCancellation accommodationCancellation);
        public List<AccommodationCancellation> GetAll();
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
        public List<AccommodationCancellation> FindReservationCancellationsInDateFrame(User user, DateTime startDateSelected, DateTime endDateSelected);
    }
}
