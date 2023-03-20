using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model
{
    public class AccommodationLocation : ISerializable, INotifyPropertyChanged
    {

        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public AccommodationLocation() { }
        public AccommodationLocation(int Id, string City, string Country)
        {
            this.Id = Id;
            this.City = City;
            this.Country = Country;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues = 
            { 
                Id.ToString(), 
                City, 
                Country 
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }

        public string IsVaild(AccommodationLocation a)
        {
            if (string.IsNullOrEmpty(a.City) || string.IsNullOrEmpty(a.Country))
                return "morate popuniti sve podatke za grad i drzavu";
            return null;
        }
    }
}