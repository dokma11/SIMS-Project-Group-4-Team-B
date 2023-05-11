using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Serialization;

namespace Sims2023.FileHandler
{
    public class TourNotificationFileHandler
    {
        private List<TourNotification> _tourNotifications;
        private readonly Serializer<TourNotification> _serializer;
        private const string FilePath = "../../../Resources/Data/tourNotifications.csv";

        public TourNotificationFileHandler()
        {
            _serializer = new Serializer<TourNotification>();
            _tourNotifications = _serializer.FromCSV(FilePath);
        }

        public TourNotification GetById(int id)
        {
            _tourNotifications = _serializer.FromCSV(FilePath);
            return _tourNotifications.FirstOrDefault(t => t.Id == id);
        }

        public List<TourNotification> Load()
        {
            _tourNotifications = _serializer.FromCSV(FilePath);
            return _tourNotifications;
        }

        public void Save(List<TourNotification> tourNotifications)
        {
            _serializer.ToCSV(FilePath, tourNotifications);
        }
    }
}
