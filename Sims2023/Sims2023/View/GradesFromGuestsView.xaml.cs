using Sims2023.Controller;
using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for GradesFromGuestsView.xaml
    /// </summary>
    public partial class GradesFromGuestsView : Window
    {
        public ObservableCollection<AccommodationGrade> people { get; set; }  
        public User owner { get; set; } 
        private AccommodationGradeController _accommodationGradeController;

        private GuestGradeController _guestGradeController;

        public AccommodationGrade SelectedPerson { get; set; }

        public GradesFromGuestsView(User user)
        {
            InitializeComponent();
            DataContext= this;
            owner = user;
            _accommodationGradeController = new AccommodationGradeController();

            _guestGradeController = new GuestGradeController();
            people = new ObservableCollection<AccommodationGrade>(_accommodationGradeController.
                FindAllGuestsWhoGraded(_accommodationGradeController.GetAllAccommodationGrades(), _guestGradeController.GetAllGrades(),owner));
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPerson != null)
            {
                var GuestsGrades = new DetailedGradeView(SelectedPerson);
                GuestsGrades.Show();
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
