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
    /// Interaction logic for Guest1GradeView.xaml
    /// </summary>
    public partial class Guest1GradeView : Window
    {
        private GuestGrade grade { get; set; }
        private AccommodationReservation guest { get; set; }

        private GuestGradeController gradeCtrl;

        public bool gradeEntered { get; set; }
         
        public Guest1GradeView(AccommodationReservation selectedGuest, ObservableCollection<AccommodationReservation> resevations)
        {
            InitializeComponent();
            DataContext = this;
            grade = new GuestGrade();
            guest = selectedGuest;
            gradeCtrl = new GuestGradeController();
           
            gradeEntered = false;
         
        }

        private void grade_click(object sender, EventArgs e)
        {
            int id = 0;
            string cleaN = comboBox.Text;
            int clean = int.Parse(cleaN);

            string Respectrules = comboBox1.Text;
            int RespectRules = int.Parse(Respectrules);

            string Communicationn = comboBox2.Text;
            int Communication = int.Parse(Communicationn);

            string comment = textBox.Text;


            grade = new GuestGrade(id, guest.AccommodationId, guest.GuestId, clean, RespectRules, Communication, comment);

            gradeCtrl.Create(grade);
            MessageBox.Show("dodan korisnik");
            gradeEntered = true;
            Close();

            

        }

    }
}
