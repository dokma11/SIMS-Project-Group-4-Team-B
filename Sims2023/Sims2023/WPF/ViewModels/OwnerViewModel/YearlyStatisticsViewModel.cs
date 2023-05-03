using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class YearlyStatisticsViewModel
    {

        public ObservableCollection<YearlyStatistics> Statistics { get; set; }

        public AccommodationStatisticsFileHandler _accommodationFileHandler;

        public YearlyStatistics SelectedYear { get; set; }

        public YearlyStatisticsViewModel(Accommodation selectedAccommodation)
        {

        }
    }
}
