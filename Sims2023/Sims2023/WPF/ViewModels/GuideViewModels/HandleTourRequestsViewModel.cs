using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

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

        public List<Request> FilterRequests(string locationSearchTerm, string guestNumberSearchTerm, string languageSearchTerm, string dateStartSearchTerm, string dateEndSearchTerm)
        {
            return _requestService.GetFiltered(locationSearchTerm, guestNumberSearchTerm, languageSearchTerm, dateStartSearchTerm, dateEndSearchTerm);
        }

        public void AcceptRequest()
        {
            if (SelectedRequest != null)
            {
                _requestService.UpdateState(SelectedRequest, RequestsState.Accepted);
                Update();
            }
            else
            {
                MessageBox.Show("Odaberite zahtev");
            }
        }

        public void DeclineRequest()
        {
            if (SelectedRequest != null)
            {
                _requestService.UpdateState(SelectedRequest, RequestsState.Invalid);
                Update();
            }
            else
            {
                MessageBox.Show("Odaberite zahtev");
            }
        }

        public void Update()
        {
            RequestsToDisplay.Clear();
            foreach (Request request in _requestService.GetOnHold())
            {
                RequestsToDisplay.Add(request);
            }
        }
    }
}
