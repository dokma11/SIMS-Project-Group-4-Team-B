using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Application.Services;
using Sims2023.Serialization;

namespace Sims2023.Domain.Models
{
    public enum NotificationType { TourStarted, AcceptedTourRequest, MatchedTourRequestsLocation, MatchedTourRequestsLanguage }
    public class TourNotification: ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Tour Tour { get; set; }
        public User Guest { get; set; }
        public NotificationType Type { get; set; }
        public bool IsNotified { get; set; }
        public TourNotification() { }
        public TourNotification(Tour tour, User guest, NotificationType type)
        {
            
            Tour = tour;
            Guest = guest;
            Type = type;    
            IsNotified = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Tour.Id.ToString(),
                Guest.Id.ToString(),
                Type.ToString(),
                IsNotified.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourService tourService = new();
            Tour =tourService.GetById(Convert.ToInt32(values[1]));
            UserService tourUserService = new();
            Guest = tourUserService.GetById(Convert.ToInt32(values[2]));
            Type = (NotificationType)Enum.Parse(typeof(NotificationType), values[3]);
            IsNotified = Convert.ToBoolean(values[4]);
        }
    }
}
