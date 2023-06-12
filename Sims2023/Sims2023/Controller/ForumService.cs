using Sims2023.Application.Injection;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Application.Services
{
    public class ForumService
    {
        private IForumCSVRepository _forums;
        private IUserCSVRepository _users;
        private ILocationCSVRepository _locations;

        public ForumService()
        {
            _users = Injector.CreateInstance<IUserCSVRepository>();
            _locations = Injector.CreateInstance<ILocationCSVRepository>();
            _forums = Injector.CreateInstance<IForumCSVRepository>();
            FindForeignAttributes();
        }

        public void Save()
        {
            _forums.Save();
            FindForeignAttributes();
        }

        private void FindForeignAttributes()
        {
            foreach (var item in GetAllForums())
            {
                var user = _users.GetById(item.User.Id);
                var loc = _locations.GetById(item.Location.Id);
                if (user != null && loc != null)
                {
                    item.User = user;
                    item.Location = loc;
                }
            }
        }

        public List<Forum> GetAllForums()
        {
            return _forums.GetAll();
        }

        public void Create(Forum forum)
        {
            _forums.Add(forum);
            Save();
        }

        public void Delete(Forum forum)
        {
            _forums.Remove(forum);
            Save();
        }

        public void Update(Forum forum)
        {
            _forums.Update(forum);
            Save();
        }

        public void Subscribe(IObserver observer)
        {
            _forums.Subscribe(observer);
        }

        public Forum GetById(int id)
        {
            return _forums.GetById(id);
        }

        public ObservableCollection<Forum> FilterForums(ObservableCollection<Forum> filteredForums, string citySearch, string countrySearch)
        {
            return _forums.FilterForums(filteredForums, citySearch, countrySearch);
        }

        public void MarkAsSpecial(List<ForumComment> allComments)
        {
            _forums.MarkAsSpecial(allComments);
        }

        public List<Forum> GetForumsForParticularOwner(List<Location> _locations)
        {
            Save();
            return _forums.GetForumsForParticularOwner(_locations);
        }
    }

}
