using Sims2023.Controller;
using Sims2023.Model;
using System.Windows;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for addAccommodationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Window
    {
        private AccommodationController _accommodationController;
        private AccomodationLocationController _accomodationLocationController;
        private Accommodation Accommodation { get; set; }
        private AccommodationLocation AccommodationLocation { get; set; }
        string outputText;
        public AccommodationRegistrationView(AccommodationController accommodationCtrl1, AccomodationLocationController accommodationLocationCtrll)
        {
            InitializeComponent();
            DataContext = this;

            _accommodationController = accommodationCtrl1;

            _accomodationLocationController = accommodationLocationCtrll;

            Accommodation = new Accommodation();

            AccommodationLocation = new AccommodationLocation();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            int Id = 0;
            int Id2 = 0;
            string Name = textBox.Text;
            string Town = textBox1.Text;
            int idLocation = 0;


            string Country = textBox2.Text;
            AccommodationLocation = new AccommodationLocation(Id2, Town, Country);

            // if location doesnt exist I create a new one

            if (_accomodationLocationController.FindIdByCityCountry(AccommodationLocation.City, AccommodationLocation.Country) == -1)
            {
                if (AccommodationLocation.IsVaild(AccommodationLocation) == null)
                    _accomodationLocationController.Create(AccommodationLocation);

            }


            // now need to find what Id it has
            idLocation = _accomodationLocationController.FindIdByCityCountry(AccommodationLocation.City, AccommodationLocation.Country);

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


            // same for minimum days

            string mindays = textBox4.Text;
            int MinDays;
            bool isMaxGuestsValid1 = int.TryParse(mindays, out MinDays);

            if (!isMaxGuestsValid1)
            {
                MessageBox.Show("Morate unijeti cijeli broj za dane");
                return;
            }
            MinDays = string.IsNullOrEmpty(mindays) ? -1 : int.Parse(mindays);


            // and of course for cancel days
            string CancelDayss = textBox5.Text;
            int CancelDays;
            bool isMaxGuestsValid2 = int.TryParse(CancelDayss, out CancelDays);

            if (!isMaxGuestsValid1)
            {
                MessageBox.Show("Morate unijeti cijeli broj za dane");
                return;
            }

            CancelDays = string.IsNullOrEmpty(CancelDayss) ? 1 : int.Parse(CancelDayss);
            Accommodation = new Accommodation(Id, Name, idLocation, Type, MaxGuests, MinDays, CancelDays, outputText);

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
