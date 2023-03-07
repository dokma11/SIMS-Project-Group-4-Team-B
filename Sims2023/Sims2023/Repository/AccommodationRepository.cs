using Sims2023.Model;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repository
{
    class AccommodationRepository
    {

        private const string StoragePath = "../../Data/accommodations.csv";

        private Serializer<Accommodation> _serializer;


        public AccommodationRepository()
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
