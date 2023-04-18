using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Collections.Generic;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class GuestsReservationReschedulingViewModel
    {
        public AccommodationReservationReschedulingService _reschedulingService;
        public GuestsReservationReschedulingViewModel()
        {
            _reschedulingService = new AccommodationReservationReschedulingService();
        }
        public List<AccommodationReservationRescheduling> GetGuestsReservationMove(User owner)
        {
            return _reschedulingService.GetGuestsForOwner(owner, _reschedulingService.GetAllReservationReschedulings());
        }

    }
}
