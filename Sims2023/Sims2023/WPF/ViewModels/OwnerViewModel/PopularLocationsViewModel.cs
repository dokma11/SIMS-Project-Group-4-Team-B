using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class PopularLocationsViewModel
    {
        public LocationService _locations;
        public AccommodationReservationService _reservations;
        public ObservableCollection<Location> Locations { get; set; }
        public PopularLocationsViewModel()
        { 
            _locations = new LocationService();
            _reservations = new AccommodationReservationService();


            Locations = new ObservableCollection<Location>(_locations.GetPopularLocations(_reservations.GetAllReservations()));

        }
    }
}
