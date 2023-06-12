using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.FileHandler
{
    public class TourRequestFileHandler
    {
        private List<TourRequest> _requests;
        private readonly Serializer<TourRequest> _serializer;
        private const string FilePath = "../../../Resources/Data/tourRequests.csv";

        public TourRequestFileHandler()
        {
            _serializer = new Serializer<TourRequest>();
            _requests = _serializer.FromCSV(FilePath);
        }

        public TourRequest GetById(int id)
        {
            _requests = _serializer.FromCSV(FilePath);
            return _requests.FirstOrDefault(r => r.Id == id);
        }

        public List<TourRequest> Load()
        {
            _requests = _serializer.FromCSV(FilePath);
            return _requests;
        }

        public void Save(List<TourRequest> requests)
        {
            _serializer.ToCSV(FilePath, requests);
        }
    }
}
