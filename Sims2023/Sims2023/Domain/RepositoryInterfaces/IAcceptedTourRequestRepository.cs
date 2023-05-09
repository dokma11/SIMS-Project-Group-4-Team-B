using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Observer;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IAcceptedTourRequestRepository
    {
        public AcceptedTourRequest GetById(int id);
        
        public int NextId();
        
        public void Add(AcceptedTourRequest acceptedTourRequest);
        public List<AcceptedTourRequest> GetAll();

        public List<AcceptedTourRequest> GetByUser(User user);

        public void Subscribe(IObserver observer);
        public void NotifyObservers();
    }
}
