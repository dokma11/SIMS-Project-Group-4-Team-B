using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Sims2023.WPF;
using Sims2023.WPF.Views.Guest2Views;
using System.Linq;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views
{
    /// <summary>
    /// Interaction logic for Guest2View.xaml
    /// </summary>
    public partial class Guest2View : Window,IObserver
    {
        public Guest2ViewModel Guest2ViewModel { get; set; }
        public User User { get; set; }

        public Guest2View(User user)
        {
            InitializeComponent();
            DataContext = this;

            User = user;
            Guest2ViewModel = new Guest2ViewModel(user,this);

            DataContext = Guest2ViewModel;
            dataGridTours.ItemsSource=Guest2ViewModel.Tours;
            dataGridTours.SelectedItem = Guest2ViewModel.SelectedTour;
            

           
        }
        private void Window_Loaded(object sender,RoutedEventArgs e)
        {
            Guest2ViewModel.Window_Loaded();
        }
        
        private void SearchTours_Click(object sender, RoutedEventArgs e)
        {
            Guest2ViewModel.SearchTours_Click();
        }

        private void ReserveTour_Click(object sender, RoutedEventArgs e)
        {
            Guest2ViewModel.ReserveTour_Click();
        }

        private void MyReservations_Click(object sender,RoutedEventArgs e)
        {
            Guest2ViewModel.MyReservations_Click();
        }

        public void Update()
        {
            Guest2ViewModel.Update();
        }

        private void SeeDetails_Click(object sender, RoutedEventArgs e)
        {
            Guest2ViewModel.SeeDetails_Click();
        }
    }
}
