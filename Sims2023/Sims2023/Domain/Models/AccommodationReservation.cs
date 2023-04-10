using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Serialization;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Sims2023.Domain.Models
{
    public class AccommodationReservation : ISerializable, INotifyPropertyChanged
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public int Id { get; set; }
        public User Guest { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }

        public bool Graded { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public AccommodationReservation() { }
        public AccommodationReservation(int id, User guest, Accommodation accommodation, DateTime startDate, DateTime endDate, int numberOfGuests, bool graded)
        {
            Id = id;
            Guest = guest;
            Accommodation = accommodation;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = numberOfGuests;
            Graded = graded;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Guest.Id.ToString(),
                Accommodation.Id.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                NumberOfDays.ToString(),
                Graded.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User guest = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            UserService userService = new();
            Guest = userService.GetById(guest.Id);
            Accommodation accommodation = new()
            {
                Id = Convert.ToInt32(values[2])
            };
            AccommodationController accommodationController = new();
            Accommodation = accommodationController.GetById(accommodation.Id);
            StartDate = DateTime.Parse(values[3]);
            EndDate = DateTime.Parse(values[4]);
            NumberOfDays = Convert.ToInt32(values[5]);
            Graded = Convert.ToBoolean(values[6]);
        }
    }
}