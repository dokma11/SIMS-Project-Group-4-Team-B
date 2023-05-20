using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.ComponentModel;

namespace Sims2023.Domain.Models
{
    public class TourReservation : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Tour Tour { get; set; }
        public User User { get; set; }
        public int GuestNumber { get; set; }
        public DateTime ReservationTime { get; set; }
        //needed for counting vaouchers in the year of reservation
        public bool ConfirmedParticipation { get; set; }
        public bool UsedVoucher { get; set; }
        //will delete later on 
        public bool ShouldConfirmParticipation { get; set; }

        public TourReservation()
        {
            ReservationTime = DateTime.Now;
        }

        public TourReservation(Tour tour, User user, int guestNumber)
        {
            Tour = tour;
            User = user;
            GuestNumber = guestNumber;
            ReservationTime = DateTime.Now;
        }

        public TourReservation(int id, Tour tour, User user, int guestNumber, bool confirmedParticipation, bool usedVoucher)
        {
            Id = id;
            Tour = tour;
            User = user;
            GuestNumber = guestNumber;
            ReservationTime = DateTime.Now;
            ShouldConfirmParticipation = false;
            ConfirmedParticipation = confirmedParticipation;
            UsedVoucher = usedVoucher;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Tour.Id.ToString(),
                User.Id.ToString(),
                GuestNumber.ToString(),
                ReservationTime.ToString(),
                ConfirmedParticipation.ToString(),
                UsedVoucher.ToString(),
                ShouldConfirmParticipation.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Tour = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            User = new()
            {
                Id = Convert.ToInt32(values[2])
            };
            GuestNumber = int.Parse(values[3]);
            ReservationTime = DateTime.Parse(values[4]);
            ConfirmedParticipation = bool.Parse(values[5]);
            UsedVoucher = bool.Parse(values[6]);
            ShouldConfirmParticipation = bool.Parse(values[7]);
        }
    }
}
