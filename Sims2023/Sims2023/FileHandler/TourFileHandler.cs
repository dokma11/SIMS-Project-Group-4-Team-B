using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.FileHandler
{
    public class TourFileHandler
    {
        private List<Tour> _tours;
        private readonly Serializer<Tour> _serializer;
        private const string FilePath = "../../../Resources/Data/tours.csv";

        public TourFileHandler()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }

        public Tour GetById(int id)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(t => t.Id == id);
        }

        public List<Tour> Load()
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours;
        }

        public void Save(List<Tour> tours)
        {
            _serializer.ToCSV(FilePath, tours);
        }
    }
}
