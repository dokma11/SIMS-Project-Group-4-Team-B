using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class RequestService
    {
        private IRequestCSVRepository _request;

        public RequestService()
        {
            _request = new RequestCSVRepository();
            //_request = Injection.Injector.CreateInstance<IRequestCSVRepository>();
        }

        public void Create(Request request)
        {
            _request.Add(request);
        }

        public List<Request> GetAll()
        {
            return _request.GetAll();
        }

        public Request GetById(int id)
        {
            return _request.GetById(id);
        }

        public List<Request> GetOnHold()
        {
            return _request.GetOnHold();
        }

        public void Save()
        {
            _request.Save();
        }

        public void Subscribe(IObserver observer)
        {
            _request.Subscribe(observer);
        }

        public List<Request> GetFiltered(string locationSearchTerm, string guestNumberSearchTerm, string languageSearchTerm, string dateStartSearchTerm, string dateEndSearchTerm)
        {
            return _request.GetFiltered(locationSearchTerm, guestNumberSearchTerm, languageSearchTerm, dateStartSearchTerm, dateEndSearchTerm);
        }

        public void UpdateState(Request selectedRequest, RequestsState requestsState)
        {
            _request.UpdateState(selectedRequest, requestsState);
        }

        public int GetMonthlyLanguageStatistics(string language, int ordinal, string year)
        {
            return _request.GetMonthlyLanguageStatistics(language, ordinal, year);
        }
        
        public int GetYearlyLanguageStatistics(string language, string year)
        {
            return _request.GetYearlyLanguageStatistics(language, year);
        }
        
        public int GetMonthlyLocationStatistics(string location, int ordinal, string year)
        {
            return _request.GetMonthlyLocationStatistics(location, ordinal, year);
        }
        
        public int GetYearlyLocationStatistics(string location, string year)
        {
            return _request.GetYearlyLocationStatistics(location, year);
        }

        public List<RequestsLanguage> GetLanguages()
        {
            return _request.GetLanguages();
        }
        
        public List<string> GetLocations()
        {
            return _request.GetLocations();
        }

        public List<string> GetYears()
        {
            return _request.GetYears();
        }

        public RequestsLanguage GetTheMostRequestedLanguage()
        {
            return _request.GetTheMostRequestedLanguage();
        }

        public Location GetTheMostRequestedLocation()
        {
            return _request.GetTheMostRequestedLocation();
        }

        public List<Request> GetByUser(User user)
        {
            return _request.GetByUser(user);
        }

        public void CheckExpirationDate(User user)
        {
            _request.CheckExpirationDate(user);
        }

        public List<Tour> GetRequestedTours(User user)
        {
            return _request.GetRequestedTours(user);
        }
    }
}
