using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.FileHandler
{
    class AccommodationRenovationFileHandler
    {
        private const string StoragePath = "../../../Resources/Data/accommodationRenovations.csv";
        private Serializer<AccommodationRenovation> _serializer;


        public AccommodationRenovationFileHandler()
        {
            _serializer = new Serializer<AccommodationRenovation>();
        }

        public List<AccommodationRenovation> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<AccommodationRenovation> renovations)
        {
            _serializer.ToCSV(StoragePath, renovations);
        }
    }
}
