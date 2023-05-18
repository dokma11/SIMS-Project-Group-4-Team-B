using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.Domain.Models
{
    public class AccommodationCancellation : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public User Guest { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }
        public bool Notified { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public AccommodationCancellation() { }
        public AccommodationCancellation(int id, User guest, Accommodation accommodation, DateTime startDate, DateTime endDate, int numberOfGuests, bool notified)
        {
            Id = id;
            Guest = guest;
            Accommodation = accommodation;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = numberOfGuests;
            Notified = notified;
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
                Notified.ToString()
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
            UserService userController = new();
            Guest = userController.GetById(guest.Id);
            Accommodation accommodation = new()
            {
                Id = Convert.ToInt32(values[2])
            };
            AccommodationService accommodationController = new();
            Accommodation = accommodationController.GetById(accommodation.Id);
            StartDate = DateTime.Parse(values[3]);
            EndDate = DateTime.Parse(values[4]);
            NumberOfDays = Convert.ToInt32(values[5]);
            Notified = Convert.ToBoolean(values[6]);
        }
    }
}
