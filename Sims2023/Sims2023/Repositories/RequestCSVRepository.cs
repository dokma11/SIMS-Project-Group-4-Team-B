using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class RequestCSVRepository : IRequestCSVRepository
    {
        private List<IObserver> _observers;
        private List<Request> _requests;
        private RequestFileHandler _fileHandler;

        public RequestCSVRepository()
        {
            _fileHandler = new RequestFileHandler();
            _requests = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            return _requests.Count == 0 ? 1 : _requests.Max(r => r.Id) + 1;
        }

        public void Add(Request request)
        {
            request.Id = NextId();
            _requests.Add(request);
            _fileHandler.Save(_requests);
            NotifyObservers();
        }

        public List<Request> GetOnHold()
        {
            return _requests.Where(r => r.State == RequestsState.OnHold).ToList();
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Save()
        {
            _fileHandler.Save(_requests);
        }

        public List<Request> GetFiltered(string locationSearchTerm, string guestNumberSearchTerm, string languageSearchTerm, string dateStartSearchTerm, string dateEndSearchTerm)
        {
            return GetOnHold().Where(request =>
                   (string.IsNullOrEmpty(locationSearchTerm) || request.Location.City.ToLower().Contains(locationSearchTerm.ToLower())) &&
                   (string.IsNullOrEmpty(guestNumberSearchTerm) || request.GuestNumber.ToString().ToLower().Contains(guestNumberSearchTerm.ToLower())) &&
                   (string.IsNullOrEmpty(languageSearchTerm) || request.Language.ToString().ToLower().Contains(languageSearchTerm.ToLower())) &&
                   (string.IsNullOrEmpty(dateStartSearchTerm) || request.Start >= DateTime.Parse(dateStartSearchTerm)) &&
                   (string.IsNullOrEmpty(dateEndSearchTerm) || request.End <= DateTime.Parse(dateEndSearchTerm))).ToList();
        }

        public void UpdateState(Request selectedRequest, RequestsState requestsState)
        {
            selectedRequest.State = requestsState;
            Save();
        }

        public int GetYearlyStatistics(string purpose, string statFor, string year)
        {
            if (purpose == "location")
            {
                return _requests.Count(r => r.Start.Year.ToString() == year && (r.Location.City + ", " + r.Location.Country) == statFor);
            }
            else
            {
                return _requests.Count(r => r.Start.Year.ToString() == year && r.Language.ToString() == statFor);
            }
        }

        public int GetYerlyStatisticByUser(User user,string location,string year)
        {
            return _requests.Count(r => r.Start.Year.ToString() == year && r.Guest.Id == user.Id && (r.Location.City + ", " + r.Location.Country) == location);
        }

        public int GetAllTimeLocationStatisticByUser(User user,string location)
        {
            return _requests.Count(r=>r.Guest.Id == user.Id && (r.Location.City + ", " + r.Location.Country) == location);
        }

        public int GetAllTimeLanguageStatisticByUser(User user, string language)
        {
            return _requests.Count(r => r.Guest.Id == user.Id && r.Language.ToString() == language);
        }

        public int GetYearlyLanguageStatisticByUser(User user, string language, string year)
        {
            return _requests.Count(r => r.Start.Year.ToString() == year && r.Guest.Id == user.Id && r.Language.ToString() == language);
        }

        public int GetMonthlyStatistics(string purpose, string statFor, string year, int ordinal)
        {
            if (purpose == "location")
            {
                return _requests.Count(r => r.Start.Month == ordinal && r.Start.Year.ToString() == year && (r.Location.City + ", " + r.Location.Country) == statFor);
            }
            else
            {
                return _requests.Count(r => r.Language.ToString() == statFor && r.Start.Year.ToString() == year && r.Start.Month == ordinal);
            }
        }

        public List<string> GetComboBoxData(string purpose)
        {
            if (purpose == "years")
            {
                return _requests.Select(r => r.Start.Year.ToString()).Distinct().Prepend("Svih vremena").ToList();
            }
            else if (purpose == "locations")
            {
                return _requests.Select(r => $"{r.Location.City}, {r.Location.Country}").Distinct().ToList();
            }
            else
            {
                return _requests.Select(r => r.Language.ToString()).Distinct().ToList();
            }
        }

        public RequestsLanguage GetTheMostRequestedLanguage()
        {
            var lastYearStartDate = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day);
            var lastYearEndDate = DateTime.Today;

            var requestsInLastYear = _requests
                .Where(r => r.Start >= lastYearStartDate && r.Start <= lastYearEndDate)
                .GroupBy(r => r.Language)
                .ToDictionary(g => g.Key, g => g.Count());

            var maxCount = requestsInLastYear.Values.Max();
            return requestsInLastYear.FirstOrDefault(r => r.Value == maxCount).Key;
        }

        public int GetTheMostRequestedLocation()
        {
            var lastYearStartDate = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day);
            var lastYearEndDate = DateTime.Today;

            var requestsInLastYear = _requests
                .Where(r => r.Start >= lastYearStartDate && r.Start <= lastYearEndDate)
                .GroupBy(r => r.Location.Id)
                .ToDictionary(g => g.Key, g => g.Count());

            var maxCount = requestsInLastYear.Values.Max();
            return requestsInLastYear.FirstOrDefault(r => r.Value == maxCount).Key;
        }

        public List<Request> GetByUser(User user)
        {
            return _requests.Where(r => r.Guest.Id == user.Id).ToList();
        }

        
       
        public void CheckExpirationDate(User user)
        {
            foreach(Request request in GetByUser(user).Intersect(GetOnHold()))
            {
                TimeSpan tillExpiration = request.Start - DateTime.Now;
                if (tillExpiration.TotalHours < 48)
                {
                    request.State = RequestsState.Invalid;
                    Save();
                }
            }
        }

        

        public List<Request> GetAcceptedTourRequestsByUser(User user)
        {
            return GetByUser(user).Where(r=> r.State==RequestsState.Accepted).ToList(); 
        }

        

        public List<Request> GetYearlyAcceptedTourRequestsByUser(User user, int year)
        {
            return GetAcceptedTourRequestsByUser(user).Where(r => r.Start.Year == year).ToList();
        }

        public List<Request> GetYearlyDeclinedTourRequestsByUser(User user, int year)
        {
            return _requests.Where(r=>r.Guest.Id==user.Id && r.Start.Year == year && r.State!=RequestsState.Accepted).ToList();
        }
    }
}
