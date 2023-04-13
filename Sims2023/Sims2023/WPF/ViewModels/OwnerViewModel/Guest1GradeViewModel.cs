using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class Guest1GradeViewModel
    {
       
        private AccommodationReservation Guest { get; set; }

        private GuestGradeService _gradeService;

        public bool GradeEntered { get; set; }

        public Guest1GradeViewModel(AccommodationReservation selectedGuest, ObservableCollection<AccommodationReservation> resevations)
        {
            Guest = selectedGuest;
            _gradeService = new GuestGradeService();

        }

        public void CreateGrade(GuestGrade Grade)
        {
            _gradeService.Create(Grade);
        }
    }
}
