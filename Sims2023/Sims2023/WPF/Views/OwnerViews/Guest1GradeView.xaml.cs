using Sims2023.Application.Services;
using Sims2023.Domain.Models;
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
        private GuestGrade Grade { get; set; }
        private AccommodationReservation Guest { get; set; }

        private GuestGradeService _gradeService;

        public bool GradeEntered { get; set; }
         
        public Guest1GradeView(AccommodationReservation selectedGuest, ObservableCollection<AccommodationReservation> resevations)
        {
            InitializeComponent();
            DataContext = this;
            Grade = new GuestGrade();
            Guest = selectedGuest;
            _gradeService = new GuestGradeService();
           
            GradeEntered = false;
         
        }

   
        private void Grade_click(object sender, EventArgs e)
        {
            int id = 0;
            string cleaN = comboBox.Text;
            string Respectrules = comboBox1.Text;
            string Communicationn = comboBox2.Text;
            string comment = textBox.Text;

            if (IsValid(cleaN, Respectrules, Communicationn, comment))
            {
                int clean = int.Parse(cleaN);
                int RespectRules = int.Parse(Respectrules);
                int Communication = int.Parse(Communicationn);
                Grade = new GuestGrade(id, Guest.Accommodation, Guest.Guest, clean, RespectRules, Communication, comment);
                _gradeService.Create(Grade);
                MessageBox.Show("Uspijesno davanje ocjene");
                GradeEntered = true;
                Close();
            }
            else MessageBox.Show("Popunite sve podatke");
        }

        public bool IsValid(string cleaN, string Respectrules, string Communicationn, string comment)
        {
            int clean;
            bool isCleanValid = int.TryParse(cleaN, out clean);
            int RespectRules;
            bool isRulesValid = int.TryParse(Respectrules, out RespectRules);
            int Communication;
            bool isCommunicationValid = int.TryParse(Communicationn, out Communication);

            if (!isCleanValid)
            {

                return false;
            }


            else if (!isRulesValid)
            {

                return false;
            }

            else if (!isCommunicationValid)
            {

                return false;
            }

            else if (string.IsNullOrEmpty(comment)) return false;

            else return true;


        }

    }
}
