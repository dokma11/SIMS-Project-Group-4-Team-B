using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class MarkGuestsPresentViewModel
    {
        public KeyPoint KeyPoint { get; set; }
        public ObservableCollection<User> GuestsToDisplay { get; set; }

        private UserService _userService;
        private TourReservationService _tourReservationService;
        private KeyPointService _keyPointService;
        public List<User> MarkedGuests { get; set; }
        public MarkGuestsPresentViewModel(KeyPoint selectedKeyPoint, TourReservationService tourReservationService, UserService userService, KeyPointService keyPointService, List<User> markedGuests)
        {
            _tourReservationService = tourReservationService;
            _userService = userService;
            _keyPointService = keyPointService;

            KeyPoint = selectedKeyPoint;
            MarkedGuests = markedGuests;

            GuestsToDisplay = new ObservableCollection<User>(_userService.GetGuestsWithReservations(KeyPoint, MarkedGuests));
        }

        public void AddMarkedGuests(List<User> items)
        {
            foreach (User guest in items)
            {
                _keyPointService.AddGuestsId(KeyPoint, guest.Id);
                MarkedGuests.Add(guest);
                ShouldConfirmParticipation(guest);
            }
        }

        //should change probably maybe even delete so wont be working on it
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
