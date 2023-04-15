using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
   
    public class VoucherListViewModel:IObserver
    {
        private VoucherService _voucherService;
        public Voucher SelectedVoucher { get; set; }
        public List<Voucher> Vouchers { get; set; }
        public User User { get; set; }

        public VoucherListView VoucherListView { get; set; }
        public VoucherListViewModel(User user,VoucherListView voucherListView)
        {
            _voucherService = new VoucherService();
            

            SelectedVoucher = new Voucher();
            User = user;
            VoucherListView = voucherListView;
           
            Vouchers = _voucherService.GetByUser(user);
        }

        public void ActivateVoucher_Click()
        {
            foreach(var voucher in Vouchers)
            {
                if (voucher == SelectedVoucher)
                {
                    _voucherService.UpdateIsUsed(voucher);
                }
            }
            MessageBox.Show("Iskoristili ste vaucer");
            VoucherListView.Close();
        }

        public void SkipVoucher_Click()
        {
            VoucherListView.Close();
        }

        public void Update()
        {
            VoucherListView.dataGridVouchers.ItemsSource = _voucherService.GetByUser(User);
        }
    }
}
