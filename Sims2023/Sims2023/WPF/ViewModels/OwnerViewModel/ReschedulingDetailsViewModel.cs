using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class ReschedulingDetailsViewModel
    {
        public AccommodationReservationRescheduling guest { get; set; }
        public AccommodationReservation UpdatedReservationStatus { get; set; }

        public AccommodationReservationService _reservationService;

        public ObservableCollection<AccommodationReservationRescheduling> peoplee;

        public AccommodationReservationReschedulingService _reschedulingService;

        public ReschedulingDetailsViewModel()
        {
            _reservationService = new AccommodationReservationService();
            _reschedulingService = new AccommodationReservationReschedulingService();

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
