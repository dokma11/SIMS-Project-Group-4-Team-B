using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for GuestsReservationReschedulingView.xaml
    /// </summary>
    public partial class GuestsReservationReschedulingView : Page
    {
        public AccommodationReservationRescheduling SelectedGuest { get; set; }
        public ObservableCollection<AccommodationReservationRescheduling> people { get; set; }

        public GuestsReservationReschedulingViewModel GuestsReservationReschedulingViewModel;

        public List<AccommodationReservationRescheduling> lista { get; set; }
        public GuestsReservationReschedulingView(User owner)
        {
            InitializeComponent();
            DataContext = this;
            GuestsReservationReschedulingViewModel = new GuestsReservationReschedulingViewModel();
            people = new ObservableCollection<AccommodationReservationRescheduling>(GuestsReservationReschedulingViewModel.GetGuestsReservationMove(owner));
            lista = new List<AccommodationReservationRescheduling>(GuestsReservationReschedulingViewModel.GetGuestsReservationMove(owner));
       
        }

        public void Details_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGuest != null)
            {
                var showDetails = new ReschedulingDetailsView(SelectedGuest, people);
                FrameManager.Instance.MainFrame.Navigate(showDetails);
            }
        }


    }
}