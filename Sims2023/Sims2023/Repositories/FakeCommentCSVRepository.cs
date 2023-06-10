using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repositories
{
    public class FakeCommentCSVRepository : IFakeCommentCSVRepository
    {
        private FakeCommentFileHandler _fileHandler;
        private List<FakeComment> _fakeComments;
        public FakeCommentCSVRepository()
        {
            _fileHandler = new FakeCommentFileHandler();
            _fakeComments = _fileHandler.Load();
        }

        public List<FakeComment> GetAll()
        {
            return _fakeComments;
        }

        public void Add(FakeComment comment)
        {
            _fakeComments.Add(comment);
            _fileHandler.Save(_fakeComments);
        }

    }
}
