using Sims2023.Application.Injection;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Application.Services
{
    public class FakeCommentService
    {
        private IFakeCommentCSVRepository _fakeComment;
        private IUserCSVRepository _users;
        private IForumCommentRepository _forum;

        public FakeCommentService()
        {
            _users = Injector.CreateInstance<IUserCSVRepository>();
            _forum = Injector.CreateInstance<IForumCommentRepository>();
            _fakeComment = Injector.CreateInstance<IFakeCommentCSVRepository>();
            FindForeignAtributes();
        }

        private void FindForeignAtributes()
        {
            foreach (var item in GetAll())
            {
                var user = _users.GetById(item.Owner.Id);
                var forum = _forum.GetById(item.Comment.Id);
                if (user != null && forum != null)
                {
                    item.Owner = user;
                    item.Comment = forum;
                }
            }
        }

        public List<FakeComment> GetAll()
        {
            return _fakeComment.GetAll();
        }

        public void Create(FakeComment forum)
        {
            _fakeComment.Add(forum);
        }
    }
}
