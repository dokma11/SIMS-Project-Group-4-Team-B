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
    public partial class AccommodationReservationDateView : Window
    {

        public List<AccommodationStay> stays = new List<AccommodationStay>();
        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationStay SelectedAccommodationStay { get; set; }

        int daysNumber;
        public AccommodationReservationDateView(Accommodation selectedAccommodationS,List<AccommodationStay> stays, int daysNUmber)
        {
            InitializeComponent();
            DataContext = this;

            daysNumber=daysNUmber;

            stays = stays;
            SelectedAccommodation = selectedAccommodationS;
            availableDatesGrid.ItemsSource = stays;

        }

        private void ButtonDateConfirmation_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationStay = (AccommodationStay)availableDatesGrid.SelectedItem;
            if (SelectedAccommodationStay == null)
            {
                MessageBox.Show("Molimo Vas selektujte datume koje zelite da rezervisete.");
                return;
            }
            if (SelectedAccommodation == null)
            {
                MessageBox.Show("MDoslo je do greske.");
                return;
            }
            AccommodationReservationConfirmationView accommodationReservationConfirmationView = new AccommodationReservationConfirmationView(SelectedAccommodation,SelectedAccommodationStay,daysNumber);
            accommodationReservationConfirmationView.Show();
        }

        private void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
