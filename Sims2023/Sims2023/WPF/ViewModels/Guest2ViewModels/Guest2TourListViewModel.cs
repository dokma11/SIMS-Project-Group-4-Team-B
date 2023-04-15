using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class Guest2TourListViewModel
    {
        public Tour SelectedTour { get; set; }
        public User User { get; set; }
        
        public ObservableCollection<Tour> Tours { get; set; }  
        public ObservableCollection<Location> Locations { get; set; }   


        public TourService _tourService;
        public TourReservationService _tourReservationService;

        public LocationService _locationService;
        public Guest2TourListViewModel(User user,Guest2TourListView guest2TourListView)
        {
            _tourReservationService = new TourReservationService();
            _tourService = new TourService();
            _locationService = new LocationService();

            Locations = new ObservableCollection<Location>(_locationService.GetAll());
            Tours = new ObservableCollection<Tour>(_tourReservationService.GetByUser(user));

            User = user;
            SelectedTour = new Tour();
            
            _tourService.AddLocationsToTour(Locations, Tours);
        }

        
        public void RateTour_Click()
        {
            if (_tourService.CanRateTour(SelectedTour))
            {
                RateTourView rateTourView = new RateTourView(User, SelectedTour);
                rateTourView.Show();
            }
            else
            {
                MessageBox.Show("Ne mozete da ocenite nezavrsenu turu");
            }
        }

        public void SeeActiveTour_Click()
        {
            if (_tourService.CanSeeTour(SelectedTour))
            {
                GuestLiveTrackingTourView guestLiveTrackingTourView = new GuestLiveTrackingTourView(SelectedTour);
                guestLiveTrackingTourView.Show();
            }
            else
            {
                MessageBox.Show("Izaberite aktivnu turu");
            }
        }

        
    }
}
