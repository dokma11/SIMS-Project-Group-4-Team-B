using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;

namespace Sims2023.WPF.ViewModels
{
    public partial class MarkGuestsPresentViewModel
    {
        public KeyPoint KeyPoint { get; set; }
        public ObservableCollection<User> GuestsToDisplay { get; set; }

        private UserService _userService;
        private TourReservationService _tourReservationService;
        public List<User> MarkedGuests { get; set; }
        public MarkGuestsPresentViewModel(KeyPoint keyPoint, TourReservationService tourReservationService, UserService userService, List<User> markedGuests)
        {
            KeyPoint = keyPoint;

            _tourReservationService = tourReservationService;
            _userService = userService;

            MarkedGuests = markedGuests;

            GuestsToDisplay = new ObservableCollection<User>(_userService.GetGuestsThatReserved(KeyPoint, MarkedGuests));
        }

        public List<User> InitializeGuestsToDisplay(KeyPoint keyPoint, List<User> markedGuests)
        {
            return _userService.GetGuestsThatReserved(keyPoint, markedGuests);
        }

        public void AddMarkedGuests(User guest)
        {
            KeyPoint.ShowedGuestsIds.Add(guest.Id);
            KeyPoint.ShowedGuestsIdsString += guest.Id.ToString() + " ";
            MarkedGuests.Add(guest);
            ShouldConfirmParticipation(guest);
        }

        private void ShouldConfirmParticipation(User user)
        {
            foreach (var tourReservation in _tourReservationService.GetAll())
            {
                if (tourReservation.User.Id == user.Id && tourReservation.Tour.Id == KeyPoint.Tour.Id)
                {
                    tourReservation.ShouldConfirmParticipation = true;
                    _tourReservationService.Save();
                }
            }
        }
    }
}
