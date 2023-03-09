using Sims2023.Model;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repository
{
    public class TourRepository
    {
        private List<Tour> _tours;
        private readonly Serializer<Tour> _serializer;
        private const string FilePath = "../../../Resources/Data/tours.csv";

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }

        public Tour GetById(int id)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(t => t.id == id);
        }

        public List<Tour> Load() 
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours;
        }

        public void Save(List<Tour> tours) 
        {
            _serializer.ToCSV(FilePath, tours);
        }
    }
}
