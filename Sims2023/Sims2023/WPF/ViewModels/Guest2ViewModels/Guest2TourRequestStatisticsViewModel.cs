using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    
       
        public class Guest2TourRequestStatisticsViewModel
        {



            public ChartValues<double> AcceptedPercentage { get; set; }
            public ChartValues<double> DeclinedPercentage { get; set; }

            public User User { get; set; }

            private RequestService _requestService;

            private string _selectedValue;

            

            public Guest2TourRequestStatisticsViewModel(User user, string selectedValue)
            {
                // Set the percentage values for the pie chart
                _requestService = new RequestService();
                User = user;

                _selectedValue = selectedValue;
                AcceptedPercentage = new ChartValues<double> { _requestService.GetAcceptedTourRequestsByUser(User).Count() };
                DeclinedPercentage = new ChartValues<double> { _requestService.GetByUser(User).Count() - _requestService.GetAcceptedTourRequestsByUser(User).Count() };
           
            }

            
        }
    
}
