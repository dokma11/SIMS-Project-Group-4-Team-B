using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.FileHandler
{
    class FakeCommentFileHandler
    {
        private const string StoragePath = "../../../Resources/Data/fakeComments.csv";
        private Serializer<FakeComment> _serializer;


        public FakeCommentFileHandler()
        {
            _serializer = new Serializer<FakeComment>();
        }

        public List<FakeComment> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<FakeComment> comments)
        {
            _serializer.ToCSV(StoragePath, comments);
        }
    }
}
