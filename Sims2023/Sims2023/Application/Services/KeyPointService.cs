﻿using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.Repository;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class KeyPointService
    {
        private readonly KeyPointRepository _keyPoint;
        public KeyPointService()
        {
            _keyPoint = new KeyPointRepository();
        }

        public List<KeyPoint> GetAllKeyPoints()
        {
            return _keyPoint.GetAll();
        }

        public KeyPoint GetById(int id)
        {
            return _keyPoint.GetById(id);
        }

        public void Create(KeyPoint keyPoint, List<string> keyPointNames, int toursId, int newToursNumber)
        {
            _keyPoint.Add(keyPoint, keyPointNames, toursId, newToursNumber);
        }

        public void Delete(KeyPoint keyPoint)
        {
            _keyPoint.Remove(keyPoint);
        }

        public void Subscribe(IObserver observer)
        {
            _keyPoint.Subscribe(observer);
        }

        public void Save()
        {
            _keyPoint.Save();
        }
    }
}
