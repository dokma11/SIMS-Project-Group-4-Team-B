using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IRequestCSVRepository
    {
        public int NextId();
        public void Add(Request request);
        public List<Request> GetAll();
        public Request GetById(int id);
        public List<Request> GetOnHold();
        public void Subscribe(IObserver observer);
        public void NotifyObservers();
        public void Save();
        public List<Request> GetFiltered(string locationSearchTerm, string guestNumberSearchTerm, string languageSearchTerm, string dateStartSearchTerm, string dateEndSearchTerm);
        void UpdateState(Request selectedRequest, RequestsState requestsState);
        int GetMonthlyLanguageStatistics(string language, int ordinal, string year);
        int GetYearlyLanguageStatistics(string language, string year);
        int GetMonthlyLocationStatistics(string location, int ordinal, string year);
        int GetYearlyLocationStatistics(string location, string year);
        List<RequestsLanguage> GetLanguages();
        List<string> GetLocations();
        List<string> GetYears();
        public RequestsLanguage GetTheMostRequestedLanguage();
        public Location GetTheMostRequestedLocation();
        public List<Request> GetByUser(User user);

        public void CheckExpirationDate(User user);
        
        
        


    }
}
