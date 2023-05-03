using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.Models
{
    public class YearlyStatistics : INotifyPropertyChanged
    {
        public string Year { get; set; }
        public int NumReservations { get; set; }
        public int NumCanceled { get; set; }
        public int NumRescheduled { get; set; }
        public int NumRenovationRecommendation{ get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
