using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Sims2023.Repositories
{
    public class RequestCSVRepository : IRequestCSVRepository
    {
        private List<IObserver> _observers;
        private List<Request> _requests;
        private RequestFileHandler _fileHandler;
        private TourReadFromCSVRepository _tours;

        public RequestCSVRepository()
        {
            _fileHandler = new RequestFileHandler();
            _requests = _fileHandler.Load();
            _observers = new List<IObserver>();
            _tours = new TourReadFromCSVRepository();
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

        public List<Request> GetAll()
        {
            return _requests;
        }

        
        public Request GetById(int id)
        {
            return _fileHandler.GetById(id);
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

        public int GetYearlyLanguageStatistics(string language, string year)
        {
            return _requests.Count(r => r.Start.Year.ToString() == year && r.Language.ToString() == language);
        }

        public int GetMonthlyLanguageStatistics(string language, int ordinal, string year)
        {
            return _requests.Count(r => r.Language.ToString() == language && r.Start.Year.ToString() == year && r.Start.Month == ordinal);
        }

        public int GetYearlyLocationStatistics(string location, string year)
        {
            return _requests.Count(r => r.Start.Year.ToString() == year && (r.Location.City + ", " + r.Location.Country) == location);
        }

        public int GetMonthlyLocationStatistics(string location, int ordinal, string year)
        {
            return _requests.Count(r => r.Start.Month == ordinal && r.Start.Year.ToString() == year && (r.Location.City + ", " + r.Location.Country) == location);
        }

        public List<RequestsLanguage> GetLanguages()
        {
            return _requests.Select(r => r.Language).Distinct().ToList();
        }

        public List<string> GetLocations()
        {
            return _requests.Select(r => $"{r.Location.City}, {r.Location.Country}").Distinct().ToList();
        }

        public List<string> GetYears()
        {
            return _requests.Select(r => r.Start.Year.ToString()).Distinct().Prepend("Svih vremena").ToList();
        }

        public RequestsLanguage GetTheMostRequestedLanguage()
        {
            var languageInstanceCounts = new Dictionary<RequestsLanguage, int>();
            foreach (RequestsLanguage language in Enum.GetValues(typeof(RequestsLanguage)))
            {
                languageInstanceCounts[language] = 1;
            }

            var lastYearStartDate = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day);
            var lastYearEndDate = DateTime.Today;
            var requestsInLastYear = _requests.Where(r => r.Start >= lastYearStartDate && r.Start <= lastYearEndDate);

            foreach (var r in requestsInLastYear)
            {
                if (languageInstanceCounts.ContainsKey(r.Language))
                {
                    languageInstanceCounts[r.Language]++;
                }
            }

            var maxCount = languageInstanceCounts.Values.Max();
            return languageInstanceCounts.FirstOrDefault(r => r.Value == maxCount).Key;
        }

        public Location GetTheMostRequestedLocation()
        {
            LocationCSVRepository locationCSV = new();

            var locationInstanceCounts = new Dictionary<int, int>();
            foreach (int locationId in _requests.Select(r => r.Location.Id).Distinct().ToList())
            {
                locationInstanceCounts[locationId] = 1;
            }

            var lastYearStartDate = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day);
            var lastYearEndDate = DateTime.Today;
            var requestsInLastYear = _requests.Where(r => r.Start >= lastYearStartDate && r.Start <= lastYearEndDate);

            foreach (var r in requestsInLastYear)
            {
                if (locationInstanceCounts.ContainsKey(r.Location.Id))
                {
                    locationInstanceCounts[r.Location.Id]++;
                }
            }

            var maxCount = locationInstanceCounts.Values.Max();
            var ret = locationInstanceCounts.FirstOrDefault(r => r.Value == maxCount).Key;
            Location loc = locationCSV.GetById(ret);
            return loc;
        }

        public List<Request> GetByUser(User user)
        {
            return _requests
                .Where(r => r.Guest.Id == user.Id)
                .ToList();
        }

        

        public void CheckExpirationDate(User user)
        {
            foreach(Request request in GetByUser(user))
            {
                TimeSpan tillExpiration=request.Start-DateTime.Now;
                if(tillExpiration.TotalHours < 48)
                {
                    request.State= RequestsState.Invalid;
                    Save();
                }
            }
        }

        public List<Request> GetAcceptedTourRequestsByUser(User user)
        {
            return GetByUser(user).Where(r=> r.State==RequestsState.Accepted).ToList(); 
        }

        public double AcceptedTourRequestPercentageByUser(User user)
        {
            return (_requests.Where(r => r.State == RequestsState.Accepted && r.Guest.Id==user.Id).Count() / _requests.Where(r=> r.Guest.Id==user.Id).Count())*100;
        }

        public List<Request> GetYearlyAcceptedTourRequestsByUser(User user, int year)
        {
            return GetAcceptedTourRequestsByUser(user).Where(r => r.Start.Year == year).ToList();
        }
    }
}
