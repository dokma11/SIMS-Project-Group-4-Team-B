using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sims2023.Model
{
    public class Location: ISerializable, INotifyPropertyChanged
    {
        public int id { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public Location() { }
        public Location(int id, string city, string country) 
        {
            this.id = id;
            this.city = city;
            this.country = country;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues = { id.ToString(), city, country}; 
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            city = values[1];
            country = values[2];
        }
    }
}
