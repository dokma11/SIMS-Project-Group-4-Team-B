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
        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationStay SelectedAccommodationStay { get; set; }
        private AccommodationReservationController _accommodationReservationController;

        int days;
        public AccommodationReservationConfirmationView(Accommodation selectedAccommodationS, AccommodationStay selectedAccommodationStayS, int daysNumber)
        {
            InitializeComponent();
            DataContext=this;
            SelectedAccommodation = selectedAccommodationS;
            SelectedAccommodationStay = selectedAccommodationStayS;
            days = daysNumber;

            _accommodationReservationController = new AccommodationReservationController();

            accommodatioNameTextBox.Text = selectedAccommodationS.Name;
            accommodatioCityTextBox.Text = selectedAccommodationS.City;
            accommodatioCountryTextBox.Text = selectedAccommodationS.Country;
            accommodatioTypeTextBox.Text = selectedAccommodationS.Type.ToString();
            accommodatioStartDateTextBox.Text = selectedAccommodationStayS.StartDate.ToString("MM/dd/yyyy");
            accommodatioEndDateTextBox.Text = selectedAccommodationStayS.EndDate.ToString("MM/dd/yyyy");

        }

        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            
            AccommodationReservation accommodationReservation = new AccommodationReservation(-1, 1,  SelectedAccommodation.Id, SelectedAccommodationStay.StartDate , SelectedAccommodationStay.EndDate, days);
            _accommodationReservationController.Create(accommodationReservation);

            MessageBox.Show("Uspesno ste rezervisali objekat!");

            Close();
            
        }

        private void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
