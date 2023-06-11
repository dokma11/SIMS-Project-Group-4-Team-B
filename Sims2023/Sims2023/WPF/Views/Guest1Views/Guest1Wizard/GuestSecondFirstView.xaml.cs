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

namespace Sims2023.WPF.Views.Guest1Views.Guest1Wizard
{
    /// <summary>
    /// Interaction logic for GuestSecondFirstView.xaml
    /// </summary>
    public partial class GuestSecondFirstView : Page
    {
        Frame MainFrame;
        WizardMainView WizardMainView;
        public GuestSecondFirstView(Frame frame, WizardMainView wizardMainView)
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
            MainFrame.Navigate(new GuestOneThirdView(MainFrame, WizardMainView));
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
