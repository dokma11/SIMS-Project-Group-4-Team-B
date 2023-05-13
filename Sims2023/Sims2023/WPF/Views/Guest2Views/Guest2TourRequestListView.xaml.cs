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
using System.Windows.Shapes;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for Guest2TourRequestListView.xaml
    /// </summary>
    public partial class Guest2TourRequestListView : Page
    {
      

        public User User { get; set; }
        public Guest2TourRequestListViewModel Guest2TourRequestListViewModel { get; set; }
        public Guest2TourRequestListView(User user)
        {
            InitializeComponent();
            Guest2TourRequestListViewModel = new Guest2TourRequestListViewModel(user);
            
            DataContext = Guest2TourRequestListViewModel;
            dataGridGuestTourRequests.ItemsSource = Guest2TourRequestListViewModel.TourRequests;
            User = user;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateTourRequestView createTourRequestView = new CreateTourRequestView(User);
            createTourRequestView.Closed += CreateTourRequestView_Closed;
            
            createTourRequestView.Show();
        }
        private void CreateTourRequestView_Closed(object sender, EventArgs e)
        {
            // Refresh the data grid
            Guest2TourRequestListViewModel = new Guest2TourRequestListViewModel(User);

            DataContext = Guest2TourRequestListViewModel;
            dataGridGuestTourRequests.ItemsSource = Guest2TourRequestListViewModel.TourRequests;

        }
    }
}
