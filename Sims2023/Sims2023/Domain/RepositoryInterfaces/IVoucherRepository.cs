using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        public int NextId();
        public void Add(Voucher voucher);
        public void AddEdited(Voucher voucher);
        public void Remove(Voucher voucher);
        public List<Voucher> GetAll();
        public Voucher GetById(int id);
        public void Save();
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
    }
}
