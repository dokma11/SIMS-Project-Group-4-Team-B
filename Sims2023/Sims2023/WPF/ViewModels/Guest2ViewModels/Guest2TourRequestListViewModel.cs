using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class Guest2TourRequestListViewModel
    {
        public List<Request> TourRequests { get; set; }

        public User User { get; set; }
        public RequestService _requestService { get; set; }

        public Guest2TourRequestListViewModel(User user)
        {
            _requestService = new RequestService();
            User = user;
            _requestService.CheckExpirationDate(user);
            TourRequests = _requestService.GetByUser(user);
        }
    }
}
