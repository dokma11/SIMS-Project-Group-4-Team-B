﻿using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IVoucherCSVRepository
    {
        public int NextId();
        public void Add(Voucher voucher);
        public void Update(Voucher voucher);//deleted addedited and added this
        public void UpdateIsUsed(Voucher voucher);//when guest used voucher
        public List<Voucher> GetAll();
        public Voucher GetById(int id);
        public List<Voucher> GetByUser(User user);//new method to show only user's vouchers
        public void Save();
        public void Subscribe(IObserver observer);
        public void NotifyObservers();
    }
}
