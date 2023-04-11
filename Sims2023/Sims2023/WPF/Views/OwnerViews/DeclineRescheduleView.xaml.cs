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
using static Sims2023.Model.AccommodationReservationRescheduling;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for DeclineRescheduleView.xaml
    /// </summary>
    public partial class DeclineRescheduleView : Window
    {
        public AccommodationReservationRescheduling chosenPerson { get; set; }

        private ReschedulingDetailsView _reschedulingDetailsView;

        private AccommodationReservationReschedulingController _rescheduleController;

        public ObservableCollection<AccommodationReservationRescheduling> peoplee;
        public DeclineRescheduleView(AccommodationReservationReschedulingController _reschedulingController, AccommodationReservationRescheduling SelectedGuest, ReschedulingDetailsView reschedulingDetailsView, ObservableCollection<AccommodationReservationRescheduling> people)
        {
            InitializeComponent();
            DataContext = this;
            _rescheduleController = _reschedulingController;
            chosenPerson = SelectedGuest;
            _reschedulingDetailsView = reschedulingDetailsView;
            peoplee = people;
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            string text = MyTextBox.Text;
            chosenPerson.Comment = text;
            chosenPerson.Status = RequestStatus.Rejected;
            _rescheduleController.Update(chosenPerson);

            peoplee.Remove(chosenPerson);
            // Close the current window
            this.Close();
            
            _reschedulingDetailsView.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
