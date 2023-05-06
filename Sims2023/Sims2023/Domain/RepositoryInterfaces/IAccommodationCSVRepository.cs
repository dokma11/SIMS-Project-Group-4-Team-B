using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IAccommodationCSVRepository
    {
        public Accommodation GetById(int id);
        public int NextId();
        public void Add(Accommodation accommodation);
        public void Remove(Accommodation accommodation);

        public void MarkRenovated(List<AccommodationRenovation> renovations);
        public void Update(Accommodation accommodation);
        public List<Accommodation> GetAll();
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
        public List<Accommodation> GetOwnerAccommodations(List<Accommodation> accommodations, User user);
        public void CheckSearchTermConditions(List<Accommodation> FilteredData, string nameSearchTerm, string citySearchTerm, string countrySearchTerm, string typeSearchTerm, int maxGuests, int minDays);
    }
}
