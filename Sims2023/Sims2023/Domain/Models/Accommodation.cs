using Sims2023.Application.Services;
using Sims2023.Serialization;
using Sims2023.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Sims2023.Domain.Models
{
    public class Accommodation : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public Location Location { get; set; }
        public string Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinDays { get; set; }
        public int CancelDays { get; set; }
        public List<string> Imageurls { get; set; }
        public string ImageUrl { get; set; }

        public bool Renovated { get; set; }

        int locid;


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public Accommodation()
        {
            Imageurls = new List<string>();
        }
        public Accommodation(int Id, string Name, Location Location, string Type, int MaxGuests, int MinDays, int CancelDays, List<string> ImageUrls, User Owner)
        {
            this.Id = Id;
            this.Name = Name;
            this.Location = Location;
            this.Type = Type;
            this.MaxGuests = MaxGuests;
            this.MinDays = MinDays;
            this.CancelDays = CancelDays;
            this.Owner = Owner;
            this.Imageurls = ImageUrls;
            this.Renovated = false;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Location.Id.ToString(),
                Type,
                MaxGuests.ToString(),
                MinDays.ToString(),
                CancelDays.ToString(),
                Owner.Id.ToString(),
                string.Join("!", Imageurls),
                Renovated.ToString()
             };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new Location(); // Instantiate Location object
            Location.Id = Convert.ToInt32(values[2]);
            Type = values[3];
            MaxGuests = Convert.ToInt32(values[4]);
            MinDays = Convert.ToInt32(values[5]);
            CancelDays = Convert.ToInt32(values[6]);
            Owner = new User();
            Owner.Id = Convert.ToInt32(values[7]);
            Imageurls = values[8].Split('!').ToList();
            Renovated = Convert.ToBoolean(values[9]);

        }

        public string IsVaild(Accommodation a)
        {
            if (string.IsNullOrEmpty(a.Name) || string.IsNullOrEmpty(a.Type) || string.IsNullOrEmpty(ImageUrl))
                return "morate popuniti sve podatke";

            if (a.CancelDays == -1 || a.MaxGuests == -1 || a.MinDays == -1 || a.Location.Id == -1)
            {
                return "morate popuniti sve podatke";
            }
            return null;
        }
    }


}