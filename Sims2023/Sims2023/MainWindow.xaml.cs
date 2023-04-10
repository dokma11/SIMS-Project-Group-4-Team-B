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

        private void Owner_Click(object sender, RoutedEventArgs e)
        {
             var newWindow = new OwnerView();
             newWindow.Show();
        }

        private void Guest1_Click(object sender, RoutedEventArgs e)
        {
            var Guest1MainView = new Guest1MainView();
            Guest1MainView.Show();
        }

        private void Guide_Click(object sender, RoutedEventArgs e)
        {
            GuideView guideView = new GuideView();
            guideView.Show();
        }

        private void Guest2_Click(object sender, RoutedEventArgs e)
        {
            Guest2View guest2View= new Guest2View();
            guest2View.Show();
        }
    }
}
