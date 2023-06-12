using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class ComplexTourRequestCSVRepository : IComplexTourRequestCSVRepository
    {
        private readonly List<ComplexTourRequest> _complexTourRequests;
        private readonly ComplexTourRequestFileHandler _fileHandler;
        private readonly List<IObserver> _observers;

        public ComplexTourRequestCSVRepository()
        {
            _fileHandler = new ComplexTourRequestFileHandler();
            _complexTourRequests = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public ComplexTourRequest GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public int NextId()
        {
            return _complexTourRequests.Count == 0 ? 1 : _complexTourRequests.Max(t => t.Id) + 1;
        }

        public void Add(ComplexTourRequest complexTourRequest)
        {
            complexTourRequest.Id = NextId();
            _complexTourRequests.Add(complexTourRequest);
            _fileHandler.Save(_complexTourRequests);
            NotifyObservers();
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequests;
        }

        public List<ComplexTourRequest> GetByUser(User user)
        {
            return _complexTourRequests.Where(r => r.Guest.Id == user.Id).ToList();
        }

        public void UpdateDate(ComplexTourRequest complexTourRequest,string date)
        {
            complexTourRequest.Date = date;
            _fileHandler.Save(_complexTourRequests);
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

        public List<ComplexTourRequest> GetOnHold()
        {
            return _complexTourRequests.Where(c => c.CurrentState == ComplexRequestsState.OnHold).ToList();
        }
    }
}
