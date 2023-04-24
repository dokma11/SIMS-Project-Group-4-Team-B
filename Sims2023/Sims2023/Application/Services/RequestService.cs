using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class RequestService
    {
        private IRequestCSVRepository _request;

        public RequestService()
        {
            _request = new RequestCSVRepository();
            //_request = Injection.Injector.CreateInstance<IRequestCSVRepository>();
        }

        public void Create(Request request)
        {
            _request.Add(request);
        }

        public List<Request> GetAll()
        {
            return _request.GetAll();
        }

        public Request GetById(int id)
        {
            return _request.GetById(id);
        }

        public List<Request> GetOnHold()
        {
            return _request.GetOnHold();
        }

        public void Save()
        {
            _request.Save();
        }

        public void Subscribe(IObserver observer)
        {
            _request.Subscribe(observer);
        }
    }
}
