using Sims2023.Model.DAO;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Controller
{
    public class KeyPointController
    {
        private KeyPointDAO _keyPoint;
        public KeyPointController()
        {
            _keyPoint = new KeyPointDAO();
        }

        public List<KeyPoint> GetAllKeyPoints()
        {
            return _keyPoint.GetAll();
        }

        public void Create(KeyPoint keyPoint)
        {
            _keyPoint.Add(keyPoint);
        }
        public void Delete(KeyPoint keyPoint)
        {
            _keyPoint.Remove(keyPoint);
        }
        public void Subscribe(IObserver observer)
        {
            _keyPoint.Subscribe(observer);
        }
    }
}
