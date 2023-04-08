﻿using Sims2023.Controller;
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
       
        private Accommodation Accommodation { get; set; }
        private AccommodationLocation AccommodationLocation { get; set; }
        public User User { get; set; }

        private LocationController _locationController;

        string outputText;
        public AccommodationRegistrationView(AccommodationController accommodationCtrl1, AccomodationLocationController accommodationLocationCtrll,User owner)
        {
            InitializeComponent();
            DataContext = this;

            User = owner;

            _accommodationController = accommodationCtrl1;


            Accommodation = new Accommodation();

            AccommodationLocation = new AccommodationLocation();

            _locationController = new LocationController();
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

            Location Location = new Location(0, Town, Country);
            _locationController.Create(Location);

            CancelDays = string.IsNullOrEmpty(CancelDayss) ? 1 : int.Parse(CancelDayss);
            bool super = false;
            Accommodation = new Accommodation(Id, Name, Location, Type, MaxGuests, MinDays, CancelDays, outputText, User, super);

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
