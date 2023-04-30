using Sims2023.Domain.Models;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IAccommodationRenovationCSVRepository
    {
        public void Add(AccommodationRenovation renovation);
        public void Update(AccommodationRenovation renovation);
        public void Remove(AccommodationRenovation renovation);
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
        public int NextId();
        public List<AccommodationRenovation> GetAll();
    }
}
