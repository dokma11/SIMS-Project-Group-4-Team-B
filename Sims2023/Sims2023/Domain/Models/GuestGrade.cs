using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.ComponentModel;

namespace Sims2023.Domain.Models
{
    public class GuestGrade : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public User Guest { get; set; }

        public int Cleanliness { get; set; }
        public int RespectRules { get; set; }
        public int Communication { get; set; }
        public string Comment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public GuestGrade() { }
        public GuestGrade(int id, Accommodation accommodation, User user, int clean, int respect, int communication, string comment, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Accommodation = accommodation;
            Guest = user;
            Cleanliness = clean;
            RespectRules = respect;
            Communication = communication;
            Comment = comment;
            StartDate = startDate;
            EndDate = endDate;
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
                RespectRules.ToString(),
                Communication.ToString(),
                Comment,
                StartDate.ToString(),
                EndDate.ToString()
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
            RespectRules = Convert.ToInt32(values[4]);
            Communication = Convert.ToInt32(values[5]);
            Comment = values[6];
            StartDate = DateTime.Parse(values[7]);
            EndDate = DateTime.Parse(values[8]);
        }
    }
}
