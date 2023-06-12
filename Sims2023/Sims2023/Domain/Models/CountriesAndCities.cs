using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sims2023.Domain.Models
{
    public class CountriesAndCities : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string City1 { get; set; }
        public string City2 { get; set; }
        public string City3 { get; set; }
        public string City4 { get; set; }
        public string City5 { get; set; }

        public CountriesAndCities(int id, string countryName, string city1, string city2, string city3, string city4, string city5)
        {
            this.Id = id;
            this.CountryName = countryName;
            City1 = city1;
            City2 = city2;
            City3 = city3;
            City4 = city4;
            City5 = city5;
        }

        public CountriesAndCities() { }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                CountryName,
                City1,
                City2,
                City3,
                City4,
                City5
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            CountryName = values[1];
            City1 = values[2];
            City2 = values[3];
            City3 = values[4];
            City4 = values[5];
            City5 = values[6];
        }


    }


}
