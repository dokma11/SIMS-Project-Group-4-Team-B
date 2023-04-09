using Sims2023.Model;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repository
{
    class AccommodationFileHandler
    {

        private const string StoragePath = "../../../Resources/Data/accommodations.csv";

        private Serializer<Accommodation> _serializer;
        private List<Accommodation> _accommodations;

        public Accommodation GetById(int id)
        {
            _accommodations = _serializer.FromCSV(StoragePath);
            return _accommodations.FirstOrDefault(u => u.Id == id);
        }

        public AccommodationFileHandler()
        {
            _serializer = new Serializer<Accommodation>();
        }

        public List<Accommodation> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Accommodation> accommodations)
        {
            _serializer.ToCSV(StoragePath, accommodations);
        }

    }
}
