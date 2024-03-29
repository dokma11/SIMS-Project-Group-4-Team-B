﻿using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class VoucherCSVRepository: IVoucherCSVRepository
    {
        private List<IObserver> _observers;
        private List<Voucher> _vouchers;
        private VoucherFileHandler _fileHandler;
        public VoucherCSVRepository()
        {
            _fileHandler = new VoucherFileHandler();
            _vouchers = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            return _vouchers.Count == 0 ? 1 : _vouchers.Max(t => t.Id) + 1;
        }

        public void Add(Voucher voucher)
        {
            voucher.Id = NextId();
            _vouchers.Add(voucher);
            _fileHandler.Save(_vouchers);
            NotifyObservers();
        }

        public void Update(Voucher voucher)//deleted addedited and added this
        {
            int index = _vouchers.FindIndex(p => p.Id == voucher.Id);
            if (index != -1)
            {
                _vouchers[index] = voucher;
            }

            _fileHandler.Save(_vouchers);
            NotifyObservers();
        }

        public void UpdateIsUsed(Voucher voucher)//when guest used voucher
        {
             voucher.IsUsed = true;
             Update(voucher);
        }

        public List<Voucher> GetAll()
        {
            return _vouchers;
        }

        public List<Voucher> GetByUser(User user)//new method to show only user's vouchers
        {
            return _vouchers.Where(voucher => voucher.User.Id == user.Id && voucher.IsUsed == false)
                    .ToList();
        }

        public Voucher GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Save()
        {
            _fileHandler.Save(_vouchers);
        }
    }
}
