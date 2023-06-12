using Sims2023.Application.Services;
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
using System.Windows.Shapes;

namespace Sims2023.WPF.Views.Guest1Views.Guest1HelpViews
{
    /// <summary>
    /// Interaction logic for GuestOneMainHelpView.xaml
    /// </summary>
    public partial class GuestOneMainHelpView : Window
    {
        GuestOneMainHelpViewModel GuestOneMainHelpViewModel;
        public GuestOneMainHelpView(String currentPageTitle)
        {
            InitializeComponent();
            this.DataContext = new GuestOneMainHelpViewModel(this, currentPageTitle);
        }
        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void ExitHelp(object sender, ExecutedRoutedEventArgs e)
        {
            Close();   
        }
        public void ShowMore(object sender, ExecutedRoutedEventArgs e)
        {
            ((GuestOneMainHelpViewModel)this.DataContext).ShowMore();
        }
    }
}
