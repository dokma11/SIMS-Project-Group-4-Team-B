using Sims2023.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AccommodationReservationConfirmationWindow.xaml
    /// </summary>
    public partial class AccommodationReservationDateWindow : Window
    {

        public List<AccommodationStay> stays = new List<AccommodationStay>();
        public Accommodation selectedAccommodation { get; set; }
        public AccommodationStay selectedAccommodationStay { get; set; }

        int daysNumber;
        public AccommodationReservationDateWindow(Accommodation selectedAccommodationS,List<AccommodationStay> stays, int daysNUmber)
        {
            InitializeComponent();
            DataContext = this;

            daysNumber=daysNUmber;

            stays = stays;
            selectedAccommodation = selectedAccommodationS;
            availableDatesGrid.ItemsSource = stays;

        }

        private void buttonDateConfirmation_Click(object sender, RoutedEventArgs e)
        {
            selectedAccommodationStay = (AccommodationStay)availableDatesGrid.SelectedItem;
            if (selectedAccommodationStay == null)
            {
                MessageBox.Show("Molimo Vas selektujte datume koje zelite da rezervisete.");
                return;
            }
            if (selectedAccommodation == null)
            {
                MessageBox.Show("MDoslo je do greske.");
                return;
            }
            AccommodationReservationConfirmationWindow accommodationReservationConfirmationWindow = new AccommodationReservationConfirmationWindow(selectedAccommodation,selectedAccommodationStay,daysNumber);
            accommodationReservationConfirmationWindow.Show();
        }

        private void buttonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
