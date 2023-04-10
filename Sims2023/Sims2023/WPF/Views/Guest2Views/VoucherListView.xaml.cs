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

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for VoucherListView.xaml
    /// </summary>
    public partial class VoucherListView : Window, IObserver
    {
        private VoucherService _voucherService;
        public Voucher SelectedVoucher { get; set; }
        public User User { get; set; }
        public ObservableCollection<Voucher> Vouchers { get; set; }
        //public List<Voucher> VouchersByUser = new List<Voucher>();
        public VoucherListView(User user)
        {
            InitializeComponent();
            DataContext = this;

            _voucherService=new VoucherService();
            _voucherService.Subscribe(this);

            SelectedVoucher= new Voucher();
            User = user;
            Vouchers = new ObservableCollection<Voucher>(_voucherService.GetAll());

            dataGridVouchers.ItemsSource = FindVouchersByUser(user,Vouchers);

        }

        public List<Voucher> FindVouchersByUser(User user,ObservableCollection<Voucher> vouchers)
        {
            List<Voucher> VouchersById = new List<Voucher>();
            foreach(Voucher voucher in Vouchers )
            {
                if(voucher.User.Id==user.Id && voucher.IsUsed==false)
                    VouchersById.Add(voucher);  
            }
            return VouchersById;
        

        }

        

        private void ActivateVoucher_Click(object sender, RoutedEventArgs e)
        {
            foreach (Voucher voucher in Vouchers)
            {
                if (SelectedVoucher == voucher)
                {
                    SelectedVoucher.IsUsed = true;
                    _voucherService.Edit(SelectedVoucher, voucher);
                    break;
                }
            }
            MessageBox.Show("Iskoristili ste vaucer");


        }

        private void SkipVoucher_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void Update()
        {
            dataGridVouchers.ItemsSource = FindVouchersByUser(User, Vouchers);
        }
    }
}
