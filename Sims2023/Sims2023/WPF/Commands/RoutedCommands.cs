using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Sims2023.WPF.Commands
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand OpenMenu = new RoutedUICommand(
            "Open Menu",
            "OpenMenu",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.M, ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand OpenHelp = new RoutedUICommand(
            "Open Help",
            "OpenHelp",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.H, ModifierKeys.Alt)
            }
            );
        public static readonly RoutedUICommand LogOut = new RoutedUICommand(
            "Log Out",
            "LogOut",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.L, ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand GuestOneMainView = new RoutedUICommand(
            "Guest One Main View",
            "GuestOneMainView",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.A, ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand AccommodationListView = new RoutedUICommand(
            "Accommodation List View",
            "AccommodationListView",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.B, ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand AccommodationReservationReschedulingView = new RoutedUICommand(
            "Accommodation Reservation Rescheduling View",
            "AccommodationReservationReschedulingView",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.C, ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand AccommodationReservationCancellation = new RoutedUICommand(
            "Accommodation Reservation Cancellation",
            "AccommodationReservationCancellation",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.D, ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand AllGuestOneReservationsView = new RoutedUICommand(
            "All Guest One Reservations View",
            "AllGuestOneReservationsView",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.E, ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand GuestOneAllGradesView = new RoutedUICommand(
            "Guest One All Grades View",
            "GuestOneAllGradesView",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F, ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand WheneverWhereverView = new RoutedUICommand(
            "Whenever Wherever View",
            "WheneverWhereverView",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.G, ModifierKeys.Control)
            }
            ); 
        public static readonly RoutedUICommand ForumView = new RoutedUICommand(
            "Forum View",
            "Forum View",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.H, ModifierKeys.Control)
            }
            );
    }
}
