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
        public ObservableCollection<Forum> FilterForums(ObservableCollection<Forum> filteredForums, string citySearch, string countrySearch)
        {
            foreach (Forum forum in _forums)
            {
                bool cityCondition = string.IsNullOrEmpty(citySearch) || forum.Location.City==citySearch;
                bool countryCondition = string.IsNullOrEmpty(countrySearch) || forum.Location.Country==countrySearch;

                if (cityCondition && countryCondition)
                {
                    filteredForums.Add(forum);
                }
            }
            return filteredForums;
        }
        public void MarkAsSpecial(List<ForumComment> comments)
        {
            List<Forum> forumsCopy = new List<Forum>(_forums);
            foreach (var forum in forumsCopy)
            {
                if (AppropriateNumberOfSpecialComments(forum, comments))
                {
                    forum.Special = true;
                    Update(forum);
                }
            }
        }

        public bool AppropriateNumberOfSpecialComments(Forum forum, List<ForumComment> comments)
        {
            if (forum.CountGuestComments>=20)
            {
                return true;
            }
            if (forum.CountOwnerComments >= 10)
            {
                return true;
            }
            return false;


        }
        public int CountSpecialComments(Forum forum, List<ForumComment> comments)
        {
            int i = 0;
            foreach (ForumComment comment in comments)
            {
                if (comment.Forum.Id == forum.Id && comment.Special == true)
                {
                    i++;
                }
            }
            return i;
        }

        public List<Forum> GetForumsForParticularOwner(List<Location> _locations)
        {
            List<Forum> forums = _forums.Where(f => _locations.Any(l => l.Id == f.Location.Id)).ToList();
            return forums;

        }
    }

}
