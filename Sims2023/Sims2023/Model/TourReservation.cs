using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.ComponentModel;

namespace Sims2023.Model
{
    public class TourReservation : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Tour Tour { get; set; }
        public User User { get; set; }
        public int GuestNumber { get; set; }
        public bool ConfirmedParticipation { get; set; }
        public bool UsedVoucher { get; set; }
        //will delete later on 
        public bool ShouldConfirmParticipation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public TourReservation() { }
        public TourReservation(int id, Tour tour, User user, int guestNumber, bool confirmedParticipation, bool usedVoucher)
        {
            Id = id;
            Tour = tour;
            User = user;
            GuestNumber = guestNumber;
            ConfirmedParticipation = confirmedParticipation;
            UsedVoucher = usedVoucher;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Tour.Id.ToString(),
                User.Id.ToString(),
                GuestNumber.ToString(),
                ConfirmedParticipation.ToString(),
                UsedVoucher.ToString(),
                //will delete later on
                ShouldConfirmParticipation.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourService tourService = new();
            Tour = tourService.GetById(Convert.ToInt32(values[1]));
            UserService userService = new();
            User = userService.GetById(Convert.ToInt32(values[2]));
            GuestNumber = int.Parse(values[3]);
            ConfirmedParticipation = bool.Parse(values[4]);
            UsedVoucher = bool.Parse(values[5]);
            //will delete later on
            ShouldConfirmParticipation = bool.Parse(values[6]);
        }

    }
}
