using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
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
using static Sims2023.Model.AccommodationReservationRescheduling;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for ReschedulingDetailsView.xaml
    /// </summary>
    public partial class ReschedulingDetailsView : Window
        {
            public AccommodationReservationRescheduling guest { get; set; }
            public AccommodationReservation UpdatedReservationStatus { get; set; }
        public AccommodationReservationService _reservationService;
            public ObservableCollection<AccommodationReservationRescheduling> peoplee;
            public AccommodationReservationReschedulingController _reschedulingController;
        public ReschedulingDetailsView(AccommodationReservationRescheduling SelectedGuest, ObservableCollection<AccommodationReservationRescheduling> people)
        {
            InitializeComponent();
            guest = SelectedGuest;
            DataContext = guest;
            _reschedulingController = new AccommodationReservationReschedulingController();
            peoplee = people;
            UpdatedReservationStatus = new AccommodationReservation();
            _reservationService = new AccommodationReservationService();
        }   

        public string isAccommodationFree
        {
            get
            {
                var message = _reschedulingController.isAccommodationFree(guest) ? "SLOBODAN" : "NIJE SLOBODAN";
                return $"SMJESTAJ {message} U TRAZENOM TERMINU";
            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            var decline = new DeclineRescheduleView(_reschedulingController, guest, this, peoplee);
            decline.Show();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            guest.Status = RequestStatus.Approved;
            UpdatedReservationStatus = _reservationService.GetById(guest.AccommodationReservation.Id);
            UpdatedReservationStatus.StartDate = guest.NewStartDate;
            UpdatedReservationStatus.EndDate = guest.NewEndDate;
            TimeSpan delta = guest.NewEndDate - guest.NewStartDate;
            UpdatedReservationStatus.NumberOfDays = (int)delta.TotalDays;
            _reservationService.Update(UpdatedReservationStatus);
            _reschedulingController.Update(guest);

            peoplee.Remove(guest);
            Close();
        }
    }
}
