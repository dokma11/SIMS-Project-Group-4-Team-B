using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.View;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using static System.Net.WebRequestMethods;

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


        public LocationService _locationService;
        public ForumService _forumService;
        public List<Location> locations { get; set; }

        public List<Forum> forums { get; set; }
        public ObservableCollection<Forum> Forums { get; set; }


        public User User { get; set; }
        // constructor for the class goes here

        public OwnerViewModel(User user)
        {
            User = user;
            _accommodationService = new AccommodationService();
            _accommodationReservationService = new AccommodationReservationService();
            _accommodationCancellationService = new AccommodationCancellationService();
            _gradeService = new GuestGradeService();
            Reservations = new List<AccommodationReservation>(_accommodationReservationService.GetAllReservations());
            AccommodationCancellations = new ObservableCollection<AccommodationCancellation>(_accommodationCancellationService.GetAllAccommodationCancellations());

            _locationService = new LocationService();
            _forumService = new ForumService();
            locations = _accommodationService.GetOwnerLocations(_accommodationService.GetOwnerAccommodations(_accommodationService.GetAllAccommodations(), User));
            forums = _forumService.GetForumsForParticularOwner(locations);
            forums.RemoveAll(forum => forum.OwnerOpened == true);
            //ShowMessage();
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
            //   checkForNotifications();
            string fileName = "../../../Resources/Data/lastshown.txt";
            string lastShownText = System.IO.File.ReadAllText(fileName);
            DateTime lastShownDate = DateTime.Parse(lastShownText);

            if (lastShownDate < DateTime.Today)
            {
                if (forums.Count() != 0)
                {
                    ToastNotificationService.ShowInformation("Imate novootvorene forume");
                    System.IO.File.WriteAllText(fileName, DateTime.Today.ToString());
                }
                else if (_accommodationReservationService.GetGradableGuests(User, Reservations, _gradeService.GetAllGrades()).Count != 0)
                {
                    ToastNotificationService.ShowInformation("Imate neocijenjene goste");


                    // Update the last shown date to today's date
                    System.IO.File.WriteAllText(fileName, DateTime.Today.ToString());
                }
            }
        }

        public string ungradedGuestsNameAndSurrname(List<AccommodationReservation> ungradedGuests)
        {
            string string1 = "Imate neocijenjene goste: \n";

            foreach (var guest in ungradedGuests)
            {
                string1 += guest.Guest.Name + guest.Guest.Surname + "\n";
            }
            return string1;
        }

        public AllGuestsView Grade_Click()
        {
            
            var guestss = new AllGuestsView(User, Reservations);
            return guestss;
       }

        public void Grades_Given_From_Guests()
        {
            var GuestsGrades = new GradesFromGuestsView(User);
            FrameManager.Instance.MainFrame.Navigate(GuestsGrades);
        }

        public void Reservations_Click()
        {
            var Reschedulings = new GuestsReservationReschedulingView(User);
            FrameManager.Instance.MainFrame.Navigate(Reschedulings);
        }

        public void Renovations_Click()
        {
            var renovations = new AllAccommodationsView(User);
            FrameManager.Instance.MainFrame.Navigate(renovations);
        }

        public void Statistics_Click()
        {
            var accommodations = new AllAccommodationStatisticsView(User);
            FrameManager.Instance.MainFrame.Navigate(accommodations);
        }

    }
}
