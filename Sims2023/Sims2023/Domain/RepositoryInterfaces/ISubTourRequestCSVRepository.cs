using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ISubTourRequestCSVRepository
    {
        public SubTourRequest GetById(int id);
        public int NextId();
        public void Add(SubTourRequest subTourRequest);
        public List<SubTourRequest> GetAll();
        public void Subscribe(IObserver observer);
        public void NotifyObservers();
        public List<SubTourRequest> GetByComplexTourId(int complexTourId);
    }
}
