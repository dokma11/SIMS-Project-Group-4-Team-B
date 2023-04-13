using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class AllGuestsViewModel
    {
        private AccommodationReservationService _reservationService; //
        public ObservableCollection<AccommodationReservation> Reservatons { get; set; }


        public AccommodationReservation SelectedGuest { get; set; }
        public List<AccommodationReservation> Reservatons2 { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public List<AccommodationReservation> ReservationsList { get; set; }  //

        private AccommodationService _accommodationService; //

        private GuestGradeService _gradeService; //
        public User user { get; set; }  //

        public AllGuestsViewModel(User use, AccommodationReservationService acml, List<AccommodationReservation> reserv)
        {
            user = use;
            _reservationService = acml;
            ReservationsList = reserv;
            _gradeService = new GuestGradeService();
            _accommodationService = new AccommodationService();


        }

        public List<AccommodationReservation> GetGradableGuests()
        {
            return _reservationService.GetGradableGuests(user, ReservationsList, _gradeService.GetAllGrades());
        }

     
    }
}
