using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class AllRenovationsViewModel
    {
        public ObservableCollection<AccommodationRenovation> renovations { get; set; }
        public AccommodationRenovationService _accommodationRenovationService;
        public AccommodationRenovation SelectedRenovation { get; set; }
        public AllRenovationsViewModel(User user)
        {
            renovations = new ObservableCollection<AccommodationRenovation>();
            _accommodationRenovationService = new AccommodationRenovationService();
            foreach(AccommodationRenovation renovation in _accommodationRenovationService.GetAll())
            {
                if (renovation.Accommodation.Owner.Id == user.Id)
                    renovations.Add(renovation);
            }

        }

        public void Delete_Click()
        {
            if (SelectedRenovation != null && SelectedRenovation.Status == "nije zapoceto")
            {
                _accommodationRenovationService.Delete(SelectedRenovation);
                renovations.Remove(SelectedRenovation);
                
            }
        }
    }
}
