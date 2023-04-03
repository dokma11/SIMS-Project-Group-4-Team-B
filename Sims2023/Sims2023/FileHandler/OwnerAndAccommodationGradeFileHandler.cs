using Sims2023.Model;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.FileHandler
{
     class OwnerAndAccommodationGradeFileHandler
    {
        private const string StoragePath = "../../../Resources/Data/ownerAndAccommodationGrades.csv";

        private Serializer<OwnerAndAccommodationGrade> _serializer;


        public OwnerAndAccommodationGradeFileHandler()
        {
            _serializer = new Serializer<OwnerAndAccommodationGrade>();
        }

        public List<OwnerAndAccommodationGrade> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<OwnerAndAccommodationGrade> grades)
        {
            _serializer.ToCSV(StoragePath, grades);
        }

    }
}
