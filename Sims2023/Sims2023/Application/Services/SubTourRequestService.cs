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
        private ILocationCSVRepository _location;
        private IUserCSVRepository _user;
        private ITourRequestCSVRepository _tourRequest;
        private IComplexTourRequestCSVRepository _complexTourRequest;


        public SubTourRequestService()
        {
            _subTourRequest=Injection.Injector.CreateInstance<ISubTourRequestCSVRepository>();
            _location = Injection.Injector.CreateInstance<ILocationCSVRepository>();
            _user = Injection.Injector.CreateInstance<IUserCSVRepository>();
            _tourRequest=Injection.Injector.CreateInstance<ITourRequestCSVRepository>();
            _complexTourRequest=Injection.Injector.CreateInstance<IComplexTourRequestCSVRepository>();
            GetTourRequestReferences();
            GetComplexTourRequestReference();



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

        public void GetTourRequestReferences()
        {
            foreach (var subTourRequest in GetAll())
            {
                subTourRequest.TourRequest = _tourRequest.GetById(subTourRequest.TourRequest.Id) ?? subTourRequest.TourRequest;
                subTourRequest.TourRequest.Location = _location.GetById(subTourRequest.TourRequest.Location.Id);
            }
        }

        public void GetComplexTourRequestReference()
        {
            foreach (var subTourRequest in GetAll())
            {
                subTourRequest.ComplexTourRequest = _complexTourRequest.GetById(subTourRequest.ComplexTourRequest.Id) ?? subTourRequest.ComplexTourRequest;
                
            }
        }





    }
}
