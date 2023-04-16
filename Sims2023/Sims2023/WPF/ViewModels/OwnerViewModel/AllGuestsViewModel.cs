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
        public AccommodationReservation SelectedGuest { get; set; }          
        public List<AccommodationReservation> ReservationsList { get; set; }  //

        private GuestGradeService _gradeService; //

        public ObservableCollection<AccommodationReservation> Reservatons { get; set; }  //

        public AllGuestsView allGuestsView;
        public User user { get; set; }  //

        public AllGuestsViewModel(AllGuestsView view,User use, List<AccommodationReservation> reserv)
        {
            allGuestsView = view;
            user = use;
            _reservationService = new AccommodationReservationService();
            ReservationsList = reserv;
            _gradeService = new GuestGradeService();
            Reservatons = new ObservableCollection<AccommodationReservation>(GetGradableGuests());
        }

       public List<AccommodationReservation> GetGradableGuests()
        {
            return _reservationService.GetGradableGuests(user, ReservationsList, _gradeService.GetAllGrades());
        }
    }
}
