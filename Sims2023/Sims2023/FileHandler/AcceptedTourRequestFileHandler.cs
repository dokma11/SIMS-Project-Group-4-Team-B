using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Serialization;

namespace Sims2023.FileHandler
{
    public class AcceptedTourRequestFileHandler
    {
        private List<AcceptedTourRequest> _acceptedTourRequests;
        private readonly Serializer<AcceptedTourRequest> _serializer;
        private const string FilePath = "../../../Resources/Data/acceptedTourRequests.csv";

        public AcceptedTourRequestFileHandler()
        {
            _serializer = new Serializer<AcceptedTourRequest>();
            _acceptedTourRequests = _serializer.FromCSV(FilePath);
        }

        public AcceptedTourRequest GetById(int id)
        {
            _acceptedTourRequests = _serializer.FromCSV(FilePath);
            return _acceptedTourRequests.FirstOrDefault(t => t.Id == id);
        }

        public List<AcceptedTourRequest> Load()
        {
            _acceptedTourRequests = _serializer.FromCSV(FilePath);
            return _acceptedTourRequests;
        }

        public void Save(List<AcceptedTourRequest> acceptedTourRequests)
        {
            _serializer.ToCSV(FilePath, acceptedTourRequests);
        }
    }
}
