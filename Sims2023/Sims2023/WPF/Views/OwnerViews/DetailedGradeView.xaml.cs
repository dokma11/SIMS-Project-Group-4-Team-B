using Sims2023.Domain.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for DetailedGradeView.xaml
    /// </summary>
    public partial class DetailedGradeView : Page
    {
        public String name { get; set; }
        public String surrname { get; set; }
        public String accommodationName { get; set; }
        public String clean { get; set; }
        public String correct { get; set; }
        public String value { get; set; }
        public String location { get; set; }
        public String comment { get; set; }

        public string welcomeString { get; set; }
        public DetailedGradeView(AccommodationGrade SelectedPerson)
        {
            InitializeComponent();
            DataContext = this;

            name = SelectedPerson.Guest.Name;
            surrname = SelectedPerson.Guest.Surname;
            accommodationName = SelectedPerson.Accommodation.Name;
            clean = SelectedPerson.Cleanliness.ToString();
            correct = SelectedPerson.Comfort.ToString();
            value= SelectedPerson.ValueForMoney.ToString();
            location = SelectedPerson.Location.ToString();
            comment = SelectedPerson.Comment.ToString();

            welcomeString = welcome_string();
            string welcome_string()
            {
                var message = "Ocijene dobijene od gosta\n        " + SelectedPerson.Guest.Name + " " + SelectedPerson.Guest.Surname;
                return message;

            }
        }

       
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService?.GoBack();
        }
    }
}
