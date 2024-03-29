﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
   
    public class Guest2VouchersActivationListViewModel
    {
        private VoucherService _voucherService;
        public Voucher SelectedVoucher { get; set; }
        public List<Voucher> Vouchers { get; set; }
        public RelayCommand ActivateCommand { get; set; }
        public RelayCommand SkipCommand { get; set; }
        

        public Guest2VouchersActivationListView Guest2VouchersActivationListView { get; set; }
        public Guest2VouchersActivationListViewModel(User user,Guest2VouchersActivationListView voucherListView)
        {
            _voucherService = new VoucherService();
            

            SelectedVoucher = null;
            Guest2VouchersActivationListView = voucherListView;
            Vouchers = _voucherService.GetByUser(user);
            this.ActivateCommand = new RelayCommand(Execute_ActivateCommand, CanExecute_Command);
            this.SkipCommand = new RelayCommand(Execute_SkipCommand, CanExecute_Command);
        }

        /*public void ActivateVoucher_Click()
        {
            if(IsNull(SelectedVoucher))
                return;
            _voucherService.UpdateIsUsed(SelectedVoucher);
            Guest2VouchersActivationListView.Close();
        }*/

        private void Execute_ActivateCommand(object obj)
        {
            if (IsNull(SelectedVoucher))
                return;
            _voucherService.UpdateIsUsed(SelectedVoucher);
            Guest2VouchersActivationListView.Close();
        }

        public bool IsNull(Voucher voucher)
        {
            if(voucher == null)
            {
                MessageBox.Show("Izaberite vaucer");
                return true;
            }
            else
            {
                MessageBox.Show("Iskoristili ste vaucer");
                return false;
            }
            
        }

        /*public void SkipVoucher_Click()
        {
            Guest2VouchersActivationListView.Close();
        }*/

        private void Execute_SkipCommand(object obj)
        {
            Guest2VouchersActivationListView.Close();
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }


    }
}
