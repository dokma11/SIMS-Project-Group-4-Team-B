﻿using Sims2023.Model.DAO;
using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Observer;
using System.Windows;

namespace Sims2023.Controller
{
    public class TourController
    {
        private TourDAO _tour;
        private LocationDAO _location;
        public TourController()
        {
            _tour = new TourDAO();
            _location = new LocationDAO();
        }

        public List<Tour> GetAllTours()
        {
            return _tour.GetAll();
        }

        public void Create(Tour tour, List<DateTime> dateTimes)
        {
            _tour.Add(tour, dateTimes);
        }
        public void AddToursLocation(Tour tour, Location location) 
        {
            List<Location> locations = new List<Location>();
            locations = _location.GetAll();
            int counter = 0;

            if(location.City == null || location.Country == null)
            {
                MessageBox.Show("Unesite lokaciju ");
            }

            if(locations.Count == 0) 
            {
                _tour.AddToursLocation(tour, location);
            }
            else
            {
                foreach(var locationInstance in locations)
                {
                    if(location.City == locationInstance.City && location.Country == locationInstance.Country)
                    {
                        counter++;
                        _tour.AddToursLocation(tour, locationInstance);
                        break;
                    }
                }
                if(counter == 0)
                {
                    _tour.AddToursLocation(tour, location);
                }
            }
            
        }
        public void Delete(Tour tour)
        {
            _tour.Remove(tour);
        }
        public void Subscribe(IObserver observer)
        {
            _tour.Subscribe(observer);
        }

    }
}
