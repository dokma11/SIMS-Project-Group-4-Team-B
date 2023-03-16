using Sims2023.Model;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repository
{
    internal class TourReservationFileHandler
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