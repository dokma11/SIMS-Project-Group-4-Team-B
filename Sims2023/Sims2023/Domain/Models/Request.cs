using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.ComponentModel;

namespace Sims2023.Domain.Models
{
    public enum RequestsLanguage { Serbian, English, German, French, Spanish, Italian, Chinese, Japanese }
    public enum RequestsState { OnHold, Invalid, Accepted }

    public class Request: ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public RequestsLanguage Language { get; set; }
        public int GuestNumber { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public RequestsState State { get; set; }
        public User Guest { get; set; }
       

        public Request() { }

        public Request(Location location, string description, RequestsLanguage language, int guestNumber, DateTime start, DateTime end,User guest)
        {
           
            Location = location;
            Description = description;
            Language = language;
            GuestNumber = guestNumber;
            Start = start;
            End = end;
            State = RequestsState.OnHold;
            Guest = guest;
           
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Location.Id.ToString(),
                Description,
                Language.ToString(),
                GuestNumber.ToString(),
                Start.ToString(),
                End.ToString(),
                State.ToString(),
                Guest.Id.ToString(),
               
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            LocationService locationService = new();
            Location = locationService.GetById(Convert.ToInt32(values[1]));
            Description = values[2];
            Language = (RequestsLanguage)Enum.Parse(typeof(RequestsLanguage), values[3]);
            GuestNumber = Convert.ToInt32(values[4]);
            Start = DateTime.Parse(values[5]);
            End = DateTime.Parse(values[6]);
            State = (RequestsState)Enum.Parse(typeof(RequestsState), values[7]);
            UserService userService = new();
            Guest = userService.GetById(Convert.ToInt32(values[8]));
            
        }
    }
}
