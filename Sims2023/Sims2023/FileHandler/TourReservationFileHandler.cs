using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System.Collections.Generic;

namespace Sims2023.FileHandler
{
    public class TourReservationFileHandler
    {
        private const string StoragePath = "../../../Resources/Data/tourReservations.csv";

        private Serializer<TourReservation> _serializer;

        public TourReservationFileHandler()
        {
            _serializer = new Serializer<TourReservation>();
        }

        public List<TourReservation> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<TourReservation> reservations)
        {
            _serializer.ToCSV(StoragePath, reservations);
        }
    }
}
