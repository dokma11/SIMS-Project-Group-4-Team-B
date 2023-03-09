using Sims2023.Model;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repository
{
    internal class AccommodationLocationRepository
    {

        private const string StoragePath = "../../../Resources/Data/accommodationLocations.csv";

        private Serializer<AccommodationLocation> _serializer;


        public AccommodationLocationRepository()
        {
            _serializer = new Serializer<AccommodationLocation>();
        }

        public List<AccommodationLocation> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<AccommodationLocation> accommodationLocations)
        {
            _serializer.ToCSV(StoragePath, accommodationLocations);
        }
    }
}
