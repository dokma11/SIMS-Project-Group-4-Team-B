using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class RequestCSVRepository: IRequestCSVRepository
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
    }
}
