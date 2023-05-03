﻿using Sims2023.Application.Services;
using Sims2023.Domain.Models;
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

        public AllAccommodationsViewModel(User user)
        {
            this.owner = user;
            _accommodationService = new AccommodationService();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetOwnerAccommodations(_accommodationService.GetAllAccommodations(), owner));

        }

        public void Shedule_Click()
        {
            if (SelectedAccommodation != null)
            {
                SchedulingRenovationAppointmentsView shedule = new SchedulingRenovationAppointmentsView(SelectedAccommodation);
                FrameManager.Instance.MainFrame.Navigate(shedule);
            }
        }

        public void Review_Click()
        {
            AllRenovationsView allRenovations = new AllRenovationsView(owner);
            FrameManager.Instance.MainFrame.Navigate(allRenovations);
        }

        public void Statistics_Click()
        {
            if (SelectedAccommodation != null)
            {
                YearlyStatisticsView statistics = new YearlyStatisticsView(SelectedAccommodation);
                FrameManager.Instance.MainFrame.Navigate(statistics);
            }
        }
    }
}