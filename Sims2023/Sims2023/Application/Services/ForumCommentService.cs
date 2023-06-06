using Sims2023.Application.Injection;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Application.Services
{
    public class ForumCommentService
    {
        private IForumCommentRepository _forumComments;
        private IUserCSVRepository _users;
        private IForumCSVRepository _forums;

        public ForumCommentService()
        {
            _users = Injector.CreateInstance<IUserCSVRepository>();
            _forums = Injector.CreateInstance<IForumCSVRepository>();
            _forumComments = Injector.CreateInstance<IForumCommentRepository>();
            FindForeignAttributes();
        }

        private void FindForeignAttributes()
        {
            foreach (var item in GetAllForumComments())
            {
                var user = _users.GetById(item.User.Id);
                var loc = _forums.GetById(item.Forum.Id);
                if (user != null && loc != null)
                {
                    item.User = user;
                    item.Forum = loc;
                }
            }
        }

        public void Save()
        {
            _forumComments.Save();
            FindForeignAttributes();
        }

        public List<ForumComment> GetAllForumComments()
        {
            return _forumComments.GetAll();
        }

        public void Create(ForumComment forumComment)
        {
            _forumComments.Add(forumComment);
            Save();
        }

        public void Delete(ForumComment forumComment)
        {
            _forumComments.Remove(forumComment);
            Save();
        }

        public void Update(ForumComment forumComment)
        {
            _forumComments.Update(forumComment);
            Save();
        }

        public ForumComment GetById(int id)
        {
            return _forumComments.GetById(id);
        }
        public void Subscribe(IObserver observer)
        {
            _forumComments.Subscribe(observer);
        }

        internal ObservableCollection<ForumComment> FilterComments(ObservableCollection<ForumComment> comments, Forum selectedForum)
        {
            return _forumComments.FilterComments(comments,selectedForum);
        }
    }

}
