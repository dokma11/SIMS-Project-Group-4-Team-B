using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for OpenAccommodationRegistrationView.xaml
    /// </summary>
    public partial class OwnerView : Window
    {
        private string outputText;
        private AccommodationService _accommodationController;
        private AccomodationLocationController _accommodationLocationController;
        private AccommodationReservationService _accommodationReservationController;
        private GuestGradeService _gradeController;

        private AccommodationCancellationController _accommodationCancellationController;
        public ObservableCollection<AccommodationCancellation> AccommodationCancellations { get; set; }

        public List<AccommodationReservation> Reservatons { get; set; }
        public List<AccommodationReservation> GradableGuests { get; set; }

        public User User { get; set; }
        public OwnerView(User owner)
        {

            InitializeComponent();
            DataContext = this;

            User = owner;

            _accommodationController = new AccommodationService();
            _accommodationLocationController = new AccomodationLocationController();

            _accommodationCancellationController = new AccommodationCancellationController();
            AccommodationCancellations = new ObservableCollection<AccommodationCancellation>(_accommodationCancellationController.GetAllAccommodationCancellations());

            _accommodationReservationController = new AccommodationReservationService();
            _gradeController = new GuestGradeService();

            Reservatons = new List<AccommodationReservation>(_accommodationReservationController.GetAllReservations());

            GradableGuests = new List<AccommodationReservation>(_accommodationReservationController.GetGradableGuests(User,Reservatons, _gradeController.GetAllGrades()));


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkForNotifications();
            string fileName = "../../../Resources/Data/lastshown.txt";

            try
            {
                string lastShownText = File.ReadAllText(fileName);
                DateTime lastShownDate = DateTime.Parse(lastShownText);

                if (lastShownDate < DateTime.Today)
                {
                    if (GradableGuests.Count != 0)
                    {
                  //      MessageBox.Show(_accommodationReservationController.GetAllUngradedNames(GradableGuests));

                        // Update the last shown date to today's date
                        File.WriteAllText(fileName, DateTime.Today.ToString());
                    }

                  
                }
            }
            catch (FileNotFoundException)
            {
             
                File.WriteAllText(fileName, DateTime.Today.ToString());
            }
        }

        private void checkForNotifications()
        {
            foreach (AccommodationCancellation accommodationCancellation in AccommodationCancellations)
            {
                if (accommodationCancellation.Notified == false && accommodationCancellation.Accommodation.Owner.Id == User.Id)
                {
                    MessageBox.Show($" Korisnik {accommodationCancellation.Guest.Name} je otkazao rezervaciju od {accommodationCancellation.StartDate.ToString("yyyy-MM-dd")} do {accommodationCancellation.EndDate.ToString("yyyy-MM-dd")}. Vas smestaj {accommodationCancellation.Accommodation.Name} je ponovo oslobodjen!");
                    accommodationCancellation.Notified = true;
                    _accommodationCancellationController.Update(accommodationCancellation);
                }
            }
        }

    private void Grade_Click(object sender, RoutedEventArgs e)
        {
            var guestss = new AllGuestsView(User,_accommodationReservationController, Reservatons);
            guestss.Show();
        }

        private void AddAccommodation_Click(object sender, RoutedEventArgs e)
        {
            var addAccommodation = new AccommodationRegistrationView(_accommodationController, _accommodationLocationController,User);
            addAccommodation.Show();
        }

        private void Grades_Given_From_Guests(object sender, RoutedEventArgs e)
        {
            var GuestsGrades = new GradesFromGuestsView(User);
            GuestsGrades.Show();
        }

        private void Reservations_Click(object sender, RoutedEventArgs e)
        {
            var Reschedulings = new GuestsReservationReschedulingView(User);
            Reschedulings.Show();
        }
    }
}
