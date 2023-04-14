using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class VoucherRepository
    {
        private List<IObserver> _observers;
        private List<Voucher> _vouchers;
        private VoucherFileHandler _fileHandler;
        public VoucherRepository()
        {
            _fileHandler = new VoucherFileHandler();
            _vouchers = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_vouchers.Count == 0) return 1;
            return _vouchers.Max(v => v.Id) + 1;
        }

        public void Add(Voucher voucher)
        {
            voucher.Id = NextId();
            _vouchers.Add(voucher);
            _fileHandler.Save(_vouchers);
            NotifyObservers();
        }

        public void AddEdited(Voucher voucher)
        {
            _vouchers.Add(voucher);
            _fileHandler.Save(_vouchers); ;
            NotifyObservers();
        }

        public void Remove(Voucher voucher)
        {
            _vouchers.Remove(voucher);
            _fileHandler.Save(_vouchers);
            NotifyObservers();
        }

        public List<Voucher> GetAll()
        {
            return _vouchers;
        }

        public Voucher GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
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
