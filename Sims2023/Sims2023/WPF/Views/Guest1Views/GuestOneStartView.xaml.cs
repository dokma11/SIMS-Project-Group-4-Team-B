using System.Windows.Controls;
using System.Windows.Input;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using Sims2023.WPF.Views.Guest1Views.Guest1Wizard;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for GuestOneStartView.xaml
    /// </summary>
    public partial class GuestOneStartView : Page
    {
        public GuestOneStartView(User user)
        {
            InitializeComponent();
            this.DataContext = new Guest1StartViewModel(user,this);
        }
        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GenerateReport(object sender, ExecutedRoutedEventArgs e)
        {
            ((Guest1StartViewModel)this.DataContext).OpenReportView();
        }
    }
}
