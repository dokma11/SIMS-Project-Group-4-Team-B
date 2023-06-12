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
        private ComplexTourRequestService _complexRequestService { get; set; }
        private SubTourRequestService _subTourRequestService { get; set; }
        public ComplexTourRequest SelectedComplexTourRequest { get; set; }

        public Guest2ComplexTourRequestListViewModel(User user)
        {
            _complexRequestService = new ComplexTourRequestService();
            _subTourRequestService = new SubTourRequestService();
            User = user;
          
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(_complexRequestService.GetByUser(user));
            SelectedComplexTourRequest = null;
           


        }

        public bool RateTour_Click()
        {
            if (IsNull(SelectedComplexTourRequest))
                return false;
            else
                return true;
            
        }

        public bool IsNull(ComplexTourRequest complexTourRequest)
        {
            if (complexTourRequest == null)
            {
                return true;
            }
            return false;
        }

        public bool IsFinished(Tour tour)//new for guest2
        {
            return tour.CurrentState == ToursState.Finished;
        }

       

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
