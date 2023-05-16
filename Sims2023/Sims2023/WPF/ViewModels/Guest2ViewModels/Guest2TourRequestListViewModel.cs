using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class Guest2TourRequestListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Request> _tourRequests;

        public ObservableCollection<Request> TourRequests
        {
            get { return _tourRequests; }
            set
            {
                _tourRequests = value;
                OnPropertyChanged("TourRequests");
            }
        }

        

        public User User { get; set; }
        public RequestService _requestService { get; set; }

        public Guest2TourRequestListViewModel(User user)
        {
            _requestService = new RequestService();
            User = user;
            _requestService.CheckExpirationDate(user);
            TourRequests = new ObservableCollection<Request>(_requestService.GetByUser(user));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

