using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;

namespace Sims2023.Application.Services
{
    public class SubTourRequestService
    {
        private readonly ISubTourRequestCSVRepository _subTourRequest;

        public SubTourRequestService()
        {
            _subTourRequest=Injection.Injector.CreateInstance<ISubTourRequestCSVRepository>();
        }

        public List<SubTourRequest> GetAll()
        {
            return _subTourRequest.GetAll();
        }

        public SubTourRequest GetById(int id)
        {
            return _subTourRequest.GetById(id);
        }

        public void Create(SubTourRequest subTourRequest)
        {

            _subTourRequest.Add(subTourRequest);
          
        }

        public void Subscribe(IObserver observer)
        {
            _subTourRequest.Subscribe(observer);
        }
    }
}
