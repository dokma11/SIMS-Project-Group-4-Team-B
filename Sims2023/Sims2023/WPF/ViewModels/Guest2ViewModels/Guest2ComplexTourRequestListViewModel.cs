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
    public class Guest2ComplexTourRequestListViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<ComplexTourRequest> _complexTourRequests;

        public ObservableCollection<ComplexTourRequest> ComplexTourRequests
        {
            get { return _complexTourRequests; }
            set
            {
                _complexTourRequests = value;
                OnPropertyChanged("ComplexTourRequests");
            }
        }



        public User User { get; set; }
        public ComplexTourRequestService _complexRequestService { get; set; }

        public Guest2ComplexTourRequestListViewModel(User user)
        {
            _complexRequestService = new ComplexTourRequestService();
            User = user;
            //_complexRequestService.CheckExpirationDate(user);
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(_complexRequestService.GetByUser(user));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
