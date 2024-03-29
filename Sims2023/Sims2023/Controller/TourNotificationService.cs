﻿using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class TourNotificationService
    {
        private readonly ITourNotificationCSVRepository _tourNotification;
        private ITourReadFromCSVRepository _tour;
        private IUserCSVRepository _user;
        private ILocationCSVRepository _location;
        public TourNotificationService()
        {
            _tourNotification = Injection.Injector.CreateInstance<ITourNotificationCSVRepository>();
            _tour = Injection.Injector.CreateInstance<ITourReadFromCSVRepository>();
            _user = Injection.Injector.CreateInstance<IUserCSVRepository>();
            _location=Injection.Injector.CreateInstance<ILocationCSVRepository>();  

            GetTourReferences();
            GetUserReferences();
        }

        public List<TourNotification> GetAll()
        {
            return _tourNotification.GetAll();
        }

        public TourNotification GetById(int id)
        {
            return _tourNotification.GetById(id);
        }

        public void Create(TourNotification acceptedTourRequest)
        {
            
            _tourNotification.Add(acceptedTourRequest);
            GetTourReferences();
            GetUserReferences();
        }

        public void Subscribe(IObserver observer)
        {
            _tourNotification.Subscribe(observer);
        }

        public List<TourNotification> GetAcceptedTourRequest(User user)
        {
            GetTourReferences();
            GetUserReferences();
            return _tourNotification.GetAcceptedTourRequest(user);
        }

        public List<TourNotification> GetMatchedTourRequestsLocation(User user)
        {
            GetTourReferences();
            GetUserReferences();
            return _tourNotification.GetMatchedTourRequestsLocation(user);
        }

        public List<TourNotification> GetMatchedTourRequestsLanguage(User user)
        {
            GetTourReferences();
            GetUserReferences();
           
            return _tourNotification.GetMatchedTourRequestsLanguage(user);
        }

        public void SetIsNotified(TourNotification tourNotification)
        {
            
            _tourNotification.SetIsNotified(tourNotification);
            GetTourReferences();
            GetUserReferences();
        }

        public void GetTourReferences()
        {
            foreach (var notification in GetAll())
            {
                notification.Tour = _tour.GetById(notification.Tour.Id) ?? notification.Tour;
                notification.Tour.Location = _location.GetById(notification.Tour.Location.Id);
            }

            
        }

        public void GetUserReferences()
        {
            foreach (var notification in GetAll())
            {
                notification.Guest = _user.GetById(notification.Guest.Id) ?? notification.Guest;
            }
        }


    }
}
