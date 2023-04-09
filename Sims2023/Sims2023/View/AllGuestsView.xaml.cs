using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for AllGuestsView.xaml
    /// </summary>
    public partial class AllGuestsView : Window
    {
        
        private AccommodationReservationController _reservationController;
        public ObservableCollection<AccommodationReservation> Reservatons { get; set; }
       

        public AccommodationReservation SelectedGuest { get; set; }
        public List<AccommodationReservation> Reservatons2 { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public List<AccommodationReservation> ReservationsList  { get; set; }

        private AccommodationController _accommodationController;

        private GuestGradeController _gradeController;

        public User user { get; set; }

        public AllGuestsView(User use,AccommodationReservationController acml, List<AccommodationReservation> reserv)
        {
            InitializeComponent();
            DataContext= this;
            user = use;
            _accommodationController = new AccommodationController();
            
            Accommodations = new List<Accommodation>(_accommodationController.GetAllAccommodations());
            
            _reservationController = acml;
            Reservatons2 = reserv;
          
            _gradeController = new GuestGradeController();
            Reservatons = new ObservableCollection<AccommodationReservation>(_reservationController.GetGradableGuests(user,reserv, _gradeController.GetAllGrades()));
        }
        private void Grade_Click(object sender, EventArgs e)
        {
            
            if (SelectedGuest != null)
            {
                var gradeView = new Guest1GradeView(SelectedGuest, Reservatons);
                gradeView.Closed += GradeView_Closed;
                gradeView.Show();
            }

        
        }
        private void GradeView_Closed(object sender, EventArgs e)
        {
            var gradeView = (Guest1GradeView)sender;

            if (gradeView.GradeEntered)
            {
                Reservatons.Remove(SelectedGuest);
            }
        }
   }
}
