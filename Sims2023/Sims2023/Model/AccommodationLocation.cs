using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model
{
    public class AccommodationLocation : Serializable, INotifyPropertyChanged
    {

        public int id { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public AccommodationLocation() { }
        public AccommodationLocation(int Id, string City, string Country)
        {
            id = Id;
            city = City;
            country = Country;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues = { id.ToString(), city, country };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            city = values[1];
            country = values[2];
        }

        public string isVaild(AccommodationLocation a)
        {
            if (string.IsNullOrEmpty(a.city) || string.IsNullOrEmpty(a.country))
                return "morate popuniti sve podatke";
            return null;
        }
    }
}