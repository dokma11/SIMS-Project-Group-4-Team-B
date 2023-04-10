using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model
{
    public class TourReservation : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int UserId { get; set; }
        public int GuestNumber { get; set; }

        public DateTime ReservationTime { get; set; }
        public bool ShouldConfirmParticipation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public TourReservation() {
            ReservationTime= DateTime.Now;
        }
        public TourReservation(int id, int tourId, int userId, int guestNumber)
        {
            Id = id;
            TourId = tourId;
            UserId = userId;
            GuestNumber = guestNumber;
            ReservationTime = DateTime.Now;
            ShouldConfirmParticipation = false;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                UserId.ToString(),
                GuestNumber.ToString(), 
                ReservationTime.ToString(),
                ShouldConfirmParticipation.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            UserId = int.Parse(values[2]);
            GuestNumber = int.Parse(values[3]);
            ReservationTime = DateTime.Parse(values[4]);
            ShouldConfirmParticipation = bool.Parse(values[5]);
        }

    }
}
