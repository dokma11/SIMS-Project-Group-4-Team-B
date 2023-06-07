using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class UserService
    {
        private IUserCSVRepository _user;
        private ITourReviewCSVRepository _tourReview;
        private ITourReadFromCSVRepository _tour;

        public UserService()
        {
            _user = Injection.Injector.CreateInstance<IUserCSVRepository>();
            _tourReview = Injection.Injector.CreateInstance<ITourReviewCSVRepository>();
            _tour = Injection.Injector.CreateInstance<ITourReadFromCSVRepository>();
        }

        public void MarkSuperOwner()
        {
            _user.FindSuperOwners();
        }

        public List<User> GetAllUsers()
        {
            return _user.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            _user.Subscribe(observer);
        }

        public User GetById(int id)
        {
            return _user.GetById(id);
        }

        public void MarkGuestAsSuper(User user)
        {
            _user.MarkGuestAsSuper(user);
        }

        public void MarkGuestAsRegular(User user)
        {
            _user.MarkGuestAsRegular(user);
        }

        public void RemovePoint(User user)
        {
            _user.RemovePointFromGuest1(user);
        }

        public void MarkSuperGuides(User loggedInGuide)
        {
            _user.MarkSuperGuides(_tourReview.GetAll(), _tour.GetFinished(loggedInGuide), loggedInGuide);
        }

        public void MarkDismissal(User loggedInGuide)
        {
            _user.MarkDismissal(loggedInGuide);
        }
    }
}
