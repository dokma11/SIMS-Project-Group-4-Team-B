using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class SubTourRequestCSVRepository : ISubTourRequestCSVRepository
    {
        private readonly List<SubTourRequest> _subTourRequests;
        private readonly SubTourRequestFileHandler _fileHandler;
        private readonly List<IObserver> _observers;

        public SubTourRequestCSVRepository()
        {
            _fileHandler = new SubTourRequestFileHandler();
            _subTourRequests = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public SubTourRequest GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public int NextId()
        {
            return _subTourRequests.Count == 0 ? 1 : _subTourRequests.Max(t => t.Id) + 1;
        }

        public void Add(SubTourRequest subTourRequest)
        {
            subTourRequest.Id = NextId();
            _subTourRequests.Add(subTourRequest);
            _fileHandler.Save(_subTourRequests);
            NotifyObservers();
        }

        public List<SubTourRequest> GetAll()
        {
            return _subTourRequests;
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

        public List<SubTourRequest> GetByComplexTourId(int complexTourId)
        {
            return _subTourRequests.Where(s => s.ComplexTourRequest.Id == complexTourId).ToList();
        }
    }
}
