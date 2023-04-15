using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class GuestLiveTrackingTourViewModel
    {
        public Tour Tour;
        public GuestLiveTrackingTourView GuestLiveTrackingTourView { get; set; }
        public KeyPoint CurrentKeyPoint { get; set; }

        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }


        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private TourService _tourService;

        public GuestLiveTrackingTourViewModel(Tour tour,GuestLiveTrackingTourView guestLiveTrackingTourView)
        {

            Tour = tour;    
            GuestLiveTrackingTourView = guestLiveTrackingTourView;

            _locationService = new LocationService();
            _keyPointService = new KeyPointService();
            _tourService = new TourService();

            Locations = new ObservableCollection<Location>(_locationService.GetAll());
            Tours = new ObservableCollection<Tour>(_tourService.GetAll());
           

            _tourService.AddLocationsToTour(Locations, Tours);//need method just for one tour
            guestLiveTrackingTourView.keyPointTextBlock.Text=(_keyPointService.GetCurrentKeyPoint(tour)).Name;
            
        }

        public void Cancel_Click()
        {
            GuestLiveTrackingTourView.Close();
        }

    }
}
