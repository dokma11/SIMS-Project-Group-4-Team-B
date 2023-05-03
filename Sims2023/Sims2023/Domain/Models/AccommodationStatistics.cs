using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Sims2023.Domain.Models
{
    public class AccommodationStatistics : ISerializable, INotifyPropertyChanged
    {
        public Accommodation Accommodation { get; set; }
        public DateTime DateOfEntry { get; set; }
        public bool isCanceled { get; set; }
        public bool isRescheduled { get; set; }
        public bool RenovationRecommendation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public AccommodationStatistics() { }

        public AccommodationStatistics(Accommodation accommodation, DateTime date, bool cancel, bool reschedule, bool recommended) 
        {
            Accommodation = accommodation;
            DateOfEntry = date;
            isCanceled = cancel;
            isRescheduled = reschedule;
            RenovationRecommendation = recommended;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Accommodation.Id.ToString(),
                DateOfEntry.ToString(),
                isCanceled.ToString(),
                isRescheduled.ToString(),
                RenovationRecommendation.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Accommodation accommodation = new()
            {
                Id = Convert.ToInt32(values[0])
            };
            AccommodationService accommodationService = new();
            Accommodation = accommodationService.GetById(accommodation.Id);
            DateOfEntry = DateTime.Parse(values[1]);
            isCanceled = Convert.ToBoolean(values[2]);
            isRescheduled = Convert.ToBoolean(values[3]);
            RenovationRecommendation = Convert.ToBoolean(values[4]);
        }


    }
}
