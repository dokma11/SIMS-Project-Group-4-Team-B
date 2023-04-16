using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
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
using static Sims2023.Domain.Models.AccommodationReservationRescheduling;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for DeclineRescheduleView.xaml
    /// </summary>
    public partial class DeclineRescheduleView : Window
    {
        public AccommodationReservationRescheduling chosenPerson { get; set; }

        private ReschedulingDetailsView _reschedulingDetailsView;

        private DeclineRescheduleViewModel DeclineRescheduleViewModel;

        public ObservableCollection<AccommodationReservationRescheduling> peoplee;
        public DeclineRescheduleView(AccommodationReservationRescheduling SelectedGuest, ReschedulingDetailsView reschedulingDetailsView, ObservableCollection<AccommodationReservationRescheduling> people)
        {
            InitializeComponent();
            DeclineRescheduleViewModel = new DeclineRescheduleViewModel(this, SelectedGuest, reschedulingDetailsView, people);
            DataContext = DeclineRescheduleViewModel;
            
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            DeclineRescheduleViewModel.Decline_Click(sender, e);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}