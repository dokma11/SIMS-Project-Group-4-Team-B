using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.Models
{
    public class AccommodationRenovation : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public AccommodationRenovation() { }
        public AccommodationRenovation(int id, User guest, Accommodation accommodation, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Accommodation = accommodation;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),             
            };
            return csvValues;
        }

    }

}
