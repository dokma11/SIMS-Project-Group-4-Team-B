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
        public TourReservation SelectedTourReservation { get; set; }
        public User User { get; set; }

        public ObservableCollection<TourReservation> TourReservations;   


        public TourService _tourService;
        public TourReservationService _tourReservationService;

        
        public Guest2TourListViewModel(User user,Guest2TourListView guest2TourListView)
        {
            _tourReservationService = new TourReservationService();
            _tourService = new TourService();
            
            TourReservations =new ObservableCollection<TourReservation>( _tourReservationService.GetByUser(user));

            User = user;
            SelectedTourReservation = null;
            
        }
        public void RateTour_Click()
        {
            if (IsNull(SelectedTourReservation))
                return;
            if (IsFinished(SelectedTourReservation.Tour))
            {
                RateTourView rateTourView = new RateTourView(User, SelectedTourReservation.Tour);
                rateTourView.Show();
            }
            else
            {
                MessageBox.Show("Izaberite zavrsenu turu");
            }
        }

        public void SeeActiveTour_Click()
        {
            if(IsNull(SelectedTourReservation))
                return ;
            if (IsStarted(SelectedTourReservation.Tour))
            {
                GuestLiveTrackingTourView guestLiveTrackingTourView = new GuestLiveTrackingTourView(SelectedTourReservation.Tour);
                guestLiveTrackingTourView.Show();
            }
            else
            {
                MessageBox.Show("Izaberite aktivnu turu");
            }
        }

        public bool IsNull(TourReservation tourReservation)
        {
            if(tourReservation == null)
            {
                MessageBox.Show("Izaberite turu");
                return true;
            }
            return false;
        }

        public bool IsFinished(Tour tour)//new for guest2
        {
            return tour.CurrentState == ToursState.Finished;
        }

        public bool IsStarted(Tour tour)//new for guest2
        {
            return tour.CurrentState == ToursState.Started;
        }



    }
}
