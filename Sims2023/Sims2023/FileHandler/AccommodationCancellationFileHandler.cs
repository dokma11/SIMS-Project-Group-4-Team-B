using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.FileHandler
{
    public class AccommodationCancellationFileHandler
    {
        private const string StoragePath = "../../../Resources/Data/accommodationCancellation.csv";

        private Serializer<AccommodationCancellation> _serializer;

        private List<AccommodationCancellation> _accommodationCancellations;

        public AccommodationCancellationFileHandler()
        {
            _serializer = new Serializer<AccommodationCancellation>();
        }

        public AccommodationCancellation GetById(int id)
        {
            _accommodationCancellations = _serializer.FromCSV(StoragePath);
            return _accommodationCancellations.FirstOrDefault(u => u.Id == id);
        }

        public List<AccommodationCancellation> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<AccommodationCancellation> accommodationCancellations)
        {
            _serializer.ToCSV(StoragePath, accommodationCancellations);
        }
    }
}
