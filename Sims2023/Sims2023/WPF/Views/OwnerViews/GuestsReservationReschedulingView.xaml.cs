using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            var showDetails = new ReschedulingDetailsView(SelectedGuest, people);
            showDetails.Show();
        }


    }
}
