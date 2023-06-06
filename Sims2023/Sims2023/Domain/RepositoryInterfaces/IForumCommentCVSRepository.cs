using Sims2023.Domain.Models;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IForumCommentRepository
    {
        ForumComment GetById(int id);
        int NextId();
        void Add(ForumComment forumComment);
        void Remove(ForumComment forumComment);
        void Save();
        void Update(ForumComment forumComment);
        List<ForumComment> GetAll();
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        void NotifyObservers();
    }

}
