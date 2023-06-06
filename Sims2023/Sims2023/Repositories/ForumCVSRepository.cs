using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repositories
{
    class ForumCSVRepository : ISubject, IForumCSVRepository
    {
        private List<IObserver> _observers;
        private ForumFileHandler _fileHandler;
        private List<Forum> _forums;

        public ForumCSVRepository()
        {
            _fileHandler = new ForumFileHandler();
            _forums = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public Forum GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public int NextId()
        {
            if (_forums.Count == 0) return 1;
            return _forums.Max(forum => forum.Id) + 1;
        }

        public void Add(Forum forum)
        {
            forum.Id = NextId();
            _forums.Add(forum);
            _fileHandler.Save(_forums);
            NotifyObservers();
        }

        public void Remove(Forum forum)
        {
            _forums.Remove(forum);
            _fileHandler.Save(_forums);
            NotifyObservers();
        }

        public void Save()
        {
            _fileHandler.Save(_forums);
        }

        public void Update(Forum forum)
        {
            int index = _forums.FindIndex(f => f.Id == forum.Id);
            if (index != -1)
            {
                _forums[index] = forum;
            }

            _fileHandler.Save(_forums);
            NotifyObservers();
        }

        public List<Forum> GetAll()
        {
            return _forums;
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
    }

}
