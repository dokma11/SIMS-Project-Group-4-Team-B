﻿using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using Sims2023.WPF.Views.Guest1Views;
using Sims2023.WPF.Views.Guest1Views.Guest1Wizard;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Transactions;
using System.Windows;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainViewModel : Window
    {
        public User User { get; set; }

        private AccommodationReservationReschedulingService _accommodationReservationReschedulingService;

        private AccommodationReservationService _accommodationReservationService;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        private UserService _userService;

        Guest1MainView Guest1MainView;
        public Guest1MainViewModel(Guest1MainView guest1MainView, User guest1)
        {
            Guest1MainView = guest1MainView;
            User = guest1;
            Guest1MainView.userName_label.Content = User.Name;
            _accommodationReservationReschedulingService = new AccommodationReservationReschedulingService();
            _accommodationReservationService = new AccommodationReservationService();
            _userService= new UserService();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingService.GetAllReservationReschedulings());
            CheckIfFirstLogIn();
        }

        private void CheckIfFirstLogIn()
        {
            if(User.FirstLogIn)
            {
                //User.FirstLogIn = false;
                //_userService.Update(User);
                _userService.AppHasBeenStarted();
                var Wizard = new WizardMainView();
                Wizard.ShowDialog();
            }
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkForNotifications(User);
            CheckGuestStatus();
        }

        private void CheckGuestStatus()
        {
            if(User.SuperGuest1)
            {
                if(CheckTheSuperGuestDate())
                {
                    Guest1MainView.userStatus_label.Content = "Super gost";
                    return;
                }
                Guest1MainView.userStatus_label.Content = "Regularan gost";
            }
            else
            {
                if(CheckIfSuperGuest(User))
                {
                    Guest1MainView.userStatus_label.Content = "Super gost";
                    return;
                }
                Guest1MainView.userStatus_label.Content = "Regularan gost";
            }
            
        }

        private bool CheckTheSuperGuestDate()
        {

            TimeSpan diff = DateTime.Today - User.DateOfBecomingSuperGuest;
            if(diff.TotalDays>=365)
            {
                return CheckIfSuperGuest(User);
            }
            return true;
        }


        private bool CheckIfSuperGuest(User user)
        {
            List<AccommodationReservation> AllReservations = _accommodationReservationService.FindAllGuestsReservations(User);
            if(AllReservations.Count > 10)
            {
                _userService.MarkGuestAsSuper(user);
                return true;
            }
            _userService.MarkGuestAsRegular(user);
            return false;
        }

        public void checkForNotifications(User guest1)
        {
            foreach (AccommodationReservationRescheduling accommodationReservationRescheduling in AccommodationReservationReschedulings)
            {
                if (Notify(accommodationReservationRescheduling, guest1))
                {
                    MessageBox.Show($" Vlasnik smestaja {accommodationReservationRescheduling.AccommodationReservation.Accommodation.Name} je promienio status vaseg zahteva za pomeranje rezervacije. Vas zahtev je {accommodationReservationRescheduling.Status}!");
                    accommodationReservationRescheduling.Notified = true;
                    _accommodationReservationReschedulingService.Update(accommodationReservationRescheduling);
                }
            }
        }

        public bool Notify(AccommodationReservationRescheduling accommodationReservationRescheduling, User guest1)
        {
            if (accommodationReservationRescheduling.Notified == false && accommodationReservationRescheduling.AccommodationReservation.Guest.Id == guest1.Id && accommodationReservationRescheduling.Status.ToString() != "Pending")
            {
                return true;
            }
            return false;
        }
        public void HideMainMenu()
        {
            Guest1MainView.MainMenu.Visibility = Visibility.Collapsed;
            Guest1MainView.Overlay.Visibility = Visibility.Collapsed;
        }
        public void ToggleMainMenu()
        {
            if (Guest1MainView.MainMenu.Visibility == Visibility.Visible)
            {
                HideMainMenu();
            }
            else
            {
                ShowMainMenu();
            }
        }
        public void ShowMainMenu()
        {
            Guest1MainView.MainMenu.Visibility = Visibility.Visible;
            Guest1MainView.Overlay.Visibility = Visibility.Visible;
        }

    }
}
