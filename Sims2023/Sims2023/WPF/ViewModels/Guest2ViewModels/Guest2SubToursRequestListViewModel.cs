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
    public class Guest2SubToursRequestListViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<SubTourRequest> _subTourRequests;

        public ObservableCollection<SubTourRequest> SubTourRequests
        {
            get { return _subTourRequests; }
            set
            {
                _subTourRequests = value;
                OnPropertyChanged("SubTourRequests");
            }
        }

        private SubTourRequestService _subTourRequestService;
        public User User { get; set; }
        public ComplexTourRequest ComplexTourRequest { get; set; }
        public Guest2SubToursRequestListViewModel(ComplexTourRequest complexTourRequest,User user)
        {
            _subTourRequestService = new SubTourRequestService();
            ComplexTourRequest = complexTourRequest;
            User = user;
            SubTourRequests = new ObservableCollection<SubTourRequest>(_subTourRequestService.GetByComplexTourRequest(complexTourRequest));
            //SubTourRequests = new ObservableCollection<SubTourRequest>(_subTourRequestService.GetAll());
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
