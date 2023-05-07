using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class Guest1GradeViewModel
    {

        public Guest1GradeView View;
        private AccommodationReservation Guest { get; set; }

        private GuestGradeService _gradeService;
        
        private AccommodationReservationService _accommodationReservationService;
        private GuestGrade Grade { get; set; }
        ObservableCollection<AccommodationReservation> resevationss { get; set; }

        public bool GradeEntered { get; set; }

        public Guest1GradeViewModel(Guest1GradeView view, AccommodationReservation selectedGuest, ObservableCollection<AccommodationReservation> resevations)
        {
            Guest = selectedGuest;
            _gradeService = new GuestGradeService();
            View = view;
            resevationss = resevations;
            _accommodationReservationService = new AccommodationReservationService();
        }

        public void Grade_click(object sender, EventArgs e)
        {
            int id = 0;
            string cleaN = View.comboBox.Text;
            string Respectrules = View.comboBox1.Text;
            string Communicationn = View.comboBox2.Text;
            string comment = View.textBox.Text;

            if (IsValid(cleaN, Respectrules, Communicationn, comment))
            {
                int clean = int.Parse(cleaN);
                int RespectRules = int.Parse(Respectrules);
                int Communication = int.Parse(Communicationn);
                Grade = new GuestGrade(id, Guest.Accommodation, Guest.Guest, clean, RespectRules, Communication, comment,Guest.StartDate,Guest.EndDate);
                CreateGrade(Grade);
                resevationss.Remove(Guest);
                MessageBox.Show("Uspijesno davanje ocjene");
                View.NavigationService.GoBack();

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

            if (!isCleanValid) return false;

            else if (!isRulesValid) return false;

            else if (!isCommunicationValid) return false;

            else if (string.IsNullOrEmpty(comment)) return false;

            else return true;
        }

        public void CreateGrade(GuestGrade Grade)
        {
            _gradeService.Create(Grade);
        }

        public void Close_Click(object sender, EventArgs e)
        {
            View.NavigationService.GoBack();
        }
    }
}
