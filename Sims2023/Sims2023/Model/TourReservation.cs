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
    public class TourReservation : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int UserId { get; set; }
        
        public int GuestNumber { get; set; }

        public bool ShouldConfirmParticipation { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public TourReservation() { }
        public TourReservation(int id, int tourId, int userId, int guestNumber)
        {
            Id = id;
            TourId = tourId;
            UserId = userId;
            GuestNumber = guestNumber;
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
                ShouldConfirmParticipation.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            GuestNumber = Convert.ToInt32(values[3]);
            ShouldConfirmParticipation = bool.Parse(values[4]);

        }

    }
}
