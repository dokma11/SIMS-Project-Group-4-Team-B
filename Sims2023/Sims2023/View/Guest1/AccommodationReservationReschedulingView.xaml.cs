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
    /// Interaction logic for AccommodationReservationReschedulingView.xaml
    /// </summary>
    public partial class AccommodationReservationReschedulingView : Window, IObserver
    {
        public AccommodationReservationRescheduling SelectedAccommodationReservationRescheduling { get; set; }

        private AccommodationReservationReschedulingController _accommodationReservationReschedulingController;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        List<AccommodationReservationRescheduling> FilteredData = new List<AccommodationReservationRescheduling>();
        public User User { get; set; }

        public AccommodationReservationReschedulingView(User guest1)
        {
            InitializeComponent();
            DataContext = this;

            User= guest1;

            _accommodationReservationReschedulingController = new AccommodationReservationReschedulingController();
            _accommodationReservationReschedulingController.Subscribe(this);
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingController.GetAllReservationReschedulings());

            FilteredData = FindSuitableReservationReschedulings(AccommodationReservationReschedulings);
            myDataGrid.ItemsSource = FilteredData;
        }

        private List<AccommodationReservationRescheduling> FindSuitableReservationReschedulings(ObservableCollection<AccommodationReservationRescheduling> accommodationReservationReschedulings)
        {
            List<AccommodationReservationRescheduling> FilteredReservationReschedulings = new List<AccommodationReservationRescheduling>();
            foreach (AccommodationReservationRescheduling accommodationReservationRescheduling in accommodationReservationReschedulings)
            {
                if (FilterdDataSelection(accommodationReservationRescheduling, User))
                {
                    FilteredReservationReschedulings.Add(accommodationReservationRescheduling);
                }
            }
            return FilteredReservationReschedulings;
        }

        private bool FilterdDataSelection(AccommodationReservationRescheduling accommodationReservationRescheduling, User guest)
        {
            TimeSpan difference = accommodationReservationRescheduling.AccommodationReservation.StartDate - DateTime.Today;
            if (difference.TotalDays >= 0 && accommodationReservationRescheduling.AccommodationReservation.Guest.Id == guest.Id)
            {
                return true;
            }
            return false;
        }

        private void newRequest_Click(object sender, RoutedEventArgs e)
        {
            var newAccommodationReservationReschedulingRequest = new NewAccommodationReservationReschedulingRequest(User);
            newAccommodationReservationReschedulingRequest.Show();
            Update();
        }

        private void report_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ova funkcionalnost jos uvek nije implementirana.");
        }

        private void comment_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservationRescheduling = (AccommodationReservationRescheduling)myDataGrid.SelectedItem;

            if (SelectedAccommodationReservationRescheduling == null)
            {
                MessageBox.Show("Molimo Vas selektujte zahtev.");
                return;
            }
            MessageBox.Show($"Komentar Vlasnika smestaja:\n\n{SelectedAccommodationReservationRescheduling.Comment}");
            return;
        }

        public void Update()
        {
            FilteredData.Clear();
            FilteredData = FindSuitableReservationReschedulings(AccommodationReservationReschedulings);
            myDataGrid.ItemsSource = FilteredData;
        }
    }
}
