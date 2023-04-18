using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest2ViewModels;
using System.Windows;

namespace Sims2023.WPF.Views
{
    /// <summary>
    /// Interaction logic for Guest2View.xaml
    /// </summary>
    public partial class Guest2View : Window, IObserver
    {
        public Guest2ViewModel Guest2ViewModel { get; set; }
       

        public Guest2View(User user)
        {
            InitializeComponent();
            DataContext = this;
            Guest2ViewModel = new Guest2ViewModel(user,this);

            DataContext = Guest2ViewModel;
            dataGridTours.ItemsSource = Guest2ViewModel.Tours;
            dataGridTours.SelectedItem = Guest2ViewModel.SelectedTour;



        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
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

        private void MyReservations_Click(object sender, RoutedEventArgs e)
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
