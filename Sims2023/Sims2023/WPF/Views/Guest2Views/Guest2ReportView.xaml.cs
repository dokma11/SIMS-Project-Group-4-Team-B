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
    /// Interaction logic for Guest2ReportView.xaml
    /// </summary>
    public partial class Guest2ReportView : Window
    {
        public Guest2ReportView(User user)
        {
            InitializeComponent();
            this.DataContext = new Guest2ReportViewModel(user, this);
        }
        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void GenerateReport(object sender, RoutedEventArgs e)
        {
            ((Guest2ReportViewModel)this.DataContext).GenerateReport();
        }
        public void GoBack(object sender, RoutedEventArgs e)
        {
            ((Guest2ReportViewModel)this.DataContext).GoBack();
        }
    }
}
