using Sims2023.Model;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repository
{
    class AccommodationReservationFileHandler
    {
        private const string StoragePath = "../../../Resources/Data/reservations.csv";

        private Serializer<AccommodationReservation> _serializer;

        public AccommodationReservationFileHandler()
        {
            _serializer = new Serializer<AccommodationReservation>();
        }

        public List<AccommodationReservation> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<AccommodationReservation> reservations)
        {
            _serializer.ToCSV(StoragePath, reservations);
        }
    }
}
