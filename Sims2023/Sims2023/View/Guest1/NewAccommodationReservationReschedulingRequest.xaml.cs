using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
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

namespace Sims2023.View.Guest1
{
    /// <summary>
    /// Interaction logic for NewAccommodationReservationReschedulingRequest.xaml
    /// </summary>
    public partial class NewAccommodationReservationReschedulingRequest : Window, IObserver
    {
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        private AccommodationReservationService _accommodationReservationService;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        List<AccommodationReservation> FilteredData = new List<AccommodationReservation>();
        public User User { get; set; }
        public NewAccommodationReservationReschedulingRequest(User guest1)
        {
            InitializeComponent();
            DataContext = this;

            User = guest1;

            _accommodationReservationService = new AccommodationReservationService();
            _accommodationReservationService.Subscribe(this);

            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllReservations());

            FilteredData = FindSuitableReservations(AccommodationReservations);
            myDataGrid.ItemsSource = FilteredData;
        }

        private List<AccommodationReservation> FindSuitableReservations(ObservableCollection<AccommodationReservation> accommodationReservations)
        {
            List<AccommodationReservation> FilteredReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in accommodationReservations)
            {
                if (FilterdDataSelection(accommodationReservation, User))
                {
                    FilteredReservations.Add(accommodationReservation);
                }
            }
            return FilteredReservations;
        }

        private bool FilterdDataSelection(AccommodationReservation accommodationReservation, User guest)
        {
            TimeSpan difference = accommodationReservation.StartDate - DateTime.Today;
            if (difference.TotalDays >= 0 && accommodationReservation.Guest.Id == guest.Id)
            {
                return true;
            }
            return false;
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
            FilteredData.Clear();
            FilteredData = FindSuitableReservations(AccommodationReservations);
            myDataGrid.ItemsSource = FilteredData;
        }
    }
}
