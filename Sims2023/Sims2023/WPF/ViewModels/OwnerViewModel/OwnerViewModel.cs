using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.View;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
   public class OwnerViewModel
    {
        public AccommodationService _accommodationService;
        
        public AccommodationReservationService _accommodationReservationService;
       
        public GuestGradeService _gradeService;

        public AccommodationCancellationService _accommodationCancellationService;
        public List<AccommodationReservation> Reservations { get; set; }
        public ObservableCollection<AccommodationCancellation> AccommodationCancellations { get; set; }
        public List<AccommodationReservation> GradableGuests { get; set; }

        public User User { get; set; }
        // constructor for the class goes here

        public OwnerViewModel(User user)
        {
            User = user;
            _accommodationService = new AccommodationService();
            _accommodationReservationService = new AccommodationReservationService();
            _accommodationCancellationService  = new AccommodationCancellationService();
            _gradeService = new GuestGradeService();
            Reservations = new List<AccommodationReservation>(_accommodationReservationService.GetAllReservations());
            AccommodationCancellations = new ObservableCollection<AccommodationCancellation>(_accommodationCancellationService.GetAllAccommodationCancellations());
        }

        public List<AccommodationReservation> GetGradableGuests()
        {
            return _accommodationReservationService.GetGradableGuests(User, Reservations, _gradeService.GetAllGrades());
        }



        public void checkForNotifications()
        {
            foreach (AccommodationCancellation accommodationCancellation in AccommodationCancellations)
            {
                if (accommodationCancellation.Notified == false && accommodationCancellation.Accommodation.Owner.Id == User.Id)
                {
                    MessageBox.Show($" Korisnik {accommodationCancellation.Guest.Name} je otkazao rezervaciju od {accommodationCancellation.StartDate.ToString("yyyy-MM-dd")} do {accommodationCancellation.EndDate.ToString("yyyy-MM-dd")}. Vas smestaj {accommodationCancellation.Accommodation.Name} je ponovo oslobodjen!");
                    accommodationCancellation.Notified = true;
                    _accommodationCancellationService.Update(accommodationCancellation);
                }
            }
        }

        public void Window_Loaded()
        {
            checkForNotifications();
            string fileName = "../../../Resources/Data/lastshown.txt";

            try
            {
                string lastShownText = File.ReadAllText(fileName);
                DateTime lastShownDate = DateTime.Parse(lastShownText);

                if (lastShownDate < DateTime.Today)
                {
                    if (_accommodationReservationService.GetGradableGuests(User, Reservations, _gradeService.GetAllGrades()).Count != 0)
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

        public void Grade_Click()
        {
            var guestss = new AllGuestsView(User, Reservations);
            guestss.Show();
        }

        public void AddAccommodation_Click()
        {
            var addAccommodation = new AccommodationRegistrationView(_accommodationService, User);
            addAccommodation.Show();
        }

        public void Grades_Given_From_Guests()
        {
            var GuestsGrades = new GradesFromGuestsView(User);
            GuestsGrades.Show();
        }

        public void Reservations_Click()
        {
            var Reschedulings = new GuestsReservationReschedulingView(User);
            Reschedulings.Show();
        }

    }
}
