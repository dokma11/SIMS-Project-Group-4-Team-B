using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.View;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToastNotifications.Position;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for NotificationView.xaml
    /// </summary>
    public partial class NotificationView : Page
    {
        public User User { get; set; }

        private AccommodationReservationService _reservationService; 
        public List<AccommodationReservation> ReservationsList { get; set; }  

        private GuestGradeService _gradeService;

        public List<AccommodationReservation> Reservationss { get; set; }
        public ObservableCollection<AccommodationReservation> Reservatons { get; set; }

        public LocationService _locationService;
        public AccommodationService _accommodationService;
        public ForumService _forumService;
        public List<Location> locations { get; set; }

        public List<Forum> forums { get; set; }
        public ObservableCollection<Forum> Forums { get; set; }


        public NotificationView(User user)
        {
            InitializeComponent();
            _reservationService = new AccommodationReservationService();
            _gradeService = new GuestGradeService();
            User = user;
            Reservationss = new List<AccommodationReservation>(_reservationService.GetAllReservations());
            Reservatons = new ObservableCollection<AccommodationReservation>(GetGradableGuests());


            _locationService = new LocationService();
            _accommodationService = new AccommodationService();
            _forumService = new ForumService();

            locations = _accommodationService.GetOwnerLocations(_accommodationService.GetOwnerAccommodations(_accommodationService.GetAllAccommodations(), User));

            forums = _forumService.GetForumsForParticularOwner(locations);
            forums.RemoveAll(forum => forum.OwnerOpened == true);
            Forums = new ObservableCollection<Forum>(forums);
            DataContext = this;
        }

        public List<AccommodationReservation> GetGradableGuests()
        {
            return _reservationService.GetGradableGuests(User, Reservationss, _gradeService.GetAllGrades());
        }


     /*   public void checkForNotifications()
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
        }  */

         private void Grade_Click(object sender, EventArgs e)
        {
            var guestss = new AllGuestsView(User, Reservationss);
            FrameManager.Instance.MainFrame.Navigate(guestss);

        }

        private void OpenForum_Click(object sender, EventArgs e)
        {
            ListOfForumsView list = new ListOfForumsView(User);
            FrameManager.Instance.MainFrame.Navigate(list);
        }


    }
     }  
