using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.FileHandler
{
     public class AccommodationStatisticsFileHandler
    {
        private const string StoragePath = "../../../Resources/Data/accommodationStatistics.csv";
        private Serializer<AccommodationStatistics> _serializer;


        public AccommodationStatisticsFileHandler()
        {
            _serializer = new Serializer<AccommodationStatistics>();
        }

        public List<AccommodationStatistics> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<AccommodationStatistics> statistics)
        {
            _serializer.ToCSV(StoragePath, statistics);
        }
    }
}
