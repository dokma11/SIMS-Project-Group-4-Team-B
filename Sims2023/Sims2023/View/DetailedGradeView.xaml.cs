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
    /// Interaction logic for DetailedGradeView.xaml
    /// </summary>
    public partial class DetailedGradeView : Window
    {
        public String Name { get; set; }
        public String Surrname { get; set; }
        public String Accommodation { get; set; }
        public String Clean { get; set; }
        public String Correct { get; set; }
        public String Respect { get; set; }

        public String name { get; set; }
        public String surrname { get; set; }
        public String clean { get; set; }
        public String correct { get; set; }
        public String communication { get; set; }
        public String comment { get; set; }
        
        public DetailedGradeView(OwnerAndAccommodationGrade SelectedPerson)
        {
            InitializeComponent();
            DataContext = this;

            name = SelectedPerson.Name;
            surrname = SelectedPerson.Surrname;
            clean = SelectedPerson.Cleanliness.ToString();
            correct = SelectedPerson.Correct.ToString();
            communication= SelectedPerson.Communication.ToString();
            comment = SelectedPerson.Comment.ToString();
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
