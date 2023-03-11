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
    /// Interaction logic for addAccommodationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Window
    {
        private AccommodationController accommodationCtrl;
        private AccomodationLocationController accomodationLocationctrl;
        private Accommodation accommodation { get; set; }
        private AccommodationLocation acmLocation { get; set; }
        string outputText;
        public AccommodationRegistrationView(AccommodationController accommodationCtrl1, AccomodationLocationController accommodationLocationCtrll)
        {
            InitializeComponent();
            DataContext= this;
            
            accommodationCtrl = accommodationCtrl1;

            accomodationLocationctrl = accommodationLocationCtrll;

            accommodation = new Accommodation();

            acmLocation = new AccommodationLocation();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            int Id = 0;
            int Id2 = 0;
            string Name = textBox.Text;
            string Town = textBox1.Text;
            int idLocation=0;

          
            string Country = textBox2.Text;
            acmLocation = new AccommodationLocation(Id2, Town, Country);

            // if location doesnt exist I create a new one

            if (accomodationLocationctrl.findIdByCityCountry(acmLocation.city,acmLocation.country) == -1)
            {
                if (acmLocation.isVaild(acmLocation) == null)
                    accomodationLocationctrl.Create(acmLocation);
               
            }

           
           // now need to find what Id it has
           idLocation = accomodationLocationctrl.findIdByCityCountry(acmLocation.city,acmLocation.country);

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
            accommodation = new Accommodation(Id,Name, idLocation, Type, MaxGuests, MinDays, CancelDays, outputText);

            if (accommodation.isVaild(accommodation) == null)
            {
                accommodationCtrl.Create(accommodation);
                MessageBox.Show("uspijsna registracija smjestaja");
                Close();
            }
            else
            {
                string s = accommodation.isVaild(accommodation);
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
