using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Interaction logic for AllGuestsView.xaml
    /// </summary>
    public partial class AllGuestsView : Window
    {
        public ObservableCollection<AccommodationReservation> Reservatons { get; set; }  //
        public AccommodationReservation SelectedGuest { get; set; } //  

        public AllGuestsViewModel allGuestsViewModel;

        public AllGuestsView(User use,AccommodationReservationService acml, List<AccommodationReservation> reserv)
        {
            InitializeComponent();
            DataContext= this;
            
            allGuestsViewModel = new AllGuestsViewModel(use,acml,reserv);       
            Reservatons = new ObservableCollection<AccommodationReservation>(allGuestsViewModel.GetGradableGuests());
        }
        private void Grade_Click(object sender, EventArgs e)
        {
         
            if (SelectedGuest != null)
            {
                var gradeView = new Guest1GradeView(SelectedGuest, Reservatons);
                gradeView.Closed += GradeView_Closed;
                gradeView.Show();
            }
        }
       private void GradeView_Closed(object sender, EventArgs e)
        {
            var gradeView = (Guest1GradeView)sender;

            if (gradeView.GradeEntered)
            {
                Reservatons.Remove(SelectedGuest);
            }
        }
   }
}
