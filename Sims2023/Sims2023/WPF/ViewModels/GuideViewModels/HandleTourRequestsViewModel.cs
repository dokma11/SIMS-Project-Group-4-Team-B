using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Collections.ObjectModel;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public class HandleTourRequestsViewModel
    {
        public ObservableCollection<Request> RequestsToDisplay { get; set; }
        public Request SelectedRequest { get; set; }
        private RequestService _requestService;

        public HandleTourRequestsViewModel(RequestService requestService)
        {
            _requestService = requestService;

            RequestsToDisplay = new ObservableCollection<Request>(_requestService.GetOnHold());
        }


    }
}
