using Sims2023.WPF.Views.Guest1Views.Guest1HelpViews;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public class GuestOneMainHelpViewModel
    {
        GuestOneMainHelpView GuestOneMainHelpView;

        Frame HelpFrame;

        String CurrentPage;

        public GuestOneMainHelpViewModel(GuestOneMainHelpView guestOneMainHelpView, String currentPage, Frame helpFrame)
        {
            GuestOneMainHelpView = guestOneMainHelpView;
            HelpFrame = helpFrame;
            CurrentPage = currentPage;
            SetCurrentPageCommands();
            SetHelpForThisFrame();
        }

        private void SetCurrentPageCommands()
        {
            if (CurrentPage != "GuestOneStartView")
            {
                GuestOneMainHelpView.AddedCommandsGrid.Visibility = Visibility.Visible;
                GuestOneMainHelpView.SpaceFillerGrid.Visibility = Visibility.Collapsed;

            }
            else
            {
                GuestOneMainHelpView.AddedCommandsGrid.Visibility = Visibility.Collapsed;
                GuestOneMainHelpView.SpaceFillerGrid.Visibility = Visibility.Visible;
            }
        }

        private void SetHelpForThisFrame()
        {
            if (CurrentPage == "AccommodationAndOwnerGradingView")
            {
                HelpFrame.Navigate(new AccommodationAndOwnerGradingViewHelp());
            }
            if (CurrentPage == "AccommodationDetailedView")
            {
                HelpFrame.Navigate(new AccommodationDetailedViewHelp());
            }
            if (CurrentPage == "AccommodationListView")
            {
                HelpFrame.Navigate(new AccommodationListViewHelp());
            }
            if (CurrentPage == "AccommodationReservationCancellationView")
            {
                HelpFrame.Navigate(new AccommodationReservationCancellationViewHelp());
            }
            if (CurrentPage == "AccommodationReservationConfirmationView")
            {
                HelpFrame.Navigate(new AccommodationReservationConfirmationHelpView());
            }
            if (CurrentPage == "AccommodationReservationDateView")
            {
                HelpFrame.Navigate(new AccommodationReservationDateHelpView());
            }
            if (CurrentPage == "AccommodationReservationReschedulingView")
            {
                HelpFrame.Navigate(new AccommodationReservationReschedulingHelpView());
            }
            if (CurrentPage == "AllGuestOneReservationsView")
            {
                HelpFrame.Navigate(new AllGuestOneReservationsHelpView());
            }
            if (CurrentPage == "NewAccommodationReservationReschedulingRequestView")
            {
                HelpFrame.Navigate(new NewAccommodationReservationReschedulingRequestHelpView());
            }
            if (CurrentPage == "AccommodationRenovationRecommodationView")
            {
                HelpFrame.Navigate(new AccommodationRenovationRecommodationHelpView());
            }
        }
    }
}
