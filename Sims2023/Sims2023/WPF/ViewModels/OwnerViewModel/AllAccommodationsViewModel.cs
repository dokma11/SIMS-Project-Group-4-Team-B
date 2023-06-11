using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class AllAccommodationsViewModel
    {
        User owner { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public AccommodationService _accommodationService;

        public Accommodation SelectedAccommodation { get; set; }

        public RelayCommand Schedule { get; set; }
        public RelayCommand Review { get; set; }
        public RelayCommand Statistics { get; set; }

        public AllAccommodationsViewModel(User user)
        {
            Schedule = new RelayCommand(Executed_ScheduleCommand, CanExecute_ScheduleCommand);
            Review = new RelayCommand(Executed_ReviewCommand, CanExecute_ReviewCommand);
            Statistics = new RelayCommand(Executed_StatisticsCommand, CanExecute_StatisticsCommand);
            this.owner = user;
            _accommodationService = new AccommodationService();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetOwnerAccommodations(_accommodationService.GetAllAccommodations(), owner));

        }

        private bool CanExecute_StatisticsCommand(object obj)
        {
            return true;
        }

        private bool CanExecute_ReviewCommand(object obj)
        {
            return true;
        }

        private bool CanExecute_ScheduleCommand(object obj)
        {
            return true;
        }

        public void Executed_ScheduleCommand(object obj)
        {
            if (SelectedAccommodation != null)
            {
                SchedulingRenovationAppointmentsView shedule = new SchedulingRenovationAppointmentsView(SelectedAccommodation);
                FrameManager.Instance.MainFrame.Navigate(shedule);
            }
            else ToastNotificationService.ShowInformation("Niste selektovali nista");
        }

        public void Executed_ReviewCommand(object obj)
        {
            AllRenovationsView allRenovations = new AllRenovationsView(owner);
            FrameManager.Instance.MainFrame.Navigate(allRenovations);
        }


        public void Executed_StatisticsCommand(object obj)
        {
            if (SelectedAccommodation != null)
            {
                YearlyStatisticsView statistics = new YearlyStatisticsView(SelectedAccommodation);
                FrameManager.Instance.MainFrame.Navigate(statistics);
            }
            else ToastNotificationService.ShowInformation("Niste selektovali nista");
        }
    }
}
