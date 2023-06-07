using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.FileHandler
{
    public class ForumFileHandler
    {
        private List<Forum> _forums;
        private readonly Serializer<Forum> _serializer;
        private const string FilePath = "../../../Resources/Data/forums.csv";

        public ForumFileHandler()
        {
            _serializer = new Serializer<Forum>();
            _forums = _serializer.FromCSV(FilePath);
        }

        public Forum GetById(int id)
        {
            _forums = _serializer.FromCSV(FilePath);
            return _forums.FirstOrDefault(f => f.Id == id);
        }

        public List<Forum> Load()
        {
            _forums = _serializer.FromCSV(FilePath);
            return _forums;
        }

        public void Save(List<Forum> forums)
        {
            _serializer.ToCSV(FilePath, forums);
        }

    }
}
