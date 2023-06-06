using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.View;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class PopularLocationsViewModel
    {
        public LocationService _locations;
        public AccommodationReservationService _reservations;
        public ObservableCollection<Location> Locations { get; set; }
        public Location SelectedLocation { get; set; }
        public User owner { get; set; }
        public PopularLocationsViewModel(User ownerr)
        { 
            _locations = new LocationService();
            _reservations = new AccommodationReservationService();

            owner = ownerr;
            Locations = new ObservableCollection<Location>(_locations.GetPopularLocations(_reservations.GetAllReservations()));

        }

        public void CreateAccommodation_Click()
        {

            if (SelectedLocation != null)
            {
              AccommodationRegistrationAlternativeView registrationView = new AccommodationRegistrationAlternativeView( owner, SelectedLocation.City, SelectedLocation.Country);
              FrameManager.Instance.MainFrame.Navigate(registrationView);
            }
        }

    }
}
