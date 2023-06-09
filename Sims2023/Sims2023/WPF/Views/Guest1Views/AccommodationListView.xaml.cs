using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims2023.WPF.Views.Guest1Views
{
    public partial class AccommodationListView : Page
    {
        public AccommodationListViewModel AccommodationListViewModel;

        public Frame MainFrame;
        public Accommodation SelectedAccommodation { get; set; }
        public User User { get; set; }
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        private AccommodationReservationReschedulingService _accommodationReservationReschedulingService;


        public AccommodationListView(User guest1, Frame mainFrame)
        {
            InitializeComponent();
            AccommodationListViewModel = new AccommodationListViewModel(this, guest1);
            DataContext = AccommodationListViewModel;

            _accommodationReservationReschedulingService = new AccommodationReservationReschedulingService();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingService.GetAllReservationReschedulings());
            User = guest1;
            MainFrame = mainFrame;
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void MakeReservation(object sender, ExecutedRoutedEventArgs e)
        {
            SelectedAccommodation = (Accommodation)myDataGrid.SelectedItem;

            if (SelectedAccommodation == null)
            {
                MessageBox.Show("Molimo Vas selektujte smeštaj koji želite da rezervišete.");
                return;
            }
            MainFrame.Navigate(new AccommodationReservationDateView(-1, SelectedAccommodation, User, AccommodationReservationReschedulings, _accommodationReservationReschedulingService, MainFrame));
        }

        public void ShowDetailedView(object sender, ExecutedRoutedEventArgs e)
        {
            SelectedAccommodation = (Accommodation)myDataGrid.SelectedItem;

            if (SelectedAccommodation == null)
            {
                MessageBox.Show("Molimo Vas selektujte smeštaj koji želite da prikažete detaljnije.");
                return;
            }
            MainFrame.Navigate(new AccommodationDetailedView(User, SelectedAccommodation, MainFrame));
        }

        public void Search(object sender, ExecutedRoutedEventArgs e)
        {
            AccommodationListViewModel.SearchAccommodation_Click(sender, e);
        }

        public void ClearSearch(object sender, ExecutedRoutedEventArgs e)
        {
            AccommodationListViewModel.GiveUpSearch_Click(sender, e);
        }
    }
}
