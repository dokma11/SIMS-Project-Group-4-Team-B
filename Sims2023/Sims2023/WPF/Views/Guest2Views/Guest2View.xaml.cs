using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest2ViewModels;
using Sims2023.WPF.Views.Guest2Views;
using Sims2023.WPF.Views.Guest2Views.Themes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims2023.WPF.Views
{
    /// <summary>
    /// Interaction logic for Guest2View.xaml
    /// </summary>
    public partial class Guest2View : Window
    {
        public Guest2ViewModel Guest2ViewModel { get; set; }
       
        public Guest2View(User user)
        {
            InitializeComponent();
            Guest2ViewModel= new Guest2ViewModel(user, this.MainFrameGuest2.NavigationService,this);
            this.DataContext = Guest2ViewModel;
            

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Guest2ViewModel.Window_Loaded();
        }
        
    }
}
