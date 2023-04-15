using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sims2023.Domain.Models.AccommodationReservationRescheduling;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class DeclineRescheduleViewModel
    {
        private DeclineRescheduleView View;
        private AccommodationReservationReschedulingService _rescheduleController;
        public AccommodationReservationRescheduling chosenPerson { get; set; }

        private ReschedulingDetailsView _reschedulingDetailsView;

        public ObservableCollection<AccommodationReservationRescheduling> peoplee;

        public DeclineRescheduleViewModel(DeclineRescheduleView view, AccommodationReservationRescheduling SelectedGuest, ReschedulingDetailsView reschedulingDetailsView, ObservableCollection<AccommodationReservationRescheduling> people)
        {
            _rescheduleController = new AccommodationReservationReschedulingService();
            View = view;
            chosenPerson = SelectedGuest;
            _reschedulingDetailsView = reschedulingDetailsView;
            peoplee = people;
        }

        public void Decline_Click(object sender, RoutedEventArgs e)
        {
            string text = View.MyTextBox.Text;
            chosenPerson.Comment = text;
            chosenPerson.Status = RequestStatus.Rejected;
            UpdateReschedule(chosenPerson);

            peoplee.Remove(chosenPerson);
            // Close the current window and previous window
            View.Close();
            _reschedulingDetailsView.Close();
        }

        public void UpdateReschedule(AccommodationReservationRescheduling chosen)
        {
             _rescheduleController.Update(chosen);
        }
           
    }
}
