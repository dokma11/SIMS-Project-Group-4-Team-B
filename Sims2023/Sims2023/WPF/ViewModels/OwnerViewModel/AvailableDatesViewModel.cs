using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class AvailableDatesViewModel
    {
        public Accommodation selectedAccommodation { get; set; }
        public List<AccommodationStay> dates { get; set; }

        public AccommodationRenovationService _renovationService;
        public AccommodationService _accommodationService;
  
        public AccommodationStay SelectedDate { get; set; }


        public AvailableDatesViewModel(Accommodation selectedAccommodationn, List<AccommodationStay> datess)
        {
            selectedAccommodation = selectedAccommodationn;
            dates = datess;
            _renovationService = new AccommodationRenovationService();
        }

        public void Schedule_Click()
        {
            if (SelectedDate!=null)
            {
                AccommodationRenovation renovation = new AccommodationRenovation(0, selectedAccommodation, SelectedDate.StartDate, SelectedDate.EndDate);
                _renovationService.Create(renovation);
                ToastNotificationService.ShowInformation("Uspiješno zakazivanje renoviranja");

            }
        }
    }
}
