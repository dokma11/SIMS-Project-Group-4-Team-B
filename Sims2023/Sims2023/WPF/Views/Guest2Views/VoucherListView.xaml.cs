using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for VoucherListView.xaml
    /// </summary>
    public partial class VoucherListView : Window, IObserver
    {
        
        public User User { get; set; }

        public VoucherListViewModel VoucherListViewModel { get; set; }
        
        public VoucherListView(User user)
        {
            InitializeComponent();
            

            User = user;
            VoucherListViewModel = new VoucherListViewModel(user, this);
            DataContext = VoucherListViewModel;

        }

        
        
        private void ActivateVoucher_Click(object sender, RoutedEventArgs e)
        {
            VoucherListViewModel.ActivateVoucher_Click();
        }

        private void SkipVoucher_Click(object sender, RoutedEventArgs e)
        {
            VoucherListViewModel.SkipVoucher_Click();
        }

        public void Update()
        {
            VoucherListViewModel.Update();
        }
    }
}
