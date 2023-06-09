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
    /// Interaction logic for Try2.xaml
    /// </summary>
    public partial class Try2 : Page
    {
        Frame MainFrame;
        public Try2(Frame frame)
        {
            InitializeComponent();
            DataContext = this;
            MainFrame = frame;

        }
        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void Next(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Ovde se zatvara tako da u sustini moram svuda da prosledim main prozor");
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
