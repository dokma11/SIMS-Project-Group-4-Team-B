﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sims2023.Controller;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.Domain.Models;
using System.IO;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for OpenAccommodationRegistrationView.xaml
    /// </summary>
    public partial class OwnerView : Window
    {
        private string outputText;
        private AccommodationController _accommodationController;
        private AccomodationLocationController _accommodationLocationController;
        private AccommodationReservationController _accommodationReservationController;
        private GuestGradeController _gradeController;

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

            _accommodationController = new AccommodationController();
            _accommodationLocationController = new AccomodationLocationController();

            _accommodationCancellationController = new AccommodationCancellationController();
            AccommodationCancellations = new ObservableCollection<AccommodationCancellation>(_accommodationCancellationController.GetAllAccommodationCancellations());

            _accommodationReservationController = new AccommodationReservationController();
            _gradeController = new GuestGradeController();

            Reservatons = new List<AccommodationReservation>(_accommodationReservationController.GetAllReservations());

            GradableGuests = new List<AccommodationReservation>(_accommodationReservationController.GetGradableGuests(Reservatons, _gradeController.GetAllGrades()));


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
                        MessageBox.Show(_accommodationReservationController.GetAllUngradedNames(GradableGuests));

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
            var guestss = new AllGuestsView(_accommodationReservationController, Reservatons);
            guestss.Show();
        }

        private void AddAccommodation_Click(object sender, RoutedEventArgs e)
        {
            var addAccommodation = new AccommodationRegistrationView(_accommodationController, _accommodationLocationController,User);
            addAccommodation.Show();
        }

        private void Grades_Given_From_Guests(object sender, RoutedEventArgs e)
        {
            var GuestsGrades = new GradesFromGuestsView();
            GuestsGrades.Show();
        }
    }
}
