using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model
{
    public class AccommodationReservation : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }

        public int GuestId { get; set; }
        public int AccommodationId { get; set; }

        public string AccommodationName { get; set; }
        public string AccommodationCity { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public AccommodationReservation() { }
        public AccommodationReservation(int id,int guestid, int accommodationId, DateTime startDate, DateTime endDate, int numberOfGuests)
        {
            Id = id;
            GuestId = guestid;
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = numberOfGuests;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                GuestId.ToString(),
                AccommodationId.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                NumberOfDays.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            AccommodationId = Convert.ToInt32(values[2]);
            StartDate = DateTime.Parse(values[3]);
            EndDate = DateTime.Parse(values[4]);
            NumberOfDays = Convert.ToInt32(values[5]);

        }
    }
}