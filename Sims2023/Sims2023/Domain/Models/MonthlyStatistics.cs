using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.Models
{
    public class MonthlyStatistics
    {
        public string Month { get; set; }
        public int NumReservations { get; set; }
        public int NumCanceled { get; set; }
        public int NumRescheduled { get; set; }
        public int NumRenovationRecommendation { get; set; }
    }
}
