using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IRequestCSVRepository
    {
        public int NextId();
        public void Add(Request request);
        public List<Request> GetAll();
        public Request GetById(int id);
        public List<Request> GetOnHold();
        public void Subscribe(IObserver observer);
        public void NotifyObservers();
        public void Save();
    }
}
