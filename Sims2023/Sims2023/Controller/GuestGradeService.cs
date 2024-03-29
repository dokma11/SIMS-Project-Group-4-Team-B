﻿using Sims2023.Application.Injection;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class GuestGradeService
    {
        private IUserCSVRepository _user;
        private IAccommodationCSVRepository _accommodation;
        private IGuestGradeCSVRepository _grade;

        public GuestGradeService()
        {
            _user = Injector.CreateInstance<IUserCSVRepository>();
            _accommodation = Injector.CreateInstance<IAccommodationCSVRepository>();
            _grade = Injector.CreateInstance<IGuestGradeCSVRepository>();
            GetReservationReferences();
        }

        public void GetReservationReferences()
        {
            foreach (var item in GetAllGrades())
            {
                var accommodation = _accommodation.GetById(item.Accommodation.Id);
                var owner = _user.GetById(accommodation.Owner.Id);
                var user = _user.GetById(item.Guest.Id);
                if (accommodation != null)
                {
                    item.Guest = user;
                    item.Accommodation = accommodation;
                    item.Accommodation.Owner = owner;
                }
            }
        }

        public List<GuestGrade> GetAllGrades()
        {
            return _grade.GetAll();
        }

       public void Create(GuestGrade grade)
        {
            _grade.Add(grade);
        }

        public void Delete(GuestGrade grade)
        {
            _grade.Remove(grade);
        }

        public void Update(GuestGrade grade)
        {
            _grade.Update(grade);
        }

        public void Subscribe(IObserver observer)
        {
            _grade.Subscribe(observer);
        }

        public List<GuestGrade> FindSuitableGrades(User user)
        {
            return _grade.FindSuitableGrades(user);
        }
    }
}
