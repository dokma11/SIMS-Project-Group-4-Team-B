using Sims2023.Controller;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.Repository;
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
using System.Windows.Shapes;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for OpenAccommodationRegistrationView.xaml
    /// </summary>
    public partial class OwnerView : Window
    {
        private string outputText;
        private AccommodationController accommodationCtrl;
        private AccomodationLocationController accommodationLocationCtrl;
        private AccommodationReservationController accommodationReservationCtrl;
        private GuestFileHandler fh;
        private List<Guest> guests;
        public List<AccommodationReservation> reservatons { get; set; }

        public OwnerView()
        {
            
            InitializeComponent();
            DataContext = this;
            accommodationCtrl = new AccommodationController();
            accommodationLocationCtrl = new AccomodationLocationController();
            fh = new GuestFileHandler();
            guests = new List<Guest>(fh.Load());
            accommodationReservationCtrl = new AccommodationReservationController();

            reservatons = new List<AccommodationReservation>(accommodationReservationCtrl.GetAllReservations());

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string fileName = "../../../Resources/Data/lastshown.txt";

            try
            {
                string lastShownText = File.ReadAllText(fileName);
                DateTime lastShownDate = DateTime.Parse(lastShownText);

                if (lastShownDate < DateTime.Today)
                {
                  
                    MessageBox.Show("Kliknite na dugme da bi ocijenili goste koji su odsjeli u prethodnih par dana kod vas");

                    // Update the last shown date to today's date
                    File.WriteAllText(fileName, DateTime.Today.ToString());
                }
            }
            catch (FileNotFoundException)
            {
                // The last shown date file does not exist, so create it with today's date
                File.WriteAllText(fileName, DateTime.Today.ToString());
            }
        }


        private void grade_click(object sender, RoutedEventArgs e)
        {
            var guestss = new AllGuestsView(guests, accommodationReservationCtrl, reservatons);
            guestss.Show();
        }

        private void addAccommodationClick(object sender, RoutedEventArgs e)
        {
            var addAccommodation = new AccommodationRegistrationView(accommodationCtrl, accommodationLocationCtrl);
            addAccommodation.Show();
        }
    }
}
