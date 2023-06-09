using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest1Views;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for AccommodationReservationReschedulingView.xaml
    /// </summary>
    public partial class AccommodationReservationReschedulingViewModel : Window, IObserver
    {
        private AccommodationReservationReschedulingService _accommodationReservationReschedulingService;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        public ObservableCollection<AccommodationReservationRescheduling> FilteredData = new ObservableCollection<AccommodationReservationRescheduling>();
        public User User { get; set; }
        public AccommodationReservationRescheduling SelectedAccommodationReservationRescheduling { get; set; }

        AccommodationReservationReschedulingView AccommodationReservationReschedulingView;

        public AccommodationReservationReschedulingViewModel(AccommodationReservationReschedulingView accommodationReservationReschedulingView, User guest1, ObservableCollection<AccommodationReservationRescheduling> filteredData, ObservableCollection<AccommodationReservationRescheduling> accommodationReservationReschedulings, AccommodationReservationReschedulingService accommodationReservationReschedulingService)
        {
            AccommodationReservationReschedulingView = accommodationReservationReschedulingView;

            User = guest1;
            FilteredData = filteredData;
            AccommodationReservationReschedulings = accommodationReservationReschedulings;

            _accommodationReservationReschedulingService = accommodationReservationReschedulingService;
            _accommodationReservationReschedulingService.Subscribe(this);

            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingService.GetAllReservationReschedulings());
            FilteredData = _accommodationReservationReschedulingService.FindSuitableReservationReschedulings(User, AccommodationReservationReschedulings);
            AccommodationReservationReschedulingView.myDataGrid.ItemsSource = FilteredData;
        }

        public void report_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ova funkcionalnost jos uvek nije implementirana.");
        }

        public void comment_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservationRescheduling = (AccommodationReservationRescheduling)AccommodationReservationReschedulingView.myDataGrid.SelectedItem;

            if (SelectedAccommodationReservationRescheduling == null)
            {
                MessageBox.Show("Molimo Vas selektujte zahtev.");
                return;
            }
            MessageBox.Show($"Komentar Vlasnika smeštaja:\n\n{SelectedAccommodationReservationRescheduling.Comment}");
            return;
        }

        public void Update()
        {
            FilteredData.Clear();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingService.GetAllReservationReschedulings());
            FilteredData = _accommodationReservationReschedulingService.FindSuitableReservationReschedulings(User, AccommodationReservationReschedulings);
            AccommodationReservationReschedulingView.myDataGrid.ItemsSource = FilteredData;
        }
    }
}
