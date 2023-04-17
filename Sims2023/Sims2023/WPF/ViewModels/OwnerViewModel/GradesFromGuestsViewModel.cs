using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class GradesFromGuestsViewModel
    {
        public ObservableCollection<AccommodationGrade> people { get; set; }
        public User owner { get; set; }
        private AccommodationGradeService _accommodationGradesService;
        private GuestGradeService _guestGradeService;
        public AccommodationGrade SelectedPerson { get; set; }

        public GradesFromGuestsViewModel(User user)
        {
            this.owner = user;
            _accommodationGradesService = new AccommodationGradeService();
            _guestGradeService = new GuestGradeService();
        }


        public List<AccommodationGrade> FindAllGuestsWhoGraded()
        {
            return _accommodationGradesService.
                FindAllGuestsWhoGraded(_accommodationGradesService.GetAllAccommodationGrades(), _guestGradeService.GetAllGrades(), owner);
        }


    }
}
