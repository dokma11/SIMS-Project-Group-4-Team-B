using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IComplexTourRequestCSVRepository
    {
        public ComplexTourRequest GetById(int id);
        public int NextId();
        public void Add(ComplexTourRequest complexTourRequest);
        public List<ComplexTourRequest> GetAll();
        public List<ComplexTourRequest> GetByUser(User user);
        public void Subscribe(IObserver observer);
        public void NotifyObservers();
        public List<ComplexTourRequest> GetOnHold();
    }
}
