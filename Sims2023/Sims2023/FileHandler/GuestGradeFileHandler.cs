using Sims2023.Model;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repository
{
    class GuestGradeFileHandler
    {
        private const string StoragePath = "../../../Resources/Data/guestGrades.csv";
        private Serializer<GuestGrade> _serializer;


        public GuestGradeFileHandler()
        {
            _serializer = new Serializer<GuestGrade>();
        }

        public List<GuestGrade> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<GuestGrade> grades)
        {
            _serializer.ToCSV(StoragePath, grades);
        }
    }
}
