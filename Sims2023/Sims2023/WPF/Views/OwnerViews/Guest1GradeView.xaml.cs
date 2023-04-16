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
        public Guest1GradeViewModel Guest1GradeViewModel;
         
        public Guest1GradeView(AccommodationReservation selectedGuest, ObservableCollection<AccommodationReservation> resevations)
        {
            InitializeComponent();
            Guest1GradeViewModel = new Guest1GradeViewModel(this,selectedGuest, resevations);
            DataContext = Guest1GradeViewModel;
          
        }

   
        private void Grade_click(object sender, EventArgs e)
        {
            Guest1GradeViewModel.Grade_click(sender, e);
        }

    }
}
