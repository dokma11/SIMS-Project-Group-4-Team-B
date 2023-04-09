using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Model;
using Sims2023.Serialization;
using System;
using System.ComponentModel;

namespace Sims2023.Domain.Models
{
    public class Voucher : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Tour Tour { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expired { get; set; }
        public string AdditionalComment { get; set; }
        public bool IsUsed { get; set; }
        public Voucher() { }
        public Voucher(int id, User user, Tour tour, DateTime created, DateTime expired, string additionalComment, bool isUsed)
        {
            Id = id;
            User = user;
            Tour = tour;
            Created = created;
            Expired = expired;
            AdditionalComment = additionalComment;
            IsUsed = isUsed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User user = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            UserController userService = new();
            User = userService.GetById(user.Id);
            Tour tour = new()
            {
                Id = Convert.ToInt32(values[2])
            };
            TourService tourService = new();
            Tour = tourService.GetById(tour.Id);
            Created = DateTime.Parse(values[3]);
            Expired = DateTime.Parse(values[4]);
            AdditionalComment = values[5];
            IsUsed = bool.Parse(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                User.Id.ToString(),
                Tour.Id.ToString(),
                Created.ToString(),
                Expired.ToString(),
                AdditionalComment,
                IsUsed.ToString()
            };
            return csvValues;
        }
    }
}
