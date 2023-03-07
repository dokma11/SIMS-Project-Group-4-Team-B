using Sims2023.Model;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repository
{
    public class LocationRepository
    {
        public List<Location> _locations;
        private readonly Serializer<Location> _serializer;
        private const string FilePath = "../../../Resources/Data/locations.csv";

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
        }

        public Location GetById(int id)
        {
            _locations = _serializer.FromCSV(FilePath);
            return _locations.FirstOrDefault(t => t.Id == id);
        }

        public List<Location> Load()
        {
            _locations = _serializer.FromCSV(FilePath);
            return _locations;
        }

        public void Save(List<Location> locations)
        {
            _serializer.ToCSV(FilePath, locations);
        }
    }
}
