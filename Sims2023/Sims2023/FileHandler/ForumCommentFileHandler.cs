using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.FileHandler
{
    public class ForumCommentFileHandler
    {
        private List<ForumComment> _forumComments;
        private readonly Serializer<ForumComment> _serializer;
        private const string FilePath = "../../../Resources/Data/forumComments.csv";

        public ForumCommentFileHandler()
        {
            _serializer = new Serializer<ForumComment>();
            _forumComments = _serializer.FromCSV(FilePath);
        }

        public ForumComment GetById(int id)
        {
            _forumComments = _serializer.FromCSV(FilePath);
            return _forumComments.FirstOrDefault(comment => comment.Id == id);
        }

        public List<ForumComment> Load()
        {
            _forumComments = _serializer.FromCSV(FilePath);
            return _forumComments;
        }

        public void Save(List<ForumComment> forumComments)
        {
            _serializer.ToCSV(FilePath, forumComments);
        }

    }
}
