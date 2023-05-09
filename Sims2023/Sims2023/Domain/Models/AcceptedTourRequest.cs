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
    public class AcceptedTourRequest: ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Tour Tour { get; set; }
        public Request Request { get; set; }
        public bool IsNotified { get; set; }
        public AcceptedTourRequest() { }
        public AcceptedTourRequest(Tour tour, Request request)
        {
            
            Tour = tour;
            Request = request;
            IsNotified = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Tour.Id.ToString(),
                Request.Id.ToString(),
                IsNotified.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourService tourService = new();
            Tour =tourService.GetById(Convert.ToInt32(values[1]));
            RequestService requestService = new();
            Request =requestService.GetById(Convert.ToInt32(values[2]));
            IsNotified = Convert.ToBoolean(values[3]);
        }
    }
}
