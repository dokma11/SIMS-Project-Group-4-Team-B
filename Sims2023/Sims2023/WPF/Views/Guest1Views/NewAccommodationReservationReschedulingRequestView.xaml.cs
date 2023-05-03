using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for NewAccommodationReservationReschedulingRequest.xaml
    /// </summary>
    public partial class NewAccommodationReservationReschedulingRequestView : Window
    {
        NewAccommodationReservationReschedulingRequestViewModel NewAccommodationReservationReschedulingRequestViewModel;

        AccommodationReservationReschedulingService _accommodationReservationReschedulingService;
        public AccommodationReservation SelectedAccommodationReservation { get; set; }
        public User User { get; set; }
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        public NewAccommodationReservationReschedulingRequestView(User guest1, ObservableCollection<AccommodationReservationRescheduling> accommodationReservationReschedulings, AccommodationReservationReschedulingService accommodationReservationReschedulingService)
        {
            InitializeComponent();
            NewAccommodationReservationReschedulingRequestViewModel = new NewAccommodationReservationReschedulingRequestViewModel(this, guest1);
            DataContext = NewAccommodationReservationReschedulingRequestViewModel;
            User = guest1;
            AccommodationReservationReschedulings = accommodationReservationReschedulings;
            _accommodationReservationReschedulingService=accommodationReservationReschedulingService;
        }

        private void makeRequest_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)myDataGrid.SelectedItem;
            if (NewAccommodationReservationReschedulingRequestViewModel.CheckIfPossible(SelectedAccommodationReservation))
            {
               // var accommodationReservationDateView = new AccommodationReservationDateView(SelectedAccommodationReservation.Id, SelectedAccommodationReservation.Accommodation, User, AccommodationReservationReschedulings,_accommodationReservationReschedulingService);
                //accommodationReservationDateView.Show();
                NewAccommodationReservationReschedulingRequestViewModel.Update();
                Close();
            }            
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
