using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationReservationReschedulingView.xaml
    /// </summary>
    public partial class AccommodationReservationReschedulingView : Page
    {
        private AccommodationReservationReschedulingService _accommodationReservationReschedulingService;

        public AccommodationReservationReschedulingViewModel AccommodationReservationReschedulingViewModel;
        public User User { get; set; }
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }
        public ObservableCollection<AccommodationReservationRescheduling> FilteredData { get; set; }

        Frame MainFrame;

        public AccommodationReservationReschedulingView(User guest1, Frame mainFrame)
        {
            InitializeComponent();
            FilteredData = new ObservableCollection<AccommodationReservationRescheduling>();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>();
            _accommodationReservationReschedulingService = new AccommodationReservationReschedulingService();

            AccommodationReservationReschedulingViewModel = new AccommodationReservationReschedulingViewModel(this, guest1,FilteredData, AccommodationReservationReschedulings, _accommodationReservationReschedulingService);
            DataContext = AccommodationReservationReschedulingViewModel;
            User = guest1;
            MainFrame = mainFrame;
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void MakeNewRequest(object sender, ExecutedRoutedEventArgs e)
        {
            MainFrame.Navigate(new NewAccommodationReservationReschedulingRequestView(User, FilteredData, _accommodationReservationReschedulingService, MainFrame));
            AccommodationReservationReschedulingViewModel.Update();
        }

        public void GetReport(object sender, ExecutedRoutedEventArgs e)
        {
            AccommodationReservationReschedulingViewModel.report_Click(sender, e);
        }

        public void ShowComment(object sender, ExecutedRoutedEventArgs e)
        {
            AccommodationReservationReschedulingViewModel.comment_Click(sender, e);
        }
    }
}
