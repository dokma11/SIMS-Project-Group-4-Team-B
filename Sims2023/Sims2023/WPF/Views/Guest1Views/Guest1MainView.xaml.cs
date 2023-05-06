using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using Sims2023.WPF.Views.Guest1Views.Guest1HelpViews;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void Overlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Guest1MainViewModel.HideMainMenu();
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void OpenMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Guest1MainViewModel.ToggleMainMenu();
        }

        public void OpenHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Page currentPage = MainFrame.Content as Page;
            var GuestOneMainHelpView = new GuestOneMainHelpView(currentPage.Title);
            GuestOneMainHelpView.Show();
        }

        public void LogOut_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow LogIn = new();
            LogIn.Show();
            Close();
        }

        public void GuestOneMainView_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Guest1MainViewModel.HideMainMenu();
            MainFrame.Navigate(new GuestOneStartView());
        }

        public void AccommodationListView_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Guest1MainViewModel.HideMainMenu();
            MainFrame.Navigate(new AccommodationListView(User, MainFrame));
        }

        public void AccommodationReservationReschedulingView_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Guest1MainViewModel.HideMainMenu();
            MainFrame.Navigate(new AccommodationReservationReschedulingView(User, MainFrame));
        }

        public void AccommodationReservationCancellation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Guest1MainViewModel.HideMainMenu();
            MainFrame.Navigate(new AccommodationReservationCancellationView(User));
        }

        public void AllGuestOneReservationsView_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Guest1MainViewModel.HideMainMenu();
            MainFrame.Navigate(new AllGuestOneReservationsView(User));
        }

        public void NotImplemented_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Ova funkcionalnost jos uvek nije implementirana");
        }
    }
}
