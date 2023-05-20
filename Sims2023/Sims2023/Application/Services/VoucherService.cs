using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class VoucherService
    {
        private readonly IVoucherCSVRepository _voucher;
        private readonly ITourReadFromCSVRepository _tour;
        private readonly IUserCSVRepository _user;
        public VoucherService()
        {
            _voucher = Injection.Injector.CreateInstance<IVoucherCSVRepository>();
            _tour = Injection.Injector.CreateInstance<ITourReadFromCSVRepository>();
            _user = Injection.Injector.CreateInstance<IUserCSVRepository>();

            GetUserReferences();
            GetTourReferences();
        }

        public List<Voucher> GetAll()
        {
            return _voucher.GetAll();
        }

        public Voucher GetById(int id)
        {
            return _voucher.GetById(id);
        }

        public List<Voucher> GetByUser(User user)//new method for guest2
        {
            return _voucher.GetByUser(user);
        }

        public void Create(Voucher voucher)
        {
            _voucher.Add(voucher);
            Save();
        }

        public void Update(Voucher voucher)//deleted edit method and add this
        {
            _voucher.Update(voucher);
            Save();
        }

        public void UpdateIsUsed(Voucher voucher)//new method
        {
            _voucher.UpdateIsUsed(voucher);
        }

        public void Subscribe(IObserver observer)
        {
            _voucher.Subscribe(observer);
        }

        public void Save()
        {
            _voucher.Save();
            GetUserReferences();
            GetTourReferences();
        }

        public void GetUserReferences()
        {
            foreach (var voucher in GetAll())
            {
                voucher.User = _user.GetById(voucher.User.Id) ?? voucher.User;
            }
        }

        public void GetTourReferences()
        {
            foreach (var voucher in GetAll())
            {
                voucher.Tour = _tour.GetById(voucher.Tour.Id) ?? voucher.Tour;
            }
        }
    }
}
