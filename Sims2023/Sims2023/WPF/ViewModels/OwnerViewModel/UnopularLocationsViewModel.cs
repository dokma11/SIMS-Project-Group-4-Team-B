using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class UnopularLocationsViewModel
    {
        public User owner { get; set; }
        public LocationService _locationService;
        public AccommodationService _accommodationService;
        public AccommodationReservationService _accommodationReservationService;
         
        public ObservableCollection<Location> Locations { get; set; }
        public List<Location> locations { get; set; }
        public List<Location> unvisitedLocations{ get; set; }
        public UnopularLocationsViewModel(User user)
        {
            owner = user;
            _locationService = new LocationService();
            _accommodationService = new AccommodationService();
            _accommodationReservationService = new AccommodationReservationService();
            
            locations =_accommodationService.GetOwnerLocations(_accommodationService.GetOwnerAccommodations(_accommodationService.GetAllAccommodations(),owner));
            unvisitedLocations = _accommodationReservationService.GetUnvisitedLocations(locations);
            Locations = new ObservableCollection<Location>(unvisitedLocations);

        }
    }
}
