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
        public ObservableCollection<Guest> ListOfGuests { get; set; }
        public ObservableCollection<Accommodation> accommodations { get; set; }

        private AccommodationController accommodationCtrl;
        public AllGuestsView(ObservableCollection<Guest> guests,AccommodationReservationController acml, ObservableCollection<AccommodationReservation> reserv)
        {
            InitializeComponent();
            DataContext= this;
            accommodationCtrl = new AccommodationController();
            accommodations = new ObservableCollection<Accommodation>(accommodationCtrl.GetAllAccommodations());
            reservationCtrl = acml;
            reservatons = reserv;
            ListOfGuests = guests;
            AddNameSurrnameToReservation(ListOfGuests, reservatons);
            AddReservationName(reservatons, accommodations);
        }

        private void AddNameSurrnameToReservation(ObservableCollection<Guest> ListOfGuests, ObservableCollection<AccommodationReservation> reservatons)
        {
            foreach (var reservation in reservatons)
            {
                foreach (var guest in ListOfGuests)
                {
                    if (reservation.GuestId == guest.Id)
                    {
                        reservation.Name = guest.Name;
                        reservation.Surrname = guest.Surrname;
                    }
                }
            }
        }

        private void AddReservationName(ObservableCollection<AccommodationReservation> reservatons, ObservableCollection<Accommodation> accommodations )
        {
            foreach (var reservation in reservatons)
            {
                foreach (var accommodation in accommodations)
                {
                    if (reservation.Id == accommodation.id)
                    {
                        reservation.AccommodationName = accommodation.name;
                     
                    }
                }
            }
        }

    }
}
