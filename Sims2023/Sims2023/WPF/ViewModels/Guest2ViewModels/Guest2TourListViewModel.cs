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
using System.Globalization;
using Sims2023.WPF.Commands;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class Guest2TourListViewModel
    {
        public TourReservation SelectedTourReservation { get; set; }
        public User User { get; set; }

        public ObservableCollection<TourReservation> TourReservations;   


        public TourService _tourService;
        public TourReservationService _tourReservationService;
        public RelayCommand RateTour_Click { get; set; }
        public RelayCommand SeeActive_Click { get; set; }

        
        public Guest2TourListViewModel(User user,Guest2TourListView guest2TourListView)
        {
            _tourReservationService = new TourReservationService();
            _tourService = new TourService();
            
            TourReservations =new ObservableCollection<TourReservation>( _tourReservationService.GetByUser(user));

            User = user;
            SelectedTourReservation = null;
            this.SeeActive_Click = new RelayCommand(Execute_SeeActive_Click, CanExecute_NavigateCommand);
            this.RateTour_Click = new RelayCommand(Execute_RateTour_Click, CanExecute_NavigateCommand);
            
        }
        public void Execute_RateTour_Click(object obj)
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
                if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
                {
                    MessageBox.Show("Izaberite zavrsenu turu");
                }
                else
                {
                    MessageBox.Show("Choose finished tour");
                }
            }
        }

        private void Execute_SeeActive_Click(object obj)
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
                if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
                {
                    MessageBox.Show("Izaberite aktivnu turu");
                }
                else
                {
                    MessageBox.Show("Choose active tour");
                }
            }
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }


        public bool IsNull(TourReservation tourReservation)
        {
            if(tourReservation == null)
            {
                if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
                {
                    MessageBox.Show("Izaberite turu");
                }
                else
                {
                    MessageBox.Show("Choose the tour");
                }
                return true;
            }
            return false;
        }

        public bool IsFinished(Tour tour)
        {
            return tour.CurrentState == ToursState.Finished;
        }

        public bool IsStarted(Tour tour)
        {
            return tour.CurrentState == ToursState.Started;
        }



    }
}
