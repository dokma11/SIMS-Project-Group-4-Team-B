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
        public User User { get; set; }
        private App app;
        private const string SRB = "sr-LATN";
        private const string ENG = "en-US";




        public Guest2View(User user)
        {
            InitializeComponent();
            //this.DataContext = new Guest2ViewModel(this.frame.NavigationService);
            MainFrame.Navigate(new Guest2StartView());
            DataContext = this;
            Guest2ViewModel = new Guest2ViewModel(user,this);
            User= user;
            app = (App)System.Windows.Application.Current;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Guest2ViewModel.Window_Loaded();
        }

       

        

        

        private void MyTours_Executed(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Guest2TourListView(User));//list of guests reserved tours
        }

        public void TourRequest_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainFrame.Navigate(new Guest2TourRequestListView(User));//list of guests tour requests 
        }

        public void ComplexTourRequest_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           MainFrame.Navigate(new Guest2ComplexTourRequestListView(User));//list of guests tour requests 
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

        private void Serbian_Click(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(SRB);
        }

        private void English_Click(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(ENG);

        }

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            AppTheme.ChangeTheme(new Uri("WPF/Views/Guest2Views/Themes/Dark.xaml", UriKind.Relative));
        }
    }
}
