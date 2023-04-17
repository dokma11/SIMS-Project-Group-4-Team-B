using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
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

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for NewAccommodationReservationReschedulingRequest.xaml
    /// </summary>
    public partial class NewAccommodationReservationReschedulingRequestView : Window, IObserver
    {
        NewAccommodationReservationReschedulingRequestViewModel NewAccommodationReservationReschedulingRequestViewModel;
        public AccommodationReservation SelectedAccommodationReservation { get; set; }
        public User User { get; set; }

        public NewAccommodationReservationReschedulingRequestView(User guest1)
        {
            InitializeComponent();
            NewAccommodationReservationReschedulingRequestViewModel = new NewAccommodationReservationReschedulingRequestViewModel(this, guest1);
            DataContext = NewAccommodationReservationReschedulingRequestViewModel;
            User = guest1;
        }

        private void makeRequest_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)myDataGrid.SelectedItem;
            if (SelectedAccommodationReservation == null)
            {
                MessageBox.Show("Molimo Vas selektujte rezervaciju koji zelite obrisete.");
                return;
            }
            var accommodationReservationDateView = new AccommodationReservationDateView(SelectedAccommodationReservation.Id, SelectedAccommodationReservation.Accommodation, User);
            accommodationReservationDateView.Show();
            Close();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void Update()
        {
            NewAccommodationReservationReschedulingRequestViewModel.Update();
        }
    }
}
