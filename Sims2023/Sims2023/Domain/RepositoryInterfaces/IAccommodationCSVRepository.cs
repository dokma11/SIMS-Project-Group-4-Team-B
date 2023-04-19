using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Domain.RepositoryInterfaces
{
     public interface IAccommodationCSVRepository
    {
        public Accommodation GetById(int id);
        public int NextId();
        public void Add(Accommodation accommodation);
        public void Remove(Accommodation accommodation);
        public void Update(Accommodation accommodation);
        public List<Accommodation> GetAll();
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
        public void CheckSearchTermConditions(List<Accommodation> FilteredData, string nameSearchTerm, string citySearchTerm, string countrySearchTerm, string typeSearchTerm, int maxGuests, int minDays);
        public bool CheckSearchTerm(Accommodation accommodation, string nameSearchTerm, string citySearchTerm, string countrySearchTerm, string typeSearchTerm, int maxGuestsSearchTerm, int minDaysSearchTerm);
    }
}
