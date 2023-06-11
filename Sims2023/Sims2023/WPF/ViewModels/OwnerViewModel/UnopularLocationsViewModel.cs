using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public List<AccommodationReservation> reservations { get; set; }
        public RelayCommand Back { get; set; }
        public RelayCommand CloseAccommodations { get; set; }

        public Location SelectedLocation { get; set; }
        public UnopularLocationsViewModel(User user)
        {
            owner = user;
            _locationService = new LocationService();
            _accommodationService = new AccommodationService();
            _accommodationReservationService = new AccommodationReservationService();
            reservations = _accommodationReservationService.GetReservationsForOwner(owner);
            locations =_accommodationService.GetOwnerLocations(_accommodationService.GetOwnerAccommodations(_accommodationService.GetAllAccommodations(),owner));
            unvisitedLocations = _accommodationReservationService.GetUnvisitedLocations(locations);
            Locations = new ObservableCollection<Location>(_locationService.GetUnpopularLocations(reservations,unvisitedLocations));

        }

        public bool CanExecute_BackCommand(object obj)
        {
            return true;
        }

        public void Executed_BackCommand(object obj)
        {
            if (FrameManager.Instance.MainFrame.CanGoBack)
            {
                FrameManager.Instance.MainFrame.GoBack();
            }
        }

        public void Executed_CloseAccommodationsCommand(object obj)
        {
            if (SelectedLocation != null)
            {
                List<Accommodation> accommodationsToDelete = _accommodationService.GetAllAccommodations()
                    .Where(accommodation => accommodation.Location.Id == SelectedLocation.Id)
                    .ToList();

                foreach (Accommodation accommodation in accommodationsToDelete)
                {
                    _accommodationService.Delete(accommodation);
                }
                Locations.Remove(SelectedLocation);
                ToastNotificationService.ShowInformation("Uspiješno brisanje smještaja sa željene lokacije");
            }

        }

        public bool CanExecute_CloseAccommodationsCommand(object obj)
        {
            return true;
        }
    }
}
