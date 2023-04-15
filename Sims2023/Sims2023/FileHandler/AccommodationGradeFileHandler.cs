using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.FileHandler
{
    class AccommodationGradeFileHandler
    {
            private const string StoragePath = "../../../Resources/Data/accommodationGrades.csv";

            private Serializer<AccommodationGrade> _serializer;

            public AccommodationGradeFileHandler()
            {
                _serializer = new Serializer<AccommodationGrade>();
            }

            public List<AccommodationGrade> Load()
            {
                return _serializer.FromCSV(StoragePath);
            }

            public void Save(List<AccommodationGrade> accommodationGrades)
            {
                _serializer.ToCSV(StoragePath, accommodationGrades);
            }

    }
}

