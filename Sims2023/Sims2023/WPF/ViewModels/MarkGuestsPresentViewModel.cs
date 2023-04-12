using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sims2023.WPF.ViewModels
{
    public partial class MarkGuestsPresentViewModel
    {
        public KeyPoint KeyPoint { get; set; }
        public ObservableCollection<User> GuestsToDisplay { get; set; }

        private UserService _userService;
        private TourReservationService _tourReservationController;
        public List<User> MarkedGuests { get; set; }
        public MarkGuestsPresentViewModel(KeyPoint keyPoint, TourReservationService tourReservationController, UserService userService, List<User> markedGuests)
        {
            InitializeComponent();
            DataContext = this;

            KeyPoint = keyPoint;

            _tourReservationController = tourReservationController;
            _userService = userService;

            MarkedGuests = markedGuests;

            GuestsToDisplay = new ObservableCollection<User>();

            InitializeGuestsToDisplay();
        }

        private void InitializeGuestsToDisplay()
        {
            foreach (var tourReservation in _tourReservationController.GetAll())
            {
                if (tourReservation.Tour.Id == KeyPoint.Tour.Id)
                {
                    User Guest = _userService.GetById(tourReservation.Tour.Id);
                    if (!CheckIfGuestMarked(tourReservation, Guest))
                    {
                        GuestsToDisplay.Add(Guest);
                    }
                }
            }
        }

        private bool CheckIfGuestMarked(TourReservation tourReservation, User guest)
        {
            if (!KeyPoint.ShowedGuestsIds.Contains(tourReservation.Tour.Id))
            {
                foreach (var markedGuest in MarkedGuests)
                {
                    if (markedGuest.Id == guest.Id)
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (guestDataGrid.SelectedItems.Count > 0)
            {
                //initialize the start of the string if necessary
                if (KeyPoint.ShowedGuestsIds == null)
                {
                    KeyPoint.ShowedGuestsIdsString = "";
                }
                foreach (User user in guestDataGrid.SelectedItems)
                {
                    KeyPoint.ShowedGuestsIds.Add(user.Id);
                    KeyPoint.ShowedGuestsIdsString += user.Id.ToString() + " ";
                    MarkedGuests.Add(user);
                    ShouldConfirmParticipation(user);
                }
            }
            Close();
        }

        private void ShouldConfirmParticipation(User user)
        {
            foreach (var tourReservation in _tourReservationController.GetAll())
            {
                if (tourReservation.User.Id == user.Id && tourReservation.Tour.Id == KeyPoint.Tour.Id)
                {
                    tourReservation.ShouldConfirmParticipation = true;
                    _tourReservationController.Save();
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
