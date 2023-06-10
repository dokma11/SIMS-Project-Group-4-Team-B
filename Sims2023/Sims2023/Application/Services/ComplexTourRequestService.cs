using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class ComplexTourRequestService
    {
        private readonly IComplexTourRequestCSVRepository _complexTourRequest;
        private IUserCSVRepository _user;

        public ComplexTourRequestService()
        {
            _complexTourRequest = Injection.Injector.CreateInstance<IComplexTourRequestCSVRepository>();
            _user = Injection.Injector.CreateInstance<IUserCSVRepository>();

            GetUserReferences();
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequest.GetAll();
        }

        public ComplexTourRequest GetById(int id)
        {
            return _complexTourRequest.GetById(id);
        }
        public void UpdateDate(ComplexTourRequest complexTourRequest, string date)
        {
            _complexTourRequest.UpdateDate(complexTourRequest, date);   
        }
        public List<ComplexTourRequest> GetByUser(User user)
        {
            //GetUserReferences();
            return _complexTourRequest.GetByUser(user);
        }

        public void Create(ComplexTourRequest complexTourRequest)
        {

            _complexTourRequest.Add(complexTourRequest);

        }

        public void Subscribe(IObserver observer)
        {
            _complexTourRequest.Subscribe(observer);
        }

        public List<ComplexTourRequest> GetOnHold()
        {
            return _complexTourRequest.GetOnHold();
        }

        public void GetUserReferences()
        {
            foreach (var request in GetAll())
            {
                request.Guest = _user.GetById(request.Guest.Id) ?? request.Guest;
            }
        }
    }
}
