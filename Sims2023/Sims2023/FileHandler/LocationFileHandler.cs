using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repository
{
    public class LocationFileHandler
    {
        private List<Location> _locations;
        private readonly Serializer<Location> _serializer;
        private const string FilePath = "../../../Resources/Data/locations.csv";

        public LocationFileHandler()
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
