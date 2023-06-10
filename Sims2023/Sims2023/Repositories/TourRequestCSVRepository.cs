using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class TourRequestCSVRepository : ITourRequestCSVRepository
    {
        private List<IObserver> _observers;
        private List<TourRequest> _requests;
        private TourRequestFileHandler _fileHandler;

        public TourRequestCSVRepository()
        {
            _fileHandler = new TourRequestFileHandler();
            _requests = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public List<TourRequest> GetAll()
        {
            return _requests;
        }

        public int NextId()
        {
            return _requests.Count == 0 ? 1 : _requests.Max(r => r.Id) + 1;
        }

        public void Add(TourRequest request)
        {
            request.Id = NextId();
            _requests.Add(request);
            _fileHandler.Save(_requests);
            NotifyObservers();
        }

        public List<TourRequest> GetOnHold()
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

        public List<TourRequest> GetFiltered(string locationSearchTerm, string guestNumberSearchTerm, string languageSearchTerm, string dateStartSearchTerm, string dateEndSearchTerm)
        {
            return GetOnHold().Where(request =>
                   (string.IsNullOrEmpty(locationSearchTerm) || request.Location.City.ToLower().Contains(locationSearchTerm.ToLower())) &&
                   (string.IsNullOrEmpty(guestNumberSearchTerm) || request.GuestNumber.ToString().ToLower().Contains(guestNumberSearchTerm.ToLower())) &&
                   (string.IsNullOrEmpty(languageSearchTerm) || request.Language.ToString().ToLower().Contains(languageSearchTerm.ToLower())) &&
                   (string.IsNullOrEmpty(dateStartSearchTerm) || request.Start >= DateTime.Parse(dateStartSearchTerm)) &&
                   (string.IsNullOrEmpty(dateEndSearchTerm) || request.End <= DateTime.Parse(dateEndSearchTerm))).ToList();
        }

        public void UpdateState(TourRequest selectedRequest, RequestsState requestsState)
        {
            selectedRequest.State = requestsState;
            Save();
        }

        public List<TourRequest> GetByUser(User user)
        {
            return _requests.Where(r => r.Guest.Id == user.Id).ToList();
        }

        public void CheckExpirationDate(User user)
        {
            foreach (TourRequest request in GetByUser(user).Intersect(GetOnHold()))
            {
                TimeSpan tillExpiration = request.Start - DateTime.Now;
                if (tillExpiration.TotalHours < 48)
                {
                    request.State = RequestsState.Invalid;
                    Save();
                }
            }
        }

       


        public List<TourRequest> GetByLocation(Location location)
        {
            return _requests.Where(req => req.Location.City.ToString() == location.City.ToString() &&
                                   req.Location.Country.ToString() == location.Country.ToString() &&
                                   req.State == RequestsState.OnHold).ToList();
        }

        public List<TourRequest> GetByLanguage(string language)
        {
            return _requests.Where(req => req.Language.ToString() == language && req.State == RequestsState.OnHold).ToList();
        }

        public TourRequest GetById(int id)
        {
            return _requests.FirstOrDefault(t => t.Id == id);
        }
    }
}
