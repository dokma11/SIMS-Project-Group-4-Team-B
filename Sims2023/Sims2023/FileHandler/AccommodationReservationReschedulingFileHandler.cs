using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.FileHandler
{
    public class AccommodationReservationReschedulingFileHandler
    {
        private const string StoragePath = "../../../Resources/Data/accommodationReservationReschedulings.csv";

        private Serializer<AccommodationReservationRescheduling> _serializer;
        private List<AccommodationReservationRescheduling> _accommodationReservationReschedulings;

        public AccommodationReservationRescheduling GetById(int id)
        {
            _accommodationReservationReschedulings = _serializer.FromCSV(StoragePath);
            return _accommodationReservationReschedulings.FirstOrDefault(u => u.Id == id);
        }

        public AccommodationReservationReschedulingFileHandler()
        {
            _serializer = new Serializer<AccommodationReservationRescheduling>();
        }

        public List<AccommodationReservationRescheduling> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<AccommodationReservationRescheduling> reservationReschedulings)
        {
            _serializer.ToCSV(StoragePath, reservationReschedulings);
        }
    }
}
