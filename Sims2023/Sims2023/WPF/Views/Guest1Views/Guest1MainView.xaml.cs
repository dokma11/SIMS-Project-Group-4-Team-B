﻿using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using Sims2023.WPF.Commands;
using System.Windows;
using System.Windows.Input;
using Sims2023.WPF.Views.GuideViews;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainView : Window
    {
        Guest1MainViewModel Guest1MainViewModel;
        public User User { get; set; }

        public Guest1MainView(User guest1)
        {
            InitializeComponent();
            MainFrame.Navigate(new GuestOneStartView());
            Guest1MainViewModel = new Guest1MainViewModel(this, guest1);
            DataContext = Guest1MainViewModel;

            User = guest1;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Guest1MainViewModel.Window_Loaded(sender, e);
        }



        private void buttonGrading_Click(object sender, RoutedEventArgs e)
        {
            var AllGuestOneReservationsView = new AllGuestOneReservationsView(User);
            AllGuestOneReservationsView.Show();
        }

        private void AccommodationCancellation_Click(object sender, RoutedEventArgs e)
        {
            var AccommodationReservationCancellationView = new AccommodationReservationCancellationView(User);
            AccommodationReservationCancellationView.Show();
        }

        private void buttonReservationMove_Click(object sender, RoutedEventArgs e)
        {
            var AccommodationReservationReschedulingView = new AccommodationReservationReschedulingView(User);
            AccommodationReservationReschedulingView.Show();
        }
        private void buttonForum_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void buttonWheneverWherever_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void buttonMyGrades_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Gust1MainWindow_Click(object sender, RoutedEventArgs e)
        {
            Guest1MainViewModel.ToggleMainMenu();
        }






        private void Overlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Guest1MainViewModel.HideMainMenu();
        }

        public void OpenMenu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void OpenMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Guest1MainViewModel.ToggleMainMenu();
        }
        public void OpenHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void OpenHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var AllGuestOneReservationsView = new GuestOneHelpView(User);
            AllGuestOneReservationsView.Show();
        }
        public void LogOut_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void LogOut_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow LogIn = new();
            LogIn.Show();
            Close();
        }

        public void GuestOneMainView_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GuestOneMainView_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainFrame.Navigate(new GuestOneStartView());
        }

        public void AccommodationListView_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void AccommodationListView_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Guest1MainViewModel.HideMainMenu();
             MainFrame.Navigate(new AccommodationListView(User));
        }

    }
}
