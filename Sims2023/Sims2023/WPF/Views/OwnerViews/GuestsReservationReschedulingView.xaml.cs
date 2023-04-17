using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for GuestsReservationReschedulingView.xaml
    /// </summary>
    public partial class GuestsReservationReschedulingView : Window
    {
        public AccommodationReservationRescheduling SelectedGuest { get; set; }
        public ObservableCollection<AccommodationReservationRescheduling> people { get; set; }

        public GuestsReservationReschedulingViewModel GuestsReservationReschedulingViewModel;
        public GuestsReservationReschedulingView(User owner)
        {
            InitializeComponent();
            DataContext = this;
            GuestsReservationReschedulingViewModel = new GuestsReservationReschedulingViewModel();
            people = new ObservableCollection<AccommodationReservationRescheduling>(GuestsReservationReschedulingViewModel.GetGuestsReservationMove(owner));
        }

        public void Details_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGuest != null)
            {
                var showDetails = new ReschedulingDetailsView(SelectedGuest, people);
                showDetails.Show();
            }
        }


    }
}