using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repositories
{
    class ForumCommentCSVRepository : ISubject, IForumCommentRepository
    {
        private List<IObserver> _observers;
        private ForumCommentFileHandler _fileHandler;
        private List<ForumComment> _forumComments;

        public ForumCommentCSVRepository()
        {
            _fileHandler = new ForumCommentFileHandler();
            _forumComments = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public ForumComment GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public int NextId()
        {
            if (_forumComments.Count == 0) return 1;
            return _forumComments.Max(comment => comment.Id) + 1;
        }

        public void Add(ForumComment forumComment)
        {
            forumComment.Id = NextId();
            _forumComments.Add(forumComment);
            _fileHandler.Save(_forumComments);
            NotifyObservers();
        }

        public void Remove(ForumComment forumComment)
        {
            _forumComments.Remove(forumComment);
            _fileHandler.Save(_forumComments);
            NotifyObservers();
        }

        public void Save()
        {
            _fileHandler.Save(_forumComments);
        }

        public void Update(ForumComment forumComment)
        {
            int index = _forumComments.FindIndex(comment => comment.Id == forumComment.Id);
            if (index != -1)
            {
                _forumComments[index] = forumComment;
            }

            _fileHandler.Save(_forumComments);
            NotifyObservers();
        }

        public List<ForumComment> GetAll()
        {
            return _forumComments;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
        public ObservableCollection<ForumComment> FilterComments(ObservableCollection<ForumComment> comments, Forum selectedForum)
        {
            foreach(var item in _forumComments)
            {
                if (item.Forum.Id == selectedForum.Id)
                {
                    comments.Add(item);
                }
            }
            return comments;
        }
    }

}
