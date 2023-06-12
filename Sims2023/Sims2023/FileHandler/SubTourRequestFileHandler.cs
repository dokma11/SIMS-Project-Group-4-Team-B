using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Serialization;

namespace Sims2023.FileHandler
{
    public class SubTourRequestFileHandler
    {
        private List<SubTourRequest> _subRequests;
        private readonly Serializer<SubTourRequest> _serializer;
        private const string FilePath = "../../../Resources/Data/subTourRequests.csv";

        public SubTourRequestFileHandler()
        {
            _serializer = new Serializer<SubTourRequest>();
            _subRequests = _serializer.FromCSV(FilePath);
        }

        public SubTourRequest GetById(int id)
        {
            _subRequests = _serializer.FromCSV(FilePath);
            return _subRequests.FirstOrDefault(r => r.Id == id);
        }

        public List<SubTourRequest> Load()
        {
            _subRequests = _serializer.FromCSV(FilePath);
            return _subRequests;
        }

        public void Save(List<SubTourRequest> subRequests)
        {
            _serializer.ToCSV(FilePath, subRequests);
        }
    }
}
