using Sims2023.Controller;
using Sims2023.Model;
using System;
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

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for AllGuestsView.xaml
    /// </summary>
    public partial class AllGuestsView : Window
    {
        
        private AccommodationReservationController reservationCtrl;
        public ObservableCollection<AccommodationReservation> reservatons { get; set; }
        public List<Guest> ListOfGuests { get; set; }

        public AccommodationReservation selectedGuest { get; set; }
        public List<AccommodationReservation> reservatons2 { get; set; }
        public List<Accommodation> accommodations { get; set; }

        private AccommodationController accommodationCtrl;


        public AllGuestsView(List<Guest> guests,AccommodationReservationController acml, List<AccommodationReservation> reserv)
        {
            InitializeComponent();
            DataContext= this;
            accommodationCtrl = new AccommodationController();
            accommodations = new List<Accommodation>(accommodationCtrl.GetAllAccommodations());
          
            reservatons2 = reserv;
            ListOfGuests = guests;
            reservatons = new ObservableCollection<AccommodationReservation>(acml.getGradableGuests(guests, accommodations, reservatons2));
            
        }

        private void Grade_Click(object sender, EventArgs e)
        {
            if (selectedGuest != null)
            {
                var grade = new Guest1GradeView(selectedGuest);
                grade.Show();
            }


        }




    }
}
