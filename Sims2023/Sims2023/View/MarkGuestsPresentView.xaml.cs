using Sims2023.Controller;
using Sims2023.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for MarkGuestsPresentView.xaml
    /// </summary>
    public partial class MarkGuestsPresentView : Window
    {
        public KeyPoint KeyPoint { get; set; }
        public ObservableCollection<User> GuestsToDisplay { get; set; }

        private UserController _userController;
        private TourReservationController _tourReservationController;
        public List<User> MarkedGuests { get; set; }
        public MarkGuestsPresentView(KeyPoint keyPoint, TourReservationController tourReservationController, UserController userController, List<User> markedGuests)
        {
            InitializeComponent();
            DataContext = this;

            KeyPoint = keyPoint;

            _tourReservationController = tourReservationController;
            _userController = userController;

            MarkedGuests = markedGuests;

            GuestsToDisplay = new ObservableCollection<User>();

            InitializeGuestsToDisplay();
        }

        private void InitializeGuestsToDisplay()
        {
            foreach (var tourReservation in _tourReservationController.GetAllReservations())
            {
                if (tourReservation.TourId == KeyPoint.Tour.Id)
                {
                    User Guest = _userController.GetById(tourReservation.UserId);
                    if (!CheckIfGuestMarked(tourReservation, Guest))
                    {
                        GuestsToDisplay.Add(Guest);
                    }
                }
            }
        }

        private bool CheckIfGuestMarked(TourReservation tourReservation, User guest)
        {
            if (!KeyPoint.ShowedGuestsIds.Contains(tourReservation.UserId))
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
            foreach (var tourReservation in _tourReservationController.GetAllReservations())
            {
                if (tourReservation.UserId == user.Id && tourReservation.TourId == KeyPoint.Tour.Id)
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
