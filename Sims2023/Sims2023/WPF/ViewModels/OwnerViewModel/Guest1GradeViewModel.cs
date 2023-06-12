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
using System.Windows.Media;
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
                ToastNotificationService.ShowInformation("Uspiješno davanje ocjene");
                View.NavigationService.GoBack();

            }
            else ToastNotificationService.ShowInformation("Popunite sve podatke");
        }

        public bool IsValid(string clean, string respectrules, string communication, string comment)
        {
            int cleann;
            bool isCleanValid = int.TryParse(clean, out cleann);
            int RespectRules;
            bool isRulesValid = int.TryParse(respectrules, out RespectRules);
            int Communication;
            bool isCommunicationValid = int.TryParse(communication, out Communication);

            List<Control> invalidControls = new List<Control>();

            // Reset background color of all fields
            View.comboBox.ClearValue(ComboBox.BackgroundProperty);
            View.comboBox1.ClearValue(ComboBox.BackgroundProperty);
            View.comboBox2.ClearValue(ComboBox.BackgroundProperty);
            View.textBox.ClearValue(TextBox.BackgroundProperty);

            if (!isCleanValid)
            {
                invalidControls.Add(View.comboBox);
            }
            if (!isRulesValid)
            {
                invalidControls.Add(View.comboBox1);
            }
            if (!isCommunicationValid)
            {
                invalidControls.Add(View.comboBox2);
            }
            if (string.IsNullOrEmpty(comment))
            {
                invalidControls.Add(View.textBox);
            }

            if (invalidControls.Count > 0)
            {
                SetControlsBackground(invalidControls);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SetControlsBackground(List<Control> controls)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
            foreach (Control control in controls)
            {
                control.Background = redBrush;
            }
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
