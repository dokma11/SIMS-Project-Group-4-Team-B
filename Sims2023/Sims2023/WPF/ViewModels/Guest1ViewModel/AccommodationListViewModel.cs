using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public partial class AccommodationListViewModel
    {
        private AccommodationListView AccommodationListView;

        private AccommodationService _accommodationService;
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public UserService _userService;
        public Accommodation SelectedAccommodation { get; set; }
        public User User { get; set; }

        public List<Accommodation> FilteredData = new List<Accommodation>();

        public AccommodationListViewModel(AccommodationListView accommodationListView, User guest1)
        {
            AccommodationListView = accommodationListView;
            _userService = new UserService();

            User = guest1;

            _userService.MarkSuperOwner();
            _accommodationService = new AccommodationService();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodations());

            FilteredData = new List<Accommodation>();
        }

        public void SearchAccommodation_Click(object sender, RoutedEventArgs e)
        {
            FilteredData.Clear();
            AccommodationListView.myDataGrid.ItemsSource = Accommodations;

            string nameSearchTerm = AccommodationListView.nameSearchBox.Text;
            string citySearchTerm = AccommodationListView.citySearchBox.Text;
            string countrySearchTerm = AccommodationListView.countrySearchBox.Text;
            string typeSearchTerm = AccommodationListView.typeComboBox.Text;
            int maxGuests = (int)AccommodationListView.numberOfGuests.Value;
            int minDays = (int)AccommodationListView.numberOfDays.Value;

            _accommodationService.CheckSearchTermConditions(FilteredData, nameSearchTerm, citySearchTerm, countrySearchTerm, typeSearchTerm, maxGuests, minDays);

            AccommodationListView.myDataGrid.ItemsSource = FilteredData;

        }

        public void GiveUpSearch_Click(object sender, RoutedEventArgs e)
        {
            FilteredData.Clear();
            AccommodationListView.myDataGrid.ItemsSource = Accommodations;
        }
    }
}
