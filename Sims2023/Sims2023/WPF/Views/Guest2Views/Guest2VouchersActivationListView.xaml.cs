using System;
using System.Collections.Generic;
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
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for Guest2VouchersActivationListView.xaml
    /// </summary>
    public partial class Guest2VouchersActivationListView : Window
    {
        public Guest2VouchersActivationListViewModel Guest2VouchersActivationListViewModel { get; set; }
        public Guest2VouchersActivationListView( User user)
        {
            InitializeComponent();
            Guest2VouchersActivationListViewModel = new Guest2VouchersActivationListViewModel(user, this);
            DataContext = Guest2VouchersActivationListViewModel;
        }

        private void ActivateVoucher_Click(object sender, RoutedEventArgs e)
        {
            Guest2VouchersActivationListViewModel.ActivateVoucher_Click();
        }

        private void SkipVoucher_Click(object sender, RoutedEventArgs e)
        {
            Guest2VouchersActivationListViewModel.SkipVoucher_Click();
        }
    }
}
