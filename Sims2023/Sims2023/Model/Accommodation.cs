using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model
{
    public class Accommodation : Serializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set;  }
        public string City { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinDays { get; set; }
        public int CancelDays { get; set; }
        public List<string> ImageUrls { get; set; }

        string ImageUrl { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public Accommodation()
        {
            ImageUrls = new List<string>();
        }
        public Accommodation(int id,string name, string city, string country, string type, int maxGuests, int minDays, int cancelDays, string ImageUrls)
        {
            Id = id;
            Name = name;
            City = city;
            Country = country;
            Type = type;
            MaxGuests = maxGuests;
            MinDays = minDays;
            CancelDays = cancelDays;
            ImageUrl = ImageUrls;

        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                City,
                Country,
                Type,
                MaxGuests.ToString(),
                MinDays.ToString(),
                CancelDays.ToString(),
                ImageUrl

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            City = values[2];

            Country = values[3];
            Type = values[4];
            MaxGuests = int.Parse(values[5]);
            MinDays = int.Parse(values[6]);
            CancelDays = int.Parse(values[7]);
            ImageUrl = values[8];

        }



    }

 
}
