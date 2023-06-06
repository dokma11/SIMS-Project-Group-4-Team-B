using Sims2023.Domain.Models;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IForumCSVRepository
    {
        Forum GetById(int id);
        int NextId();
        void Add(Forum forum);
        void Remove(Forum forum);
        void Save();
        void Update(Forum forum);
        List<Forum> GetAll();
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        void NotifyObservers();
    }

}
