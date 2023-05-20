using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourRequestCSVRepository
    {
        public int NextId();
        public void Add(TourRequest request);
        public void Subscribe(IObserver observer);
        public void NotifyObservers();
        public void Save();
        void UpdateState(TourRequest selectedRequest, RequestsState requestsState);
        public List<TourRequest> GetAll();
        public List<TourRequest> GetFiltered(string locationSearchTerm, string guestNumberSearchTerm, string languageSearchTerm, string dateStartSearchTerm, string dateEndSearchTerm);
        public List<TourRequest> GetOnHold();
        public List<TourRequest> GetByUser(User user);
        public void CheckExpirationDate(User user);
        public List<TourRequest> GetByLocation(Location location);
        public List<TourRequest> GetByLanguage(string language);
    }
}
