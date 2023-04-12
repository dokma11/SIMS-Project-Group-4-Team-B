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
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for Guest2TourListView.xaml
    /// </summary>
    public partial class Guest2TourListView : Window,IObserver
    {
        public Tour SelectedTour { get; set; }
        public User User { get; set; }
        //public List<Tour> Tours { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }   


        public TourService _tourService;
        public TourReservationService _tourReservationService;

        public Guest2TourListView(User user)
        {
            InitializeComponent();
            DataContext = this;

            _tourReservationService = new TourReservationService();
            _tourReservationService.Subscribe(this);

            _tourService = new TourService();
            _tourService.Subscribe(this);

            User = user;
            SelectedTour=new Tour();

            Tours=GetGuestAllReservations();


            //Tours = new ObservableCollection<Tour>(_tourService.GetAll());

        }


        private ObservableCollection<Tour> GetGuestAllReservations()
        {
            ObservableCollection<Tour> Tours = new ObservableCollection<Tour>();
            foreach(TourReservation reservation in _tourReservationService.GetAll() )
            {
                if (reservation.User.Id == User.Id)
                {
                    Tours.Add(reservation.Tour);
                }
            }

            return Tours;

        }

        private void RateTour_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SeeActiveTour_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }

    
}
