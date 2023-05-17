using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IRequestCSVRepository
    {
        public int NextId();
        public void Add(Request request);
        public List<Request> GetOnHold();
        public void Subscribe(IObserver observer);
        public void NotifyObservers();
        public void Save();
        public List<Request> GetFiltered(string locationSearchTerm, string guestNumberSearchTerm, string languageSearchTerm, string dateStartSearchTerm, string dateEndSearchTerm);
        void UpdateState(Request selectedRequest, RequestsState requestsState);
        public RequestsLanguage GetTheMostRequestedLanguage();
        public int GetTheMostRequestedLocation();
        public List<Request> GetByUser(User user);
        public void CheckExpirationDate(User user);

        public List<Request> GetAcceptedTourRequestsByUser(User user);
        public double AcceptedTourRequestPercentageByUser(User user);

        public List<Request> GetYearlyAcceptedTourRequestsByUser(User user,int year);

        public List<string> GetComboBoxData(string purpose);
        public int GetYearlyStatistics(string purpose, string statFor, string year);
        public int GetMonthlyStatistics(string purpose, string statFor, string year, int ordinal);
        public List<Request> GetByLocation(Location location);
        public List<Request> GetByLanguage(string language);
    }
}
