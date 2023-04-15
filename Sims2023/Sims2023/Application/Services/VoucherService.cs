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

        public void Create(Voucher voucher)
        {
            _voucher.Add(voucher);
        }

        public void Delete(Voucher voucher)
        {
            _voucher.Remove(voucher);
        }

        public void Edit(Voucher voucher, Voucher oldVoucher)
        {
            _voucher.Remove(oldVoucher);
            _voucher.AddEdited(voucher);
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
