using Sims2023.View;
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
             var newWindow = new OwnerView();
             newWindow.Show();
        }

        private void Guest1Click(object sender, RoutedEventArgs e)
        {
            var Guest1MainWindow = new Guest1MainWindow();
            Guest1MainWindow.Show();
        }

        private void GuideClick(object sender, RoutedEventArgs e)
        {
            GuideView guideView = new GuideView();
            guideView.Show();
        }

        private void Guest2Click(object sender, RoutedEventArgs e)
        {
            // var OpenGuest2View = new OpenGuest2View();
            // OpenGuest2View.show();
        }
    }
}
