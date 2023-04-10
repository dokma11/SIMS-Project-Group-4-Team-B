using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static Sims2023.Domain.Models.Tour;

namespace Sims2023.Model
{
    public class AccommodationReservationRescheduling : ISerializable, INotifyPropertyChanged
    {
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public int Id { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; }
        public enum RequestStatus { Pending, Approved, Rejected}
        public RequestStatus Status { get; set; }
        public bool Notified { get; set; }
        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public string Comment { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public AccommodationReservationRescheduling() { }
        public AccommodationReservationRescheduling(int id, AccommodationReservation accommodationReservation, RequestStatus status, bool notified, DateTime startDate, DateTime endDate, String comment)
        {
            Id = id;
            AccommodationReservation = accommodationReservation;
            Status = status;
            Notified = notified;
            NewStartDate = startDate;
            NewEndDate = endDate;
            Comment = comment;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationReservation.Id.ToString(),
                Status.ToString(),
                Notified.ToString(),
                NewStartDate.ToString(),
                NewEndDate.ToString(),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationReservation accommodationReservation = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            AccommodationReservationController accommodationReservationController = new();
            AccommodationReservation = accommodationReservationController.GetById(accommodationReservation.Id);
            Status = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[2]);
            Notified = Convert.ToBoolean(values[3]);
            NewStartDate = Convert.ToDateTime(values[4]);
            NewEndDate = Convert.ToDateTime(values[5]);
            Comment = values[6];
        }
    }

}

