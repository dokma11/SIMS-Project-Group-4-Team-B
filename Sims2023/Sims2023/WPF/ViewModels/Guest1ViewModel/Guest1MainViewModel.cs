using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainViewModel : Window
    {
        public User User { get; set; }

        private AccommodationReservationReschedulingService _accommodationReservationReschedulingService;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }
        Guest1MainView Guest1MainView;
        public Guest1MainViewModel(Guest1MainView guest1MainView, User guest1)
        {
            Guest1MainView = guest1MainView;
            User = guest1;
            _accommodationReservationReschedulingService = new AccommodationReservationReschedulingService();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingService.GetAllReservationReschedulings());
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkForNotifications(User);
        }
        public void checkForNotifications(User guest1)
        {
            foreach (AccommodationReservationRescheduling accommodationReservationRescheduling in AccommodationReservationReschedulings)
            {
                if (Notify(accommodationReservationRescheduling, guest1))
                {
                    MessageBox.Show($" Vlasnik smestaja {accommodationReservationRescheduling.AccommodationReservation.Accommodation.Name} je promienio status vaseg zahteva za pomeranje rezervacije. Vas zahtev je {accommodationReservationRescheduling.Status}!");
                    accommodationReservationRescheduling.Notified = true;
                    _accommodationReservationReschedulingService.Update(accommodationReservationRescheduling);
                }
            }
        }

        public bool Notify(AccommodationReservationRescheduling accommodationReservationRescheduling, User guest1)
        {
            if (accommodationReservationRescheduling.Notified == false && accommodationReservationRescheduling.AccommodationReservation.Guest.Id == guest1.Id && accommodationReservationRescheduling.Status.ToString() != "Pending")
            {
                return true;
            }
            return false;
        }
        public void HideMainMenu()
        {
            Guest1MainView.MainMenu.Visibility = Visibility.Collapsed;
            Guest1MainView.Overlay.Visibility = Visibility.Collapsed;
        }
        public void ToggleMainMenu()
        {
            if (Guest1MainView.MainMenu.Visibility == Visibility.Visible)
            {
                HideMainMenu();
            }
            else
            {
                ShowMainMenu();
            }
        }
        public void ShowMainMenu()
        {
            Guest1MainView.MainMenu.Visibility = Visibility.Visible;
            Guest1MainView.Overlay.Visibility = Visibility.Visible;
        }

    }
}
