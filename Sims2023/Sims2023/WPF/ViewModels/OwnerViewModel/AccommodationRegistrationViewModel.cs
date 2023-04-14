using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class AccommodationRegistrationViewModel
    {
        private AccommodationService _accommodationService;

        private Accommodation Accommodation { get; set; }
        public User User { get; set; }
        public AccommodationRegistrationView _accommodationRegistrationView; 
        private LocationService _locationService;

        public AccommodationRegistrationViewModel(AccommodationService accommodationCtrl1, User owner)
        {
            _accommodationService = accommodationCtrl1;
            User = owner;
            _locationService = new LocationService();
    
        }

        public void CreateLocation(Location location)
        {
            _locationService.Create(location);
        }

        public void CreateAccommodation(Accommodation accommodation)
        {
            _accommodationService.Create(accommodation);
        }


    }
}
