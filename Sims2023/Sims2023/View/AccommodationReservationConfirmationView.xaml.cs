using Sims2023.Controller;
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
    
    public partial class AccommodationReservationConfirmationView : Window
    {
        public Accommodation selectedAccommodation { get; set; }
        public AccommodationStay selectedAccommodationStay { get; set; }
        private AccommodationReservationController _accommodationReservationController { get; set; }

        int days;
        public AccommodationReservationConfirmationView(Accommodation selectedAccommodationS, AccommodationStay selectedAccommodationStayS, int daysNumber)
        {
            InitializeComponent();
            DataContext=this;
            selectedAccommodation = selectedAccommodationS;
            selectedAccommodationStay = selectedAccommodationStayS;
            days = daysNumber;

            _accommodationReservationController = new AccommodationReservationController();

            accommodatioNameTextBox.Text = selectedAccommodationS.name;
            accommodatioCityTextBox.Text = selectedAccommodationS.city;
            accommodatioCountryTextBox.Text = selectedAccommodationS.country;
            accommodatioTypeTextBox.Text = selectedAccommodationS.type.ToString();
            accommodatioStartDateTextBox.Text = selectedAccommodationStayS.StartDate.ToString("MM/dd/yyyy");
            accommodatioEndDateTextBox.Text = selectedAccommodationStayS.EndDate.ToString("MM/dd/yyyy");

        }

        private void reservationButton_Click(object sender, RoutedEventArgs e)
        {
            
            AccommodationReservation accommodationReservation = new AccommodationReservation(-1, 1,  selectedAccommodation.id, selectedAccommodationStay.StartDate , selectedAccommodationStay.EndDate, days);
            _accommodationReservationController.Create(accommodationReservation);

            MessageBox.Show("Uspesno ste rezervisali objekat!");

            Close();
            
        }

        private void buttonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
