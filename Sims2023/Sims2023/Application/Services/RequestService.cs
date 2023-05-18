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
        private ILocationCSVRepository _location;

        public RequestService()
        {
            _request = new RequestCSVRepository();
            //_request = Injection.Injector.CreateInstance<IRequestCSVRepository>();
            _location = new LocationCSVRepository();
            //_location = Injection.Injector.CreateInstance<ILocationCSVRepository>();
        }

        public void Create(Request request)
        {
            _request.Add(request);
        }

        public List<Request> GetOnHold()
        {
            return _request.GetOnHold();
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
            return _request.GetMonthlyStatistics("language", language, year, ordinal);
        }
        
        public int GetYearlyLanguageStatistics(string language, string year)
        {
            return _request.GetYearlyStatistics("language", language, year);
        }
        
        public int GetMonthlyLocationStatistics(string location, int ordinal, string year)
        {
            return _request.GetMonthlyStatistics("location", location, year, ordinal);
        }
        
        public int GetYearlyLocationStatistics(string location, string year)
        {
            return _request.GetYearlyStatistics("location", location, year);
        }

        public List<string> GetComboBoxData(string purpose)
        {
            return _request.GetComboBoxData(purpose);
        }

        public RequestsLanguage GetTheMostRequestedLanguage()
        {
            return _request.GetTheMostRequestedLanguage();
        }

        public Location GetTheMostRequestedLocation()
        {
            return _location.GetById(_request.GetTheMostRequestedLocation());
        }

        public List<Request> GetByUser(User user)
        {
            return _request.GetByUser(user);
        }

        public void CheckExpirationDate(User user)
        {
            _request.CheckExpirationDate(user);
        }

        public List<Request> GetAcceptedTourRequestsByUser(User user)
        {
            return _request.GetAcceptedTourRequestsByUser(user);
        }

        public List<Request> GetYearlyAcceptedTourRequestsByUser(User user, int year)
        {
            return _request.GetYearlyAcceptedTourRequestsByUser(user, year);
        }

        public int GetYerlyStatisticByUser(User user, string location, string year)
        {
            return _request.GetYerlyStatisticByUser(user, location, year);
        }

        public int GetAllTimeLocationStatisticByUser(User user, string location)
        {
            return _request.GetAllTimeLocationStatisticByUser(user, location);
        }

        public int GetAllTimeLanguageStatisticByUser(User user, string language)
        {
            return _request.GetAllTimeLanguageStatisticByUser(user, language);
        }

        public int GetYearlyLanguageStatisticByUser(User user,string language,string year)
        {
            return _request.GetYearlyLanguageStatisticByUser(user, language, year);
        }


    }
}
