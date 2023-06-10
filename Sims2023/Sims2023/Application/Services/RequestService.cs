using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class RequestService
    {
        private ITourRequestCSVRepository _tourRequest;
        private ITourRequestStatisticCSVRepository _tourRequestStatistic;
        private ILocationCSVRepository _location;
        private IUserCSVRepository _user;

        public RequestService()
        {
            _tourRequest = Injection.Injector.CreateInstance<ITourRequestCSVRepository>();
            _location = Injection.Injector.CreateInstance<ILocationCSVRepository>();
            _tourRequestStatistic = Injection.Injector.CreateInstance<ITourRequestStatisticCSVRepository>();
            _user = Injection.Injector.CreateInstance<IUserCSVRepository>();

            GetUserReferences();
            GetUserReferencesForStatistics();
            GetLocationReferences();
            GetLocationReferencesForStatistics();
        }

        public TourRequest GetById(int id)
        {
            return _tourRequest.GetById(id);
        }

        public void Create(TourRequest request)
        {
            _tourRequest.Add(request);
        }

        public List<TourRequest> GetOnHold()
        {
            GetLocationReferences();
            GetUserReferences();
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
            GetLocationReferencesForStatistics();
            GetUserReferencesForStatistics();
            int ret = _tourRequestStatistic.GetMonthlyStatistics("location", location, year, ordinal);
            return ret;
        }

        public int GetYearlyLocationStatistics(string location, string year)
        {
            GetLocationReferencesForStatistics();
            GetUserReferencesForStatistics();
            int ret =  _tourRequestStatistic.GetYearlyStatistics("location", location, year);
            return ret;
        }


        public List<string> GetComboBoxData(string purpose)
        {
            GetLocationReferencesForStatistics();
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
            return _tourRequestStatistic.GetYearlyFilteredTourRequestsByUser(user, year, state);
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

        public void GetLocationReferences()
        {
            foreach (var request in GetAll())
            {
                request.Location = _location.GetById(request.Location.Id) ?? request.Location;
            }
        }

        public void GetLocationReferencesForStatistics()
        {
            foreach (var request in GetAllStatistics())
            {
                request.Location = _location.GetById(request.Location.Id) ?? request.Location;
            }
        }

        public void GetUserReferences()
        {
            foreach (var request in GetAll())
            {
                request.Guest = _user.GetById(request.Guest.Id) ?? request.Guest;
            }
        }

        public void GetUserReferencesForStatistics()
        {
            foreach (var request in GetAllStatistics())
            {
                request.Guest = _user.GetById(request.Guest.Id) ?? request.Guest;
            }
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequest.GetAll();
        }

        public List<TourRequest> GetAllStatistics()
        {
            return _tourRequestStatistic.GetAll();
        }

        public double GetUsersAcceptedPercentageByYearAndLanguage(string year, string language)
        {
            return _tourRequestStatistic.GetUsersAcceptedPercentageByYearAndLanguage(year, language);
        }
        
        public double GetUsersDeclinedPercentageByYearAndLanguage(string year, string language)
        {
            return _tourRequestStatistic.GetUsersDeclinedPercentageByYearAndLanguage(year, language);
        }

        public int GetByLanguageCount(string year, string requestsLanguage)
        {
            return _tourRequest.GetByLanguageCount(year, requestsLanguage); 
        }
        
        public double GetUsersAcceptedPercentageByYearAndLocation(string year, string location)
        {
            return _tourRequestStatistic.GetUsersAcceptedPercentageByYearAndLocation(year, location);
        }
        
        public double GetUsersDeclinedPercentageByYearAndLocation(string year, string location)
        {
            return _tourRequestStatistic.GetUsersDeclinedPercentageByYearAndLocation(year, location);
        }

        public int GetByLocationCount(string year, string location)
        {
            return _tourRequest.GetByLocationCount(year, location); 
        }

        public void Save()
        {
            _tourRequest.Save();
            _tourRequestStatistic.Save();
            GetLocationReferences();
            GetUserReferences();
        }
    }
}
