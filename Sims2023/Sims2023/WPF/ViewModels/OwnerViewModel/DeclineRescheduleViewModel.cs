using Sims2023.Application.Services;
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
    public class DeclineRescheduleViewModel
    {
        private AccommodationReservationReschedulingService _rescheduleController;

        public DeclineRescheduleViewModel()
        {
            _rescheduleController = new AccommodationReservationReschedulingService();
        }

        public void UpdateReschedule(AccommodationReservationRescheduling chosen)
        {
             _rescheduleController.Update(chosen);
        }
           
    }
}
