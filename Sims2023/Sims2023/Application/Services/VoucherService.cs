using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class VoucherService
    {
        private readonly IVoucherRepository _voucher;
        public VoucherService()
        {
            _voucher = new VoucherRepository();
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
        }

        public void Delete(Voucher voucher)
        {
            _voucher.Remove(voucher);
        }

        public void Update(Voucher voucher)//deleted edit method and add this
        {
            _voucher.Update(voucher);
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
        }
    }
}
