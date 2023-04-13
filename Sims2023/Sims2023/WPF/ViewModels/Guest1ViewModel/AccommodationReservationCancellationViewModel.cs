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
using Sims2023.Observer;
using Sims2023.Domain.Models;
using Sims2023.Application.Services;
using Sims2023.WPF.Views.Guest1Views;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for AccommodationReservationCancellationView.xaml
    /// </summary>
    public partial class AccommodationReservationCancellationViewModel : Window, IObserver
    {
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        private AccommodationReservationService _accommodationReservationService;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        private AccommodationCancellationService _accommodationCancellationController;
        public ObservableCollection<AccommodationCancellation> AccommodationCancellations { get; set; }

        private AccommodationService _accommodationService;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<AccommodationGrade> AccommodationGrades { get; set; }

        List<AccommodationReservation> FilteredData = new List<AccommodationReservation>();
        public User User { get; set; }

        AccommodationReservationCancellationView AccommodationReservationCancellationView;
        public AccommodationReservationCancellationViewModel(AccommodationReservationCancellationView accommodationReservationCancellationView,User guest1)
        {
            AccommodationReservationCancellationView = accommodationReservationCancellationView;

            User = guest1;

            _accommodationReservationService = new AccommodationReservationService();
            _accommodationReservationService.Subscribe(this);

            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllReservations());

            _accommodationService = new AccommodationService();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodations());

            _accommodationCancellationController = new AccommodationCancellationService();
            AccommodationCancellations = new ObservableCollection<AccommodationCancellation>(_accommodationCancellationController.GetAllAccommodationCancellations());

            FilteredData = _accommodationReservationService.FindSuitableReservations(User);
            AccommodationReservationCancellationView.myDataGrid.ItemsSource = FilteredData;

        }

        public void cancellation_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)AccommodationReservationCancellationView.myDataGrid.SelectedItem;
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

        public bool CheckSelectedAccommodationReservation(AccommodationReservation selectedAccommodationReservation)
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

        public bool CancellationIsPossible(AccommodationReservation selectedAccommodationReservation)
        {
            TimeSpan difference = selectedAccommodationReservation.StartDate- DateTime.Today;
            if (difference.TotalDays <= selectedAccommodationReservation.Accommodation.CancelDays)
            {
                return true;
            }
            return false;
        }

        public void DeleteAccommodationReservation(AccommodationReservation selectedAccommodationReservation)
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

        public void CreateAccommodationCancellation(AccommodationReservation selectedAccommodationReservation)
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

        public void RemoveAccommodationReservation(AccommodationReservation selectedAccommodationReservation)
        {
            AccommodationReservations.Remove(selectedAccommodationReservation);
            _accommodationReservationService.Delete(selectedAccommodationReservation);
            Update();
        }

        public void Update()
        {
            FilteredData.Clear();
            FilteredData = _accommodationReservationService.FindSuitableReservations(User);
            AccommodationReservationCancellationView.myDataGrid.ItemsSource = FilteredData;
        }
    }
}
