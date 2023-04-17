using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{   
    public partial class Guest2TourListView : Window
    {
        public Guest2TourListViewModel Guest2TourListViewModel { get; set; }
        
        public Guest2TourListView(User user)
        {
            InitializeComponent();
            DataContext = this;

            Guest2TourListViewModel = new Guest2TourListViewModel(user, this);

            DataContext = Guest2TourListViewModel;
            dataGridGuestTours.ItemsSource = Guest2TourListViewModel.Tours;
            dataGridGuestTours.SelectedItem = Guest2TourListViewModel.SelectedTour;


        }
        
        private void RateTour_Click(object sender, RoutedEventArgs e)
        {
            Guest2TourListViewModel.RateTour_Click();
        }

        private void SeeActiveTour_Click(object sender, RoutedEventArgs e)
        {
            Guest2TourListViewModel.SeeActiveTour_Click();
        }

    }

    
}
