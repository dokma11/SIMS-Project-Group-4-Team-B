using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Windows;
using System.Windows.Input;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : Window
    {
        public ReportView(User user)
        {
            InitializeComponent();
            this.DataContext = new ReportViewModel(user, this);
        }
        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void GenerateReport(object sender, RoutedEventArgs e)
        {
            ((ReportViewModel)this.DataContext).GenerateReport();
        }
        public void GoBack(object sender, RoutedEventArgs e)
        {
            ((ReportViewModel)this.DataContext).GoBack();
        }
        public void OpenHelp(object sender, ExecutedRoutedEventArgs e)
        {
            ((ReportViewModel)this.DataContext).OpenHelp();
        }
    }
}
