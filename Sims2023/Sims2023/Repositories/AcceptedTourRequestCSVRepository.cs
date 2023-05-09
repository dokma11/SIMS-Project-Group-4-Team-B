using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;

namespace Sims2023.Repositories
{
    public class AcceptedTourRequestCSVRepository:IAcceptedTourRequestRepository
    {
        
        private readonly List<AcceptedTourRequest> _acceptedTourRequests;
        private readonly AcceptedTourRequestFileHandler _fileHandler;
        private readonly List<IObserver> _observers;

        public AcceptedTourRequestCSVRepository()
        {
            _fileHandler = new AcceptedTourRequestFileHandler();
            _acceptedTourRequests = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public AcceptedTourRequest GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        

        public int NextId()
        {
            return _acceptedTourRequests.Count == 0 ? 1 : _acceptedTourRequests.Max(t => t.Id) + 1;
        }
        public void Add(AcceptedTourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _acceptedTourRequests.Add(tourRequest);
            _fileHandler.Save(_acceptedTourRequests);
            NotifyObservers();
        }

        public List<AcceptedTourRequest> GetAll()
        {
            return _acceptedTourRequests;
        }

        public List<AcceptedTourRequest> GetByUser(User user)
        {
            return _acceptedTourRequests
                .Where(r => r.Request.Guest.Id == user.Id)
                .ToList();
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
    }
}
