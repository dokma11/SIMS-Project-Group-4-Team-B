using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.ComponentModel;

namespace Sims2023.Model
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


        public GuestGrade() { }
        public GuestGrade(int id, Accommodation accommodation, User user, int clean, int respect, int communication, string comment)
        {
            Id = id;
            Accommodation = accommodation;
            Guest = user;
            Cleanliness = clean;
            RespectRules = respect;
            Communication = communication;
            Comment = comment;
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
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation accommodation = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            AccommodationController accommodationController = new();
            Accommodation = accommodationController.GetById(accommodation.Id);
            User guest = new()
            {
                Id = Convert.ToInt32(values[2])
            };
            UserService userService = new();
            Guest = userService.GetById(guest.Id);
            Cleanliness = Convert.ToInt32(values[3]);
            RespectRules = Convert.ToInt32(values[4]);
            Communication = Convert.ToInt32(values[5]);
            Comment = values[6];
        }



    }
}
