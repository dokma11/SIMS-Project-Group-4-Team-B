using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.View.Guest1;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for AllGuestOneReservationsView.xaml
    /// </summary>
    public partial class AllGuestOneReservationsView : Window, IObserver
    {
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        private AccommodationReservationController _accommodationReservationController;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        private AccommodationController _accommodationController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        private AccomodationLocationController _accommodationLocationController;
        public ObservableCollection<AccommodationLocation> AccommodationLocations { get; set; }
        public ObservableCollection<AccommodationGrade> AccommodationGrades { get; set; }

        List<AccommodationReservation> FilteredData=new List<AccommodationReservation>();   

        public User User { get; set; }

        public AllGuestOneReservationsView(User guest1)
        {
            InitializeComponent();
            DataContext = this;

            User = guest1;

            _accommodationReservationController = new AccommodationReservationController();
            _accommodationReservationController.Subscribe(this);

            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.GetAllReservations());

            _accommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAllAccommodations());

            _accommodationLocationController = new AccomodationLocationController();
            AccommodationLocations = new ObservableCollection<AccommodationLocation>(_accommodationLocationController.GetAllAccommodationLocations());

            FilteredData = FindSuitableReservations(AccommodationReservations);
            myDataGrid.ItemsSource = FilteredData;
        }

        private List<AccommodationReservation> FindSuitableReservations(ObservableCollection<AccommodationReservation> accommodationReservations)
        {
            List<AccommodationReservation> FilteredReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in accommodationReservations)
            {
               if( CheckReservation(accommodationReservation))
                {
                    FilteredReservations.Add(accommodationReservation);
                }
            }
            return FilteredReservations;
        }

        private bool CheckReservation(AccommodationReservation accommodationReservation)
        {
            TimeSpan difference = DateTime.Today - accommodationReservation.EndDate;
            if (difference.TotalDays <= 5 && difference.TotalDays>=0 && accommodationReservation.Guest.Id==User.Id && accommodationReservation.Graded==false)
            {
                return true;
            }
            return false;
            
        }

        private void grading_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)myDataGrid.SelectedItem;
            
            if (SelectedAccommodationReservation == null)
            {
                MessageBox.Show("Molimo Vas selektujte smestaj koji zelite da ocenite.");
                return;
            }
            if (!SelectedAccommodationReservation.Graded)
            {
                var AccommodationAndOwnerGradingView = new AccommodationAndOwnerGradingView(SelectedAccommodationReservation, User, _accommodationReservationController);
                AccommodationAndOwnerGradingView.ShowDialog();
                Update();
            }
            else
            {
                MessageBox.Show("Vec ste ocenili ovaj smestaj.");
                return;
            }
        }

        private void renovation_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)myDataGrid.SelectedItem;
            if (SelectedAccommodationReservation == null)
            {
                MessageBox.Show("Molimo Vas selektujte smestaj koji zelite da ocenite.");
                return;
            }
            var AccommodationAndOwnerGradingView = new AccommodationAndOwnerGradingView(SelectedAccommodationReservation,User, _accommodationReservationController);
            AccommodationAndOwnerGradingView.ShowDialog();
        }

        public void Update()
        {
            FilteredData.Clear();
            FilteredData = FindSuitableReservations(AccommodationReservations);
            myDataGrid.ItemsSource = FilteredData;
        }
    }
}
