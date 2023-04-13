using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.Models
{
    public class AccommodationStay
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AccommodationStay() { }
        public AccommodationStay(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

    }
}
