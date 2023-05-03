using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.Models
{
    public class YearlyStatistics
    {
        public int Year { get; set; }
        public int NumReservations { get; set; }
        public int NumCanceled { get; set; }
        public int NumRescheduled { get; set; }
        public int NumRenovationRecommendation{ get; set; }
    }
}
