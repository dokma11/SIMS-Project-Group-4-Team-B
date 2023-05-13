using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest2ViewModels;
using Sims2023.WPF.Views.Guest2Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims2023.WPF.Views
{
    /// <summary>
    /// Interaction logic for Guest2View.xaml
    /// </summary>
    public partial class Guest2View : Window, IObserver
    {
        public Guest2ViewModel Guest2ViewModel { get; set; }
        public User User { get; set; }
       

        public Guest2View(User user)
        {
            InitializeComponent();
            MainFrame.Navigate(new Guest2StartView());
            DataContext = this;
            Guest2ViewModel = new Guest2ViewModel(user,this);
            User= user;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Guest2ViewModel.Window_Loaded();
        }

       

        private void ReserveTour_Click(object sender, RoutedEventArgs e)
        {
            //Guest2ViewModel.ReserveTour_Click();
        }

        

        public void Update()
        {
            //Guest2ViewModel.Update();
        }

        private void SeeDetails_Click(object sender, RoutedEventArgs e)
        {
            //Guest2ViewModel.SeeDetails_Click();
        }

        

        private void MyTours_Executed(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Guest2TourListView(User));//list of guests reserved tours
        }

        public void TourRequest_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainFrame.Navigate(new Guest2TourRequestListView(User));//list of guests tour requests 
        }

        public void Home_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainFrame.Navigate(new Guest2StartView());//start view
        }
        public void TourList_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainFrame.Navigate(new TourListView(User));//all tours view
        }
        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void LogOut_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow LogIn = new();
            LogIn.Show();
            Close();
        }

        public void VoucherList_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainFrame.Navigate(new Guest2VoucherListView(User));
        }

    }
}
