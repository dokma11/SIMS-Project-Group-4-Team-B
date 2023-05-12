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
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for TourListView.xaml
    /// </summary>
    public partial class TourListView : Page
    {
        public User User { get; set; }
        public Guest2ViewModel Guest2ViewModel { get; set; }    
        public TourListView(User user)
        {
            InitializeComponent();
            User = user;
            //Guest2ViewModel = new Guest2ViewModel(user, this);
            DataContext=Guest2ViewModel;
        }

        private void SearchTours_Click(object sender, RoutedEventArgs e)
        {
           // Guest2ViewModel.SearchTours_Click();
        }

        private void ReserveTour_Click(object sender, RoutedEventArgs e)
        {
           // Guest2ViewModel.ReserveTour_Click();
        }
    }
}
