using Sims2023.Controller;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
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
        private AccommodationController accommodationCtrl;
        private AccomodationLocationController accommodationLocationCtrl;

        public OwnerView()
        {
            
            InitializeComponent();
            DataContext = this;
            accommodationCtrl = new AccommodationController();
            accommodationLocationCtrl = new AccomodationLocationController();





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
                  
                    MessageBox.Show("Kliknite na dugme da bi ocijenili goste koji su odsjeli u ptethodnih par dana kod vas");

                    // Update the last shown date to today's date
                    File.WriteAllText(fileName, DateTime.Today.ToString());
                }
            }
            catch (FileNotFoundException)
            {
                // The last shown date file does not exist, so create it with today's date
                File.WriteAllText(fileName, DateTime.Today.ToString());
            }
        }




        private void addAccommodationClick(object sender, RoutedEventArgs e)
        {
            var addAccommodation = new AccommodationRegistrationView(accommodationCtrl, accommodationLocationCtrl);
            addAccommodation.Show();
        }
    }
}
