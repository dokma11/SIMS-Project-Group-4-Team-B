using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for WheneverWhereverMainView.xaml
    /// </summary>
    public partial class WheneverWhereverMainView : Page
    {

        public WheneverWhereverMainView(User guest1,Frame mainFrame)
        {
            InitializeComponent();
            this.DataContext = new WheneverWhereverMainViewModel(guest1, mainFrame, this);
        }
        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void MakeReservation(object sender, ExecutedRoutedEventArgs e)
        {
            ((WheneverWhereverMainViewModel)this.DataContext).MakeReservation();

        }
    }
}
