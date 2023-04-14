using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class MarkGuestsPresentView : Window
    {
        public KeyPoint KeyPoint { get; set; }
        private UserService _userService;
        private TourReservationService _tourReservationService;
        private KeyPointService _keyPointService;
        public MarkGuestsPresentViewModel MarkGuestsPresentViewModel;
        public List<User> MarkedGuests { get; set; }

        public MarkGuestsPresentView(KeyPoint selectedKeyPoint, TourReservationService tourReservationService, UserService userService, KeyPointService keyPointService, List<User> markedGuests)
        {
            InitializeComponent();

            _tourReservationService = tourReservationService;
            _userService = userService;
            _keyPointService = keyPointService;

            MarkedGuests = markedGuests;
            KeyPoint = selectedKeyPoint;

            MarkGuestsPresentViewModel = new(KeyPoint, _tourReservationService, _userService, _keyPointService, MarkedGuests);
            DataContext = MarkGuestsPresentViewModel;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (guestDataGrid.SelectedItems.Count > 0)
            {
                List<User> users = guestDataGrid.SelectedItems.Cast<User>().ToList();
                MarkGuestsPresentViewModel.AddMarkedGuests(users);
            }
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
