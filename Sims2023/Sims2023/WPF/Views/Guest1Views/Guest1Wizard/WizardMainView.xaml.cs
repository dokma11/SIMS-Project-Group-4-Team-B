using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using Sims2023.WPF.Views.Guest1Views.Guest1HelpViews;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims2023.WPF.Views.Guest1Views.Guest1Wizard
{
    /// <summary>
    /// Interaction logic for WizardMainWindow.xaml
    /// </summary>
    public partial class WizardMainView : Window
    {
        public WizardMainView()
        {
            InitializeComponent();
            this.DataContext = this;
            MainFrame.Navigate(new GuestOneMainWizardView(MainFrame,this));
        }
    }
}
