using Sims2023.Application.Services;
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
using static Sims2023.Domain.Models.AccommodationReservationRescheduling;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for ReschedulingDetailsView.xaml
    /// </summary>
    public partial class ReschedulingDetailsView : Window
    {
        public AccommodationReservationRescheduling guest { get; set; }
        public AccommodationReservation UpdatedReservationStatus { get; set; }

        public ObservableCollection<AccommodationReservationRescheduling> peoplee;

        public AccommodationReservationReschedulingService _reschedulingController;

        public ReschedulingDetailsViewModel ReschedulingDetailsViewModel;
        public ReschedulingDetailsView(AccommodationReservationRescheduling SelectedGuest, ObservableCollection<AccommodationReservationRescheduling> people)
        {
            InitializeComponent();
            ReschedulingDetailsViewModel = new ReschedulingDetailsViewModel(SelectedGuest, people, this);
            guest = SelectedGuest;
            DataContext = guest;
            peoplee = people;

        }

        public string isAccommodationFree
        {
            get
            {
                var message = ReschedulingDetailsViewModel.isAccommodationFree(guest) ? "SLOBODAN" : "NIJE SLOBODAN";
                return $"SMJESTAJ {message} U TRAZENOM TERMINU";
            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            var decline = new DeclineRescheduleView(guest, this, peoplee);
            decline.Show();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ReschedulingDetailsViewModel.button2_Click(sender, e);
        }
    }
}