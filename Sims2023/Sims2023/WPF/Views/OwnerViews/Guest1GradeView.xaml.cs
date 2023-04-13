using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
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
        ObservableCollection<AccommodationReservation> resevationss { get; set; }

        public Guest1GradeViewModel Guest1GradeViewModel;
         
        public Guest1GradeView(AccommodationReservation selectedGuest, ObservableCollection<AccommodationReservation> resevations)
        {
            InitializeComponent();
            DataContext = this;
            Guest1GradeViewModel = new Guest1GradeViewModel(selectedGuest, resevations);
            Grade = new GuestGrade();
            Guest = selectedGuest;          
            resevationss = resevations;
             
         
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
                Guest1GradeViewModel.CreateGrade(Grade);
                resevationss.Remove(Guest);
                MessageBox.Show("Uspijesno davanje ocjene");
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

            if (!isCleanValid)      return false;

            else if (!isRulesValid) return false;
            
            else if (!isCommunicationValid) return false;
            
            else if (string.IsNullOrEmpty(comment)) return false;

            else return true;


        }

    }
}
