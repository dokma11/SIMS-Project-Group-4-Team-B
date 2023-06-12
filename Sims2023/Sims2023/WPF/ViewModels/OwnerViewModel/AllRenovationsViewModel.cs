using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class AllRenovationsViewModel
    {
        public ObservableCollection<AccommodationRenovation> renovations { get; set; }
        public AccommodationRenovationService _accommodationRenovationService;
        public AccommodationRenovation SelectedRenovation { get; set; }
        public RelayCommand CancelRenovation { get; set; }
        public RelayCommand Back { get; set; }

        public AllRenovationsViewModel(User user)
        {
            CancelRenovation = new RelayCommand(Executed_CancelCommand, CanExecute_CancelCommand);
            Back = new RelayCommand(Executed_BackCommand, CanExecute_BackCommand);
            renovations = new ObservableCollection<AccommodationRenovation>();
            _accommodationRenovationService = new AccommodationRenovationService();
            foreach(AccommodationRenovation renovation in _accommodationRenovationService.GetAll())
            {
                if (renovation.Accommodation.Owner.Id == user.Id)
                    renovations.Add(renovation);
            }

        }

        private bool CanExecute_BackCommand(object obj)
        {
            return true;
        }

        private void Executed_BackCommand(object obj)
        {
            if (FrameManager.Instance.MainFrame.CanGoBack)
            {
                FrameManager.Instance.MainFrame.GoBack();
            }
        }

        public void Executed_CancelCommand(object obj)
        {

            TimeSpan difference = SelectedRenovation.StartDate - DateTime.Today;
            if (SelectedRenovation != null && SelectedRenovation.Status == "nije zapoceto" && difference.TotalDays > 5)
            {
                _accommodationRenovationService.Delete(SelectedRenovation);
                renovations.Remove(SelectedRenovation);
                ToastNotificationService.ShowInformation("Uspiješno otkazano renoviranje");
            }
            else ToastNotificationService.ShowInformation("Nije moguća operacija brisanja");
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }

    }
}
