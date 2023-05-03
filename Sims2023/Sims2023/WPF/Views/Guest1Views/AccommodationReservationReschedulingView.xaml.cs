using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationReservationReschedulingView.xaml
    /// </summary>
    public partial class AccommodationReservationReschedulingView : Page
    {
        private AccommodationReservationReschedulingService _accommodationReservationReschedulingService;

        public AccommodationReservationReschedulingViewModel AccommodationReservationReschedulingViewModel;
        public User User { get; set; }
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }
        public ObservableCollection<AccommodationReservationRescheduling> FilteredData { get; set; }

        public AccommodationReservationReschedulingView(User guest1)
        {
            InitializeComponent();
            FilteredData = new ObservableCollection<AccommodationReservationRescheduling>();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>();
            _accommodationReservationReschedulingService = new AccommodationReservationReschedulingService();

            AccommodationReservationReschedulingViewModel = new AccommodationReservationReschedulingViewModel(this, guest1,FilteredData, AccommodationReservationReschedulings, _accommodationReservationReschedulingService);
            DataContext = AccommodationReservationReschedulingViewModel;
            User = guest1;
        }

        private void newRequest_Click(object sender, RoutedEventArgs e)
        {            
            var newAccommodationReservationReschedulingRequest = new NewAccommodationReservationReschedulingRequestView(User, FilteredData, _accommodationReservationReschedulingService);
            newAccommodationReservationReschedulingRequest.Show();
            AccommodationReservationReschedulingViewModel.Update();
        }

        private void report_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationReschedulingViewModel.report_Click(sender, e);
        }

        private void comment_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationReschedulingViewModel.comment_Click(sender, e);
        }
    }
}
