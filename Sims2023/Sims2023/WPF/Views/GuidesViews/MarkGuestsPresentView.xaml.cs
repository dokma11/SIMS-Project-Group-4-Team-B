using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.WPF.Views.GuidesViews
{
    public partial class MarkGuestsPresentView: Window
    {
        public KeyPoint KeyPoint { get; set; }
        public ObservableCollection<User> GuestsToDisplay { get; set; }

        private UserService _userService;
        private TourReservationService _tourReservationService;
        public MarkGuestsPresentViewModel MarkGuestsPresentViewModel;
        public List<User> MarkedGuests { get; set; }
        public MarkGuestsPresentView(KeyPoint keyPoint, TourReservationService tourReservationService, UserService userService, List<User> markedGuests)
        {
            InitializeComponent();
            DataContext = this;

            KeyPoint = keyPoint;
            _tourReservationService = tourReservationService;
            _userService = userService;
            MarkedGuests = markedGuests;

            MarkGuestsPresentViewModel = new(KeyPoint, _tourReservationService, _userService, MarkedGuests);
            GuestsToDisplay = new ObservableCollection<User>(MarkGuestsPresentViewModel.GuestsToDisplay);
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
                foreach (User guest in guestDataGrid.SelectedItems)
                {
                    MarkGuestsPresentViewModel.AddMarkedGuests(guest);
                }
            }
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
