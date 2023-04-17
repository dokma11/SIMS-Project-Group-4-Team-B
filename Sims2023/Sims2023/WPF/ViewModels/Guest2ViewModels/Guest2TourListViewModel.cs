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

        public ObservableCollection<Tour> Tours;   


        public TourService _tourService;
        public TourReservationService _tourReservationService;

        
        public Guest2TourListViewModel(User user,Guest2TourListView guest2TourListView)
        {
            _tourReservationService = new TourReservationService();
            _tourService = new TourService();
            
            Tours =new ObservableCollection<Tour>( _tourReservationService.GetByUser(user));

            User = user;
            SelectedTour = null;
            
        }
        public void RateTour_Click()
        {
            if (IsNull(SelectedTour))
                return;
            if (_tourService.CanRateTour(SelectedTour))
            {
                RateTourView rateTourView = new RateTourView(User, SelectedTour);
                rateTourView.Show();
            }
            else
            {
                MessageBox.Show("Izaberite zavrsenu turu");
            }
        }

        public void SeeActiveTour_Click()
        {
            if(IsNull(SelectedTour))
                return ;
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

        public bool IsNull(Tour tour)
        {
            if(tour == null)
            {
                MessageBox.Show("Izaberite turu");
                return true;
            }
            return false;
        }

        
    }
}
