using Sims2023.Controller;
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
        
        private AccommodationReservationController reservationCtrl;
        public ObservableCollection<AccommodationReservation> reservatons { get; set; }
       

        public AccommodationReservation selectedGuest { get; set; }
        public List<AccommodationReservation> reservatons2 { get; set; }
        public List<Accommodation> accommodations { get; set; }
        public List<AccommodationReservation> reservationsList  { get; set; }

        private AccommodationController accommodationCtrl;

        private GuestGradeController gradeCtrl;


        public AllGuestsView(List<Guest> guests,AccommodationReservationController acml, List<AccommodationReservation> reserv)
        {
            InitializeComponent();
            DataContext= this;
          
            accommodationCtrl = new AccommodationController();
            
            accommodations = new List<Accommodation>(accommodationCtrl.GetAllAccommodations());
            
            reservationCtrl = acml;
            reservatons2 = reserv;
          
            gradeCtrl = new GuestGradeController();
            reservatons = new ObservableCollection<AccommodationReservation>(reservationCtrl.GetGradableGuests(guests, accommodations, reservatons2, gradeCtrl.GetAllGrades()));

       

        }




        private void Grade_Click(object sender, EventArgs e)
        {
            
            if (selectedGuest != null)
            {
                var gradeView = new Guest1GradeView(selectedGuest, reservatons);
                gradeView.Closed += GradeView_Closed;
                gradeView.Show();
            }

        
        }
        private void GradeView_Closed(object sender, EventArgs e)
        {
            var gradeView = (Guest1GradeView)sender;

            if (gradeView.gradeEntered)
            {
                reservatons.Remove(selectedGuest);
            }
        }



    }
}
