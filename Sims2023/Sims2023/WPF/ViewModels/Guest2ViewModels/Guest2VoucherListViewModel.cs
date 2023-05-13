using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    
    public class Guest2VoucherListViewModel
    {
        private VoucherService _voucherService;
        public Voucher SelectedVoucher { get; set; }
        public List<Voucher> Vouchers { get; set; }


        public Guest2VoucherListView Guest2VoucherListView { get; set; }

        public Guest2VoucherListViewModel(User user,Guest2VoucherListView guest2VoucherListView)
        {
            _voucherService = new VoucherService();


            SelectedVoucher = null;
            Guest2VoucherListView= guest2VoucherListView;
            Vouchers = _voucherService.GetByUser(user);
        }
    }
}
