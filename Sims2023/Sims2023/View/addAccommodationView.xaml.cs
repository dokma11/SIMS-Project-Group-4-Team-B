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
    public partial class addAccommodationView : Window
    {
        private AccommodationController accommodationCtrl;
        private Accommodation accommodation { get; set; }
        string outputText;
        public addAccommodationView(AccommodationController accommodationCtrl1)
        {
            InitializeComponent();
            DataContext= this;
            accommodationCtrl = accommodationCtrl1;
            accommodation = new Accommodation();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            int Id = 0;
            string Name = textBox.Text;
            string Town = textBox1.Text;
            string Country = textBox2.Text;
            string Type = comboBox.Text;
            string MaxGuests1 = textBox3.Text;
          

            int MaxGuests = string.IsNullOrEmpty(MaxGuests1) ? -1 : int.Parse(MaxGuests1);

            string mindays = textBox4.Text;
           
            int Mindays = string.IsNullOrEmpty(mindays) ? -1 : int.Parse(mindays);
            string CancelDayss = textBox5.Text;


           
          





            int CancelDays = string.IsNullOrEmpty(CancelDayss) ? -1 : int.Parse(CancelDayss);
            accommodation = new Accommodation(Id,Name, Town, Country, Type, MaxGuests, Mindays, CancelDays, outputText);

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
