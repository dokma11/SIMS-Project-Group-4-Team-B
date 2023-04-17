using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Collections.Generic;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class GuestsReservationReschedulingViewModel
    {
        public AccommodationReservationReschedulingService _reschedulingController;
        public GuestsReservationReschedulingViewModel()
        {
            _reschedulingController = new AccommodationReservationReschedulingService();
        }
        public List<AccommodationReservationRescheduling> GetGuestsReservationMove(User owner)
        {
            return _reschedulingController.GetGuestsReservationMove(owner, _reschedulingController.GetAllReservationReschedulings());
        }

    }
}
