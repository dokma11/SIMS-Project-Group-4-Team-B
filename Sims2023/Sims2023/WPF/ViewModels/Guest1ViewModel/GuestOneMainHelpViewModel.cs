using Sims2023.WPF.Views;
using Sims2023.WPF.Views.Guest1Views;
using Sims2023.WPF.Views.Guest1Views.Guest1HelpViews;
using Sims2023.WPF.Views.Guest2Views;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public class GuestOneMainHelpViewModel
    {
        GuestOneMainHelpView GuestOneMainHelpView;

        Frame HelpFrame = new();

        String CurrentPage;

        public GuestOneMainHelpViewModel(GuestOneMainHelpView guestOneMainHelpView, String currentPage)
        {
            GuestOneMainHelpView = guestOneMainHelpView;
            HelpFrame = GuestOneMainHelpView.HelpFrame;
            CurrentPage = currentPage;
            SetCurrentPageCommands();
            SetHelpForThisFrame();
        }

        internal void ShowMore()
        {
            if (RightPage())
            {
                Page currentPage = new();
                currentPage = HelpFrame.Content as Page;
                if (currentPage.Title != "DateTimeHelpView")
                {
                    GuestOneMainHelpView.AddedCommandsGrid.Visibility = Visibility.Visible;
                    HelpFrame.Navigate(new DateTimeHelpView());
                }
                else
                {
                    SetCurrentPageCommands();
                    SetHelpForThisFrame();
                }
            }
        }

        private bool RightPage()
        {
            switch (CurrentPage)
            {
                case "AccommodationListView":
                case "AccommodationReservationDateView":
                case "AccommodationRenovationRecommodationView":
                case "WheneverWhereverMainView":
                case "SearchForumView":
                case "ReportView":
                    return true;
                default:
                    return false;
            }
        }

        private void SetCurrentPageCommands()
        {
            GuestOneMainHelpView.AddedCommandsGrid.Visibility = Visibility.Visible;
            GuestOneMainHelpView.SpaceFillerGrid.Visibility = Visibility.Collapsed;

            if (RightPage())
            {
                GuestOneMainHelpView.MoreButton.Visibility = Visibility.Visible;
                GuestOneMainHelpView.MoreLabel.Visibility = Visibility.Visible;
            }
            else
            {
                GuestOneMainHelpView.MoreButton.Visibility = Visibility.Collapsed;
                GuestOneMainHelpView.MoreLabel.Visibility = Visibility.Collapsed;
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
            if (CurrentPage == "WheneverWhereverMainView")
            {
                HelpFrame.Navigate(new WheneverWhereverMainHelpView());
            }
            if (CurrentPage == "GuestOneReviewsView")
            {
                HelpFrame.Navigate(new GuestOneReviewsHelpView());
            }            
            if (CurrentPage == "WheneverWhereverOptionsView")
            {
                HelpFrame.Navigate(new WheneverWhereverOptionsHelpView());
            }
            if (CurrentPage == "WhereverWheneverDetailedView")
            {
                HelpFrame.Navigate(new WheneverWhereverDetailedHelpView());
            }
            if (CurrentPage == "SearchForumView")
            {
                HelpFrame.Navigate(new SearchForumHelpView());
            }
            if (CurrentPage == "OpenedForumView")
            {
                HelpFrame.Navigate(new OpenedForumHelpView());
            }
            if (CurrentPage == "MakeNewForumView")
            {
                HelpFrame.Navigate(new MakeNewForumHelpView());
            }
            if (CurrentPage == "ForumCommentView")
            {
                HelpFrame.Navigate(new ForumCommentHelpView());
            }
            if (CurrentPage == "ReportView")
            {
                HelpFrame.Navigate(new ReportHelpView());
            }
            if (CurrentPage == "GuestOneStartView")
            {
                HelpFrame.Navigate(new GuestOneStartHelpView());
            }
            return;
        }
    }
}
