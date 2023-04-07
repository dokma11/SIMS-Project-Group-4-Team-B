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
        private AccommodationController _accommodationController;
        private AccomodationLocationController _accommodationLocationController;
        private AccommodationReservationController _accommodationReservationController;
        private GuestGradeController _gradeController;
        private GuestFileHandler fh;
        private List<Guest> Guests;
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
            fh = new GuestFileHandler();
            Guests = new List<Guest>(fh.Load());
            _accommodationReservationController = new AccommodationReservationController();
            _gradeController = new GuestGradeController();

            Reservatons = new List<AccommodationReservation>(_accommodationReservationController.GetAllReservations());

            GradableGuests = new List<AccommodationReservation>(_accommodationReservationController.GetGradableGuests(Guests, _accommodationController.GetAllAccommodations(),
                                                                                Reservatons, _gradeController.GetAllGrades()));

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


        private void Grade_Click(object sender, RoutedEventArgs e)
        {
            var guestss = new AllGuestsView(Guests, _accommodationReservationController, Reservatons);
            guestss.Show();
        }

        private void AddAccommodation_Click(object sender, RoutedEventArgs e)
        {
            var addAccommodation = new AccommodationRegistrationView(_accommodationController, _accommodationLocationController,User);
            addAccommodation.Show();
        }
    }
}
