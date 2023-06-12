using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Sims2023.WPF.Views.Guest1Views.Guest1Wizard
{
    /// <summary>
    /// Interaction logic for GuestOneFirstView.xaml
    /// </summary>
    public partial class GuestOneFirstView : Page
    {
        Frame MainFrame;
        WizardMainView WizardMainView;
        public GuestOneFirstView(Frame frame, WizardMainView wizardMainView)
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
            MainFrame.Navigate(new GuestSecondFirstView(MainFrame, WizardMainView));
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
