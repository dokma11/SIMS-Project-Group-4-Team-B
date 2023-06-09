using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Observer;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IComplexTourRequestCSVRepository
    {
        public ComplexTourRequest GetById(int id);
        public int NextId();
        public void Add(ComplexTourRequest complexTourRequest);
        public List<ComplexTourRequest> GetAll();
        public List<ComplexTourRequest> GetByUser(User user);
        public void UpdateDate(ComplexTourRequest complexTourRequest,string date);
        public void Subscribe(IObserver observer);
        public void NotifyObservers();

    }
}
