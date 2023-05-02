using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.Models
{
    public class AccommodationRenovation : INotifyPropertyChanged,ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Status { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public AccommodationRenovation() { }
        public AccommodationRenovation(int id, Accommodation accommodation, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Accommodation = accommodation;
            StartDate = startDate;
            EndDate = endDate;
            Status = "nije zapoceto";
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                Status,
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation accommodation = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            AccommodationService accommodationService = new();
            Accommodation = accommodationService.GetById(accommodation.Id);
            StartDate = DateTime.Parse(values[2]);
            EndDate = DateTime.Parse(values[3]);
            Status = values[4];
        }
  
    }

}
