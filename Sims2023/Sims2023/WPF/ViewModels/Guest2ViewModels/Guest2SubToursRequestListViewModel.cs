using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class Guest2SubToursRequestListViewModel : INotifyPropertyChanged
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
        private RequestService _requestService { get; set; }
        public RelayCommand NewSubtour_Click { get; set; }
        public RelayCommand Back_Click { get; set; }
        public Guest2SubToursRequestListView Guest2SubToursRequestListView{get; set;}

        public Guest2SubToursRequestListViewModel(ComplexTourRequest complexTourRequest,User user,Guest2SubToursRequestListView guest2SubToursRequestListView)
        {
            _subTourRequestService = new SubTourRequestService();
            _requestService=new RequestService();
            ComplexTourRequest = complexTourRequest;
            User = user;
            _requestService.CheckExpirationDate(complexTourRequest.Guest);
            SubTourRequests = new ObservableCollection<SubTourRequest>(_subTourRequestService.GetByComplexTourRequest(complexTourRequest));
            Guest2SubToursRequestListView = guest2SubToursRequestListView;
            this.NewSubtour_Click = new RelayCommand(Execute_Command, CanExecute_NavigateCommand);
            this.Back_Click = new RelayCommand(Execute_CancelCommand, CanExecute_NavigateCommand);
            
            //SubTourRequests = new ObservableCollection<SubTourRequest>(_subTourRequestService.GetAll());
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Execute_CancelCommand(object obj)
        {
            Guest2SubToursRequestListView.Close();
        }
        private void Execute_Command(object obj)
        {
            CreateSubTourRequestView createSubTourRequestView = new CreateSubTourRequestView(User, ComplexTourRequest);
            createSubTourRequestView.Closed += CreateSubTourRequestView_Closed;
            createSubTourRequestView.Show();
        }
        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        private void CreateSubTourRequestView_Closed(object sender, EventArgs e)
        {
            // Refresh the data grid

            SubTourRequests = new ObservableCollection<SubTourRequest>(_subTourRequestService.GetByComplexTourRequest(ComplexTourRequest));

        }
    }
}
