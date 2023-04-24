using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
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
    }
}
