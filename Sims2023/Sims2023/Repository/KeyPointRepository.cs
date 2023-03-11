using Sims2023.Model;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repository
{
    public class KeyPointRepository
    {
        private List<KeyPoint> _keyPoints;
        private readonly Serializer<KeyPoint> _serializer;
        private const string FilePath = "../../../Resources/Data/keyPoints.csv";

        public KeyPointRepository()
        {
            _serializer = new Serializer<KeyPoint>();
            _keyPoints = _serializer.FromCSV(FilePath);
        }

        public KeyPoint GetById(int id)
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            return _keyPoints.FirstOrDefault(k => k.Id == id);
        }

        public List<KeyPoint> Load()
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            return _keyPoints;
        }

        public void Save(List<KeyPoint> keyPoints)
        {
            _serializer.ToCSV(FilePath, keyPoints);
        }
    }
}
