using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Serialization;

namespace Sims2023.FileHandler
{
    public class ComplexTourRequestFileHandler
    {
        private List<ComplexTourRequest> _complexRequests;
        private readonly Serializer<ComplexTourRequest> _serializer;
        private const string FilePath = "../../../Resources/Data/complexTourRequests.csv";

        public ComplexTourRequestFileHandler()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _complexRequests = _serializer.FromCSV(FilePath);
        }

        public ComplexTourRequest GetById(int id)
        {
            _complexRequests = _serializer.FromCSV(FilePath);
            return _complexRequests.FirstOrDefault(r => r.Id == id);
        }

        public List<ComplexTourRequest> Load()
        {
            _complexRequests = _serializer.FromCSV(FilePath);
            return _complexRequests;
        }

        public void Save(List<ComplexTourRequest> complexRequests)
        {
            _serializer.ToCSV(FilePath, complexRequests);
        }
    }
}
