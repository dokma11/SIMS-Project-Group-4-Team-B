using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.ComponentModel;

namespace Sims2023.Domain.Models
{
    public class AccommodationGrade : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public User Guest { get; set; }
        public int Cleanliness { get; set; }
        public int Comfort { get; set; }
        public int Location { get; set; }
        public int Owner { get; set; }
        public int ValueForMoney { get; set; }
        public string Comment { get; set; }
        public string CurrentAccommodationState { get; set; }
        public string RenovationUrgency { get; set; }
        public DateTime ReservationStartDate { get; set; }

        public AccommodationGrade() { }
        public AccommodationGrade(int id, int cleanliness, int comfort, int location, int owner, int valueForMoney, string comment, Accommodation accommodation, User user, string currentAccommodationState, string renovationUrgency, DateTime reservationStartDate)
        {
            Id = id;
            Cleanliness = cleanliness;
            Comfort = comfort;
            Location = location;
            Owner = owner;
            ValueForMoney = valueForMoney;
            Comment = comment;
            Accommodation = accommodation;
            Guest = user;
            CurrentAccommodationState = currentAccommodationState;
            RenovationUrgency = renovationUrgency;
            ReservationStartDate = reservationStartDate;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                Guest.Id.ToString(),
                Cleanliness.ToString(),
                Comfort.ToString(),
                Location.ToString(),
                Owner.ToString(),
                ValueForMoney.ToString(),
                Comment,
                CurrentAccommodationState,
                RenovationUrgency,
                ReservationStartDate.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            Guest = new()
            {
                Id = Convert.ToInt32(values[2])
            };
            Cleanliness = Convert.ToInt32(values[3]);
            Comfort = Convert.ToInt32(values[4]);
            Location = Convert.ToInt32(values[5]);
            Owner = Convert.ToInt32(values[6]);
            ValueForMoney = Convert.ToInt32(values[7]);
            Comment = values[8];
            CurrentAccommodationState = values[9];
            RenovationUrgency = values[10];
            ReservationStartDate = DateTime.Parse(values[11]);
        }
    }
}
