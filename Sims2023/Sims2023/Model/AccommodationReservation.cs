using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Sims2023.Model
{
    public class AccommodationReservation : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public AccommodationReservation() { }
        public AccommodationReservation(int id, int accommodationId, DateTime startDate, DateTime endDate, int numberOfGuests)
        {
            Id = id;
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfGuests = numberOfGuests;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                NumberOfGuests.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            StartDate=DateTime.Parse(values[2]);
            EndDate=DateTime.Parse(values[3]);
            NumberOfGuests = int.Parse(values[4]);

        }

    }
}