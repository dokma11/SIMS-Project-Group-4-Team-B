using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using static Sims2023.Domain.Models.AccommodationReservationRescheduling;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class ReschedulingDetailsViewModel
    {
        private ReschedulingDetailsView View;
        public AccommodationReservationRescheduling guest { get; set; }
        public AccommodationReservation UpdatedReservationStatus { get; set; }

        public AccommodationReservationService _reservationService;

        public ObservableCollection<AccommodationReservationRescheduling> peoplee;

        public AccommodationReservationReschedulingService _reschedulingService;

        public AccommodationReservationReschedulingService _reschedulingController;

        public ReschedulingDetailsViewModel(AccommodationReservationRescheduling SelectedGuest, ObservableCollection<AccommodationReservationRescheduling> people, ReschedulingDetailsView view)
        {
            _reservationService = new AccommodationReservationService();
            _reschedulingService = new AccommodationReservationReschedulingService();
            View = view;
            guest = SelectedGuest;
            peoplee = people;
            _reschedulingController = new AccommodationReservationReschedulingService();
            UpdatedReservationStatus = new AccommodationReservation();
        }

        public void button2_Click(object sender, RoutedEventArgs e)
        {
            guest.Status = RequestStatus.Approved;
            UpdatedReservationStatus = GetById(guest.AccommodationReservation.Id);
            UpdatedReservationStatus.StartDate = guest.NewStartDate;
            UpdatedReservationStatus.EndDate = guest.NewEndDate;
            TimeSpan delta = guest.NewEndDate - guest.NewStartDate;
            UpdatedReservationStatus.NumberOfDays = (int)delta.TotalDays;
            UpdateReservation(UpdatedReservationStatus);
            UpdateReschedule(guest);
            peoplee.Remove(guest);
            View.Close();
        }


        public bool isAccommodationFree(AccommodationReservationRescheduling guest)
        {
            return _reschedulingService.isAccommodationFree(guest);
        }

        public void UpdateReservation(AccommodationReservation reservation)
        {
            _reservationService.Update(reservation);
        }

        public void UpdateReschedule(AccommodationReservationRescheduling guest)
        {
            _reschedulingService.Update(guest);
        }

        public AccommodationReservation GetById(int id)
        {
            return _reservationService.GetById(id);
        }

    }
}
