using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.FileHandler
{
    public class RequestFileHandler
    {
        private List<Request> _requests;
        private readonly Serializer<Request> _serializer;
        private const string FilePath = "../../../Resources/Data/requests.csv";

        public RequestFileHandler()
        {
            _serializer = new Serializer<Request>();
            _requests = _serializer.FromCSV(FilePath);
        }

        public Request GetById(int id)
        {
            _requests = _serializer.FromCSV(FilePath);
            return _requests.FirstOrDefault(r => r.Id == id);
        }

        public List<Request> Load()
        {
            _requests = _serializer.FromCSV(FilePath);
            return _requests;
        }

        public void Save(List<Request> requests)
        {
            _serializer.ToCSV(FilePath, requests);
        }
    }
}
