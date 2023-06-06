using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    internal class WhereverWheneverDetailedViewModel
    {
        private Accommodation SelectedAccommodation;
        private int DaysNumber;
        private int GuestsNumber;
        private User User;
        private DateTime StartDateSelected;
        private DateTime EndDateSelected;
        private Frame MainFrame;
        WhereverWheneverDetailedView WhereverWheneverDetailedView;

        public WhereverWheneverDetailedViewModel(Accommodation selectedAccommodation, int daysNumber, int guestsNumber, User user, DateTime startDateSelected, DateTime endDateSelected, Frame mainFrame, WhereverWheneverDetailedView whereverWheneverDetailedView)
        {
            SelectedAccommodation = selectedAccommodation;
            DaysNumber = daysNumber;
            GuestsNumber = guestsNumber;
            User = user;
            StartDateSelected = startDateSelected;
            EndDateSelected = endDateSelected;
            MainFrame = mainFrame;
            WhereverWheneverDetailedView = whereverWheneverDetailedView;
            FillTextBoxes(SelectedAccommodation);
        }

        public void FillTextBoxes(Accommodation selectedAccommodation)
        {
            WhereverWheneverDetailedView.accommodatioNameLabel.Content = "Naziv smeštaja: " + selectedAccommodation.Name;
            WhereverWheneverDetailedView.accommodatioCityLabel.Content = "Grad: " + selectedAccommodation.Location.City;
            WhereverWheneverDetailedView.accommodatioCountryLabel.Content = "Država: " + selectedAccommodation.Location.Country;
            WhereverWheneverDetailedView.accommodatioMaxGuestsLabel.Content = "Maksimalan broj gostiju: " + selectedAccommodation.MaxGuests;
            WhereverWheneverDetailedView.accommodatioMinDaysLabel.Content = "Minimalan broj dana: " + selectedAccommodation.MinDays;
            WhereverWheneverDetailedView.accommodatioOwnerLabel.Content = "Ime vlasnika: " + selectedAccommodation.Owner.Name + " " + selectedAccommodation.Owner.Surname;
            WhereverWheneverDetailedView.PicturesListView.ItemsSource = selectedAccommodation.Imageurls;
        }

        public void GoBack()
        {
            NavigationService navigationService = NavigationService.GetNavigationService(WhereverWheneverDetailedView);

            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }

        public void MakeReservation()
        {
            GoBack();
        }
    }
}
