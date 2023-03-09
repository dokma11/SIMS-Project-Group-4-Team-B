using Sims2023.Controller;
using Sims2023.Observer;
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
    /// Interaction logic for OpenAccommodationRegistrationView.xaml
    /// </summary>
    public partial class OpenAccommodationRegistrationView : Window
    {
        private string outputText;
        private AccommodationController accommodationCtrl;
        private AccomodationLocationController accommodationLocationCtrl;

        public OpenAccommodationRegistrationView()
        {
            
            InitializeComponent();
            DataContext = this;
            accommodationCtrl = new AccommodationController();
            accommodationLocationCtrl = new AccomodationLocationController();
        }



        private void addAccommodationClick(object sender, RoutedEventArgs e)
        {
            var addAccommodation = new addAccommodationView(accommodationCtrl, accommodationLocationCtrl);
            addAccommodation.Show();
        }
    }
}
