using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest1Views;
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

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for AccommodationReservationReschedulingView.xaml
    /// </summary>
    public partial class AccommodationReservationReschedulingViewModel : Window, IObserver
    {     
        private AccommodationReservationReschedulingService _accommodationReservationReschedulingController;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        List<AccommodationReservationRescheduling> FilteredData = new List<AccommodationReservationRescheduling>();
        public User User { get; set; }
        public AccommodationReservationRescheduling SelectedAccommodationReservationRescheduling { get; set; }

        AccommodationReservationReschedulingView AccommodationReservationReschedulingView;

        public AccommodationReservationReschedulingViewModel(AccommodationReservationReschedulingView accommodationReservationReschedulingView,User guest1)
        {
            AccommodationReservationReschedulingView = accommodationReservationReschedulingView;

            User = guest1;

            _accommodationReservationReschedulingController = new AccommodationReservationReschedulingService();
            _accommodationReservationReschedulingController.Subscribe(this);
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingController.GetAllReservationReschedulings());

            FilteredData = _accommodationReservationReschedulingController.FindSuitableReservationReschedulings(User);
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
            MessageBox.Show($"Komentar Vlasnika smestaja:\n\n{SelectedAccommodationReservationRescheduling.Comment}");
            return;
        }

        public void Update()
        {
            FilteredData.Clear();
            FilteredData = _accommodationReservationReschedulingController.FindSuitableReservationReschedulings(User);
            AccommodationReservationReschedulingView.myDataGrid.ItemsSource = FilteredData;
        }
    }
}
