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
using System.Windows;
using System.Windows.Input;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Diagnostics;

namespace Sims2023.WPF.Views.Guest1Views.Guest1Wizard
{
    /// <summary>
    /// Interaction logic for GuestOneForthView.xaml
    /// </summary>
    public partial class GuestOneFourthView : Page
    {
        Frame MainFrame;
        WizardMainView WizardMainView;
        public GuestOneFourthView(Frame frame, WizardMainView wizardMainView)
        {
            InitializeComponent();
            DataContext = this;
            MainFrame = frame;
            WizardMainView = wizardMainView;

        }
        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void Next(object sender, ExecutedRoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Da li ste sigurni da želite da izađete iz wizard-a? Ukoliko izađete nećete moći ponovo da ga pokrenete.", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                WizardMainView.Close();
            }
            else
            {
                return;
            }
        }
        public void Back(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }
    }
}
