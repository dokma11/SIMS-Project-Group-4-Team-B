using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System.Collections.ObjectModel;
using System.Windows;

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

        public Guest1MainViewModel(Guest1MainView guest1MainView, User guest1)
        {
            User = guest1;
            _accommodationReservationReschedulingService = new AccommodationReservationReschedulingService();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingService.GetAllReservationReschedulings());
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _accommodationReservationReschedulingService.checkForNotifications(User);
        }
    }
}
