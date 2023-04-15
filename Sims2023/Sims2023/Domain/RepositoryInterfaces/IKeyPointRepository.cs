using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IKeyPointRepository
    {
        public int NextId();
        public void Add(KeyPoint keyPoint, List<string> keyPointNames, int toursId, int newToursNumber);
        public void Remove(KeyPoint keyPoint);
        public List<KeyPoint> GetAll();
        public KeyPoint GetById(int id);
        public KeyPoint GetCurrentKeyPoint(Tour tour);//new method for guest2
        public void Save();
    }
}
