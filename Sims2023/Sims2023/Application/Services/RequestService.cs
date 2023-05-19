using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class RequestService
    {
        private ITourRequestCSVRepository _tourRequest;
        private ITourRequestStatisticCSVRepository _tourRequestStatistic;
        private ILocationCSVRepository _location;

        public RequestService()
        {
            _tourRequest = new TourRequestCSVRepository();
            //_request = Injection.Injector.CreateInstance<IRequestCSVRepository>();
            _location = new LocationCSVRepository();
            //_location = Injection.Injector.CreateInstance<ILocationCSVRepository>();
            _tourRequestStatistic = new TourRequestStatisticCSVRepository();
           // _tourRequestStatistic = Injection.Injector.CreateInstance<ITourRequestStatisticCSVRepository>();
        }

        public void Create(TourRequest request)
        {
            _tourRequest.Add(request);
        }

        public List<TourRequest> GetOnHold()
        {
            return _tourRequest.GetOnHold();
        }

        public void Subscribe(IObserver observer)
        {
            _tourRequest.Subscribe(observer);
        }

        public List<TourRequest> GetFiltered(string locationSearchTerm, string guestNumberSearchTerm, string languageSearchTerm, string dateStartSearchTerm, string dateEndSearchTerm)
        {
            return _tourRequest.GetFiltered(locationSearchTerm, guestNumberSearchTerm, languageSearchTerm, dateStartSearchTerm, dateEndSearchTerm);
        }

        public void UpdateState(TourRequest selectedRequest, RequestsState requestsState)
        {
            _tourRequest.UpdateState(selectedRequest, requestsState);
        }

        public int GetMonthlyLanguageStatistics(string language, int ordinal, string year)
        {
            return _tourRequestStatistic.GetMonthlyStatistics("language", language, year, ordinal);
        }
        
        public int GetYearlyLanguageStatistics(string language, string year)
        {
            return _tourRequestStatistic.GetYearlyStatistics("language", language, year);
        }
        
        public int GetMonthlyLocationStatistics(string location, int ordinal, string year)
        {
            return _tourRequestStatistic.GetMonthlyStatistics("location", location, year, ordinal);
        }
        
        public int GetYearlyLocationStatistics(string location, string year)
        {
            return _tourRequestStatistic.GetYearlyStatistics("location", location, year);
        }

       
        public List<string> GetComboBoxData(string purpose)
        {
            return _tourRequestStatistic.GetComboBoxData(purpose);
        }

        public RequestsLanguage GetTheMostRequestedLanguage()
        {
            return _tourRequestStatistic.GetTheMostRequestedLanguage();
        }

        public Location GetTheMostRequestedLocation()
        {
            return _location.GetById(_tourRequestStatistic.GetTheMostRequestedLocation());
        }

        public List<TourRequest> GetByUser(User user)
        {
            return _tourRequest.GetByUser(user);
        }

        public void CheckExpirationDate(User user)
        {
            _tourRequest.CheckExpirationDate(user);
        }

        public List<TourRequest> GetAcceptedTourRequestsByUser(User user)
        {
            return _tourRequestStatistic.GetUsersAccepted(user);
        }

        
        public List<TourRequest> GetYearlyFilteredTourRequestsByUser(User user, int year, string state)
        {
            return _tourRequestStatistic.GetYearlyFilteredTourRequestsByUser(user,year,state);
        }

        public int GetYearlyStatisticByUser(User user, string statFor, string year, string purpose)
        {
            return _tourRequestStatistic.GetYearlyStatisticByUser(user, statFor, year, purpose);
        }

        public int GetAllTimeStatisticByUser(User user, string statFor, string purpose)
        {
            return _tourRequestStatistic.GetAllTimeStatisticByUser(user, statFor, purpose);
        }

        public double GetAverageYearlyAcceptedTourRequestGuestNumber(User user, int year)
        {
            return _tourRequestStatistic.GetAverageYearlyAcceptedTourRequestGuestNumber(user, year);
        }

        public double GetAverageAllTimeAcceptedTourRequestGuestNumber(User user)
        {
            return _tourRequestStatistic.GetAverageAllTimeAcceptedTourRequestGuestNumber(user);
        }

        public List<TourRequest> GetByLocation(Location location)
        {
            return _tourRequest.GetByLocation(location);
        }

        public List<TourRequest> GetByLanguage(string language)
        {
            return _tourRequest.GetByLanguage(language);
        }
    }
}
