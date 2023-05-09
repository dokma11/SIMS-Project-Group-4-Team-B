using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;

namespace Sims2023.Application.Services
{
    public class AcceptedTourRequestService
    {
        private readonly IAcceptedTourRequestRepository _acceptedTourRequests;
        public AcceptedTourRequestService()
        {
            _acceptedTourRequests = new AcceptedTourRequestCSVRepository();
            //_acceptedTourRequests = Injection.Injector.CreateInstance<IAcceptedTourRequestRepository>();
        }

        public AcceptedTourRequest GetById(int id)
        {
            return _acceptedTourRequests.GetById(id);
        }

        public void Create(AcceptedTourRequest acceptedTourRequest)
        {
            _acceptedTourRequests.Add(acceptedTourRequest);
        }

        
        public void Subscribe(IObserver observer)
        {
            _acceptedTourRequests.Subscribe(observer);
        }

        public List<AcceptedTourRequest> GetByUser(User user)
        {
            return _acceptedTourRequests.GetByUser(user);
        }
    }
}
