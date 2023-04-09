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
using Sims2023.Controller;
using Sims2023.Model;
using Sims2023.Observer;

namespace Sims2023.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationReservationCancellationView.xaml
    /// </summary>
    public partial class AccommodationReservationCancellationView : Window, IObserver
    {
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        private AccommodationReservationController _accommodationReservationController;

        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        private AccommodationCancellationController _accommodationCancellationController;
        public ObservableCollection<AccommodationCancellation> AccommodationCancellations { get; set; }

        private AccommodationController _accommodationController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<AccommodationGrade> AccommodationGrades { get; set; }

        List<AccommodationReservation> FilteredData = new List<AccommodationReservation>();
        public User User { get; set; }
        public AccommodationReservationCancellationView(User guest1)
        {
            InitializeComponent();
            DataContext = this;

            User = guest1;

            _accommodationReservationController = new AccommodationReservationController();
            _accommodationReservationController.Subscribe(this);

            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.GetAllReservations());

            _accommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAllAccommodations());

            _accommodationCancellationController = new AccommodationCancellationController();
            AccommodationCancellations = new ObservableCollection<AccommodationCancellation>(_accommodationCancellationController.GetAllAccommodationCancellations());

            FilteredData = FindSuitableReservations(AccommodationReservations);
            myDataGrid.ItemsSource = FilteredData;

        }
        private List<AccommodationReservation> FindSuitableReservations(ObservableCollection<AccommodationReservation> accommodationReservations)
        {
            List<AccommodationReservation> FilteredReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in accommodationReservations)
            {
                if(FilterdDataSelection(accommodationReservation,User))
                {
                    FilteredReservations.Add(accommodationReservation);
                }
            }
            return FilteredReservations;
        }

        private bool FilterdDataSelection(AccommodationReservation accommodationReservation,User guest)
        {
            TimeSpan difference = accommodationReservation.StartDate - DateTime.Today;
            if (difference.TotalDays >= 0 && accommodationReservation.Guest.Id == guest.Id)
            {
                return true;
            }
            return false;
        }

        private void cancellation_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)myDataGrid.SelectedItem;
            if(CheckSelectedAccommodationReservation(SelectedAccommodationReservation))
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da obrisete ovu rezervaciju?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    CreateAccommodationCancellation(SelectedAccommodationReservation);
                    DeleteAccommodationReservation(SelectedAccommodationReservation);
                }
                else
                {
                    return;
                }
            }
            return;
        }

        private bool CheckSelectedAccommodationReservation(AccommodationReservation selectedAccommodationReservation)
        {
            if (selectedAccommodationReservation == null)
            {
                MessageBox.Show("Molimo Vas selektujte rezervaciju koji zelite obrisete.");
                return false;
            }
            if (CancellationIsPossible(selectedAccommodationReservation))
            {
                MessageBox.Show("Nije moguce otkazati rezervaciju. Pocetak boravka je previse blizu");
                return false;
            }
            return true;
        }

        private bool CancellationIsPossible(AccommodationReservation selectedAccommodationReservation)
        {
            TimeSpan difference = selectedAccommodationReservation.StartDate- DateTime.Today;
            if (difference.TotalDays <= selectedAccommodationReservation.Accommodation.CancelDays)
            {
                return true;
            }
            return false;
        }

        private void DeleteAccommodationReservation(AccommodationReservation selectedAccommodationReservation)
        {
            foreach (AccommodationReservation accommodationResrvation in AccommodationReservations)
            {
                if(accommodationResrvation.Id==selectedAccommodationReservation.Id)
                {
                    RemoveAccommodationReservation(selectedAccommodationReservation);
                    return;
                }
            }
        }

        private void CreateAccommodationCancellation(AccommodationReservation selectedAccommodationReservation)
        {
            AccommodationCancellation accommodationCancellation = new();
            accommodationCancellation.StartDate = selectedAccommodationReservation.StartDate;
            accommodationCancellation.EndDate = selectedAccommodationReservation.EndDate;   
            accommodationCancellation.NumberOfDays=selectedAccommodationReservation.NumberOfDays;
            accommodationCancellation.Accommodation = selectedAccommodationReservation.Accommodation;
            accommodationCancellation.Guest = selectedAccommodationReservation.Guest;
            accommodationCancellation.Notified = false;

            if (accommodationCancellation != null)
            {
                _accommodationCancellationController.Create(accommodationCancellation);
                MessageBox.Show("Uspesno ste otkazali rezervaciju.");

            }
        }

        private void RemoveAccommodationReservation(AccommodationReservation selectedAccommodationReservation)
        {
            AccommodationReservations.Remove(selectedAccommodationReservation);
            _accommodationReservationController.Delete(selectedAccommodationReservation);
            Update();
        }

        public void Update()
        {
            FilteredData.Clear();
            FilteredData = FindSuitableReservations(AccommodationReservations);
            myDataGrid.ItemsSource = FilteredData;
        }
    }
}
