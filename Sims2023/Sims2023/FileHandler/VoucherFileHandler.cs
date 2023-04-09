using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.FileHandler
{
    public class VoucherFileHandler
    {
        private List<Voucher> _vouchers;
        private readonly Serializer<Voucher> _serializer;
        private const string FilePath = "../../../Resources/Data/vouchers.csv";

        public VoucherFileHandler()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(FilePath);
        }

        public Voucher GetById(int id)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.FirstOrDefault(v => v.Id == id);
        }

        public List<Voucher> Load()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers;
        }

        public void Save(List<Voucher> vouchers)
        {
            _serializer.ToCSV(FilePath, vouchers);
        }
    }
}
