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

namespace Sims2023
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OwnerClick(object sender, RoutedEventArgs e)
        {
            // var OpenAccommodationRegistrationView = new OpenAccommodationRegistrationView();
            // OpenAccommodationRegistrationView.show();
        }

        private void Guest1Click(object sender, RoutedEventArgs e)
        {
            // var OpenGuest1View = new OpenGuest1View();
            // OpenGuest1View.show();
        }

        private void GuideClick(object sender, RoutedEventArgs e)
        {
            // var OpenCreateTourView = new OpenCreateTourView();
            // OpenCreateTourView.show();
        }

        private void Guest2Click(object sender, RoutedEventArgs e)
        {
            // var OpenGuest2View = new OpenGuest2View();
            // OpenGuest2View.show();
        }
    }
}
