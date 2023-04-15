using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        public int NextId();
        public void Add(Voucher voucher);
        public void Update(Voucher voucher);//deleted addedited and added this
        public void UpdateIsUsed(Voucher voucher);//when guest used voucher
        public void Remove(Voucher voucher);
        public List<Voucher> GetAll();
        public List<Voucher> GetByUser(User user);//new method to show only user's vouchers
        public Voucher GetById(int id);
        public void Save();

    }
}
