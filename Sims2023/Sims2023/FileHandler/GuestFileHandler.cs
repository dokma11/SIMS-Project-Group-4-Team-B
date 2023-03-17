using Sims2023.Model;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repository
{
    public class GuestFileHandler
    {
        private const string StoragePath = "../../../Resources/Data/guests.csv";

        private List<Guest> guests;

        private Serializer<Guest> _serializer;


        public GuestFileHandler()
        {
            _serializer = new Serializer<Guest>();
        }

        public List<Guest> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Guest> guest)
        {
            _serializer.ToCSV(StoragePath, guest);
        }

        public Guest GetById(int id)
        {
            guests = _serializer.FromCSV(StoragePath);
            return guests.FirstOrDefault(t => t.Id == id);
        }
    }
}
