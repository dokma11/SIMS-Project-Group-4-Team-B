using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.FileHandler
{
    public class CountriesAndCitiesFileHandler
    {
        private const string StoragePath = "../../../Resources/Data/countriesAndCities.csv";

        private Serializer<CountriesAndCities> _serializer;
        private List<CountriesAndCities> _accommodations;

        public CountriesAndCities GetById(int id)
        {
            _accommodations = _serializer.FromCSV(StoragePath);
            return _accommodations.FirstOrDefault(u => u.Id == id);
        }

        public CountriesAndCitiesFileHandler()
        {
            _serializer = new Serializer<CountriesAndCities>();
        }

        public List<CountriesAndCities> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<CountriesAndCities> accommodations)
        {
            _serializer.ToCSV(StoragePath, accommodations);
        }
    }
}
