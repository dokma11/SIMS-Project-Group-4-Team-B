using Sims2023.Controller;
using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class DeclineRescheduleViewModel
    {
        private AccommodationReservationReschedulingController _rescheduleController;

        public DeclineRescheduleViewModel()
        {
            _rescheduleController = new AccommodationReservationReschedulingController();
        }

        public void UpdateReschedule(AccommodationReservationRescheduling chosen)
        {
             _rescheduleController.Update(chosen);
        }
           
    }
}
