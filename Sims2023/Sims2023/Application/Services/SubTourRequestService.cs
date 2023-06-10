using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class SubTourRequestService
    {
        private readonly ISubTourRequestCSVRepository _subTourRequest;
        private ILocationCSVRepository _location;
        private IUserCSVRepository _user;
        private ITourRequestCSVRepository _request;
        private IComplexTourRequestCSVRepository _complexTourRequest;


        public SubTourRequestService()
        {
            _subTourRequest = Injection.Injector.CreateInstance<ISubTourRequestCSVRepository>();
            _location = Injection.Injector.CreateInstance<ILocationCSVRepository>();
            _user = Injection.Injector.CreateInstance<IUserCSVRepository>();
            _request = Injection.Injector.CreateInstance<ITourRequestCSVRepository>();
            _complexTourRequest = Injection.Injector.CreateInstance<IComplexTourRequestCSVRepository>();

            GetTourRequestReferences();
            GetComplexTourRequestReferences();
        }

        private void GetTourRequestReferences()
        {
            foreach(var item in GetAll())
            {
                var request = _request.GetById(item.TourRequest.Id);
                var user = _user.GetById(request.Guest.Id);
                var location = _location.GetById(request.Location.Id);
                if(request != null)
                {
                    item.TourRequest = request;
                    item.TourRequest.Location = location;
                    item.TourRequest.Guest = user;
                }
            }
        }

        private void GetComplexTourRequestReferences()
        {
            foreach (var item in GetAll())
            {
                var complexTourRequest = _complexTourRequest.GetById(item.ComplexTourRequest.Id);
                var user = _user.GetById(complexTourRequest.Guest.Id);
                if (complexTourRequest != null)
                {
                    item.ComplexTourRequest = complexTourRequest;
                    item.ComplexTourRequest.Guest = user;
                }
            }
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

        /*public void CheckExpirationDate(ComplexTourRequest complexTourRequest)
        {
            GetComplexTourRequestReference();
            GetTourRequestReferences();
            _subTourRequest.CheckExpirationDate(complexTourRequest);
        }*/

        public List<SubTourRequest> GetByComplexTourRequest(ComplexTourRequest complexTourRequest)
        {
            GetTourRequestReferences();
            
            return _subTourRequest.GetByComplexTourRequest(complexTourRequest);
        }
        public string GetEarliestSubTourDateByComplexTourRequest(ComplexTourRequest complexTourRequest)
        {
            GetTourRequestReferences();
            GetComplexTourRequestReference();
            return _subTourRequest.GetEarliestSubTourDateByComplexTourRequest(complexTourRequest);
        }

        






        public List<SubTourRequest> GetByComplexTourId(int complexTourId)
        {
            return _subTourRequest.GetByComplexTourId(complexTourId);
        }
    }
}
