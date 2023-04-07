﻿using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model.DAO
{
    internal class AccommodationGradeDAO: ISubject
    {

        private List<IObserver> _observers;

        private AccommodationGradeFileHandler _fileHandler;
        private List<AccommodationGrade> _accommodationGrades;



        public AccommodationGradeDAO()
        {
            _fileHandler = new AccommodationGradeFileHandler();
            _accommodationGrades = _fileHandler.Load();
            _observers = new List<IObserver>();
        }



        public int NextId()
        {
            if (_accommodationGrades.Count == 0) return 1;
            return _accommodationGrades.Max(s => s.Id) + 1;
        }

        public void Add(AccommodationGrade accommodationGrade)
        {
            accommodationGrade.Id = NextId();
            _accommodationGrades.Add(accommodationGrade);
            _fileHandler.Save(_accommodationGrades);
            NotifyObservers();
        }

        public void Remove(AccommodationGrade accommodationGrade)
        {
            _accommodationGrades.Remove(accommodationGrade);
            _fileHandler.Save(_accommodationGrades);
            NotifyObservers();
        }

        public void Update(AccommodationGrade accommodationGrade)
        {
            int index = _accommodationGrades.FindIndex(p => p.Id == accommodationGrade.Id);
            if (index != -1)
            {
                _accommodationGrades[index] = accommodationGrade;
            }

            _fileHandler.Save(_accommodationGrades);
            NotifyObservers();
        }

        public List<AccommodationGrade> GetAll()
        {
            return _accommodationGrades;
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