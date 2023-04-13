using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class GuestsReservationReschedulingViewModel
    {
        public AccommodationReservationReschedulingController _reschedulingController;
        public GuestsReservationReschedulingViewModel()
        {
            _reschedulingController = new AccommodationReservationReschedulingController();
        }
        public List<AccommodationReservationRescheduling> GetGuestsReservationMove(User owner)
        {
           return _reschedulingController.GetGuestsReservationMove(owner, _reschedulingController.GetAllReservationReschedulings());
        }
     
    }
}
