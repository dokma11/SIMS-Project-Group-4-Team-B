using Sims2023.Model.DAO;
using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Observer;

namespace Sims2023.Controller
{
    public class TourController
    {
        private TourDAO _tours;
        public TourController()
        {
            _tours = new TourDAO();
        }

        public List<Tour> GetAllTours()
        {
            return _tours.GetAll();
        }

        public void Create(Tour tour)
        {
            _tours.Add(tour);
        }
        public void Delete(Tour tour)
        {
            _tours.Remove(tour);
        }
        public void Subscribe(IObserver observer)
        {
            _tours.Subscribe(observer);
        }

    }
}
