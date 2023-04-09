using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Model;
using System.Windows;
using Sims2023.Domain.Models;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for addAccommodationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Window
    {
        private AccommodationController _accommodationController;
       
        private Accommodation Accommodation { get; set; }
        public User User { get; set; }

        private LocationService _locationService;

        string outputText;
        public AccommodationRegistrationView(AccommodationController accommodationCtrl1, AccomodationLocationController accommodationLocationCtrll,User owner)
        {
            InitializeComponent();
            DataContext = this;

            User = owner;

            _accommodationController = accommodationCtrl1;

            Accommodation = new Accommodation();

            _locationService = new LocationService();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            int Id = 0;
            int Id2 = 0;
            string Name = textBox.Text;
            string Town = textBox1.Text;
            int idLocation = 0;


            string Country = textBox2.Text;
            string Type = comboBox.Text;
            string MaxGuests1 = textBox3.Text;

            // making sure user enters integer
            int MaxGuests;
            bool isMaxGuestsValid = int.TryParse(MaxGuests1, out MaxGuests);
            if (!isMaxGuestsValid)
            {
                MessageBox.Show("Morate unijeti cijeli broj za dane");
                return;
            }
            MaxGuests = string.IsNullOrEmpty(MaxGuests1) ? -1 : int.Parse(MaxGuests1);
           
            string mindays = textBox4.Text;
            int MinDays;
            bool isMaxGuestsValid1 = int.TryParse(mindays, out MinDays);

            if (!isMaxGuestsValid1)
            {
                MessageBox.Show("Morate unijeti cijeli broj za dane");
                return;
            }
            MinDays = string.IsNullOrEmpty(mindays) ? -1 : int.Parse(mindays);
     
            string CancelDayss = textBox5.Text;
            int CancelDays;
            bool isMaxGuestsValid2 = int.TryParse(CancelDayss, out CancelDays);

            if (!isMaxGuestsValid1)
            {
                MessageBox.Show("Morate unijeti cijeli broj za dane");
                return;
            }

            Location Location = new Location(0, Town, Country);
            _locationService.Create(Location);

            CancelDays = string.IsNullOrEmpty(CancelDayss) ? 1 : int.Parse(CancelDayss);
            
            Accommodation = new Accommodation(Id, Name, Location, Type, MaxGuests, MinDays, CancelDays, outputText, User);

            if (Accommodation.IsVaild(Accommodation) == null)
            {
                _accommodationController.Create(Accommodation);
                MessageBox.Show("uspijsna registracija smjestaja");
                Close();
            }
            else
            {
                string s = Accommodation.IsVaild(Accommodation);
                MessageBox.Show(s);
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            string inputText = textBox6.Text;
            lstOutput.Items.Add(inputText);
            textBox6.Clear();
            outputText = outputText + "," + inputText;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
