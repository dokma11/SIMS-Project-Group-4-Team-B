using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Observer;

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
    }
}
