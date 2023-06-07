using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.GuideViewModels;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications.Messages.Core;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    internal class SearchForumViewModel: IObserver
    {
        public User User { get; set; }
        public Location NewLocation { get; set; }
        public Frame MainFrame { get; set; }
        public ForumService _forumService;
        public ForumCommentService _forumCommentService;
        public ObservableCollection<Forum> FilteredForums=new();
        public String CitySearch;
        public String CountrySearch;
        public CountriesAndCitiesService _countriesAndCitiesService;
        public LocationService _locationService;
        public Forum SelectedForum { get; set; }

        SearchForumView SearchForumView;

        public SearchForumViewModel(User user, Frame mainFrame, SearchForumView searchForumView)
        {
            User = user;
            MainFrame = mainFrame;
            SearchForumView= searchForumView;
            NewLocation = new();
            _forumService =new ForumService();
            _forumService.Subscribe(this);
            _forumCommentService = new ForumCommentService();
            _forumCommentService.Subscribe(this);
            FilteredForums = new ();

            _countriesAndCitiesService = new CountriesAndCitiesService();
            _locationService = new LocationService();
            SearchForumView.countryBox.ItemsSource = GetCitiesAndCountries();
            SearchForumView.countryBox.DisplayMemberPath = "CountryName";
            SearchForumView.countryBox.SelectedValuePath = "CountryName";

        }

        public void CountryComboBox_SelectionChanged()
        {
            var selectedCountry = (CountriesAndCities)SearchForumView.countryBox.SelectedItem;
            var cities = new List<string> { selectedCountry.City1, selectedCountry.City2, selectedCountry.City3, selectedCountry.City4, selectedCountry.City5 };
            NewLocation = new();
            SearchForumView.cityBox.ItemsSource = cities;
        }
        public List<CountriesAndCities> GetCitiesAndCountries()
        {
            return _countriesAndCitiesService.GetAllLocations();
        }

        public void NewForum()
        {
            CitySearch = SearchForumView.cityBox.Text;
            CountrySearch = SearchForumView.countryBox.Text;
            NewLocation = new();
            Location location = findLocation(CountrySearch, CitySearch);
            if (!string.IsNullOrEmpty(CountrySearch) || !string.IsNullOrEmpty(CitySearch))
            {
                MainFrame.Navigate(new MakeNewForumView(location, FilteredForums, _forumService, MainFrame, User));
                return;
            }
            if(location==null)
            {
                MessageBox.Show("Doslo je do greske prilikom pretrazivanja lokacije");
            }
            MessageBox.Show("Unesite grad i drzavu za koje zelite da procitate ili otvorite forum");
        }

        private Location findLocation(string countrySearch, string citySearch)
        {
            NewLocation.City = citySearch;
            NewLocation.Country = countrySearch;
            _locationService.Create(NewLocation);
            return NewLocation;
        }

        public void SearchForum()
        {
            FilteredForums.Clear();
            CitySearch = SearchForumView.cityBox.Text;
            CountrySearch= SearchForumView.countryBox.Text;
            if(!string.IsNullOrEmpty(CountrySearch) || !string.IsNullOrEmpty(CitySearch))
            {
                FilteredForums=_forumService.FilterForums(FilteredForums, CitySearch, CountrySearch);
                SearchForumView.ForumsGrid.ItemsSource = FilteredForums;
                return;
            }
            MessageBox.Show("Unesite gradove za koje zelite da pronađete ili otvorite forum");
        }

        public void ShowForum()
        {
            SelectedForum = (Forum)SearchForumView.ForumsGrid.SelectedItem;
            if(SelectedForum != null)
            {
                MainFrame.Navigate(new OpenedForumView(_forumService, MainFrame, User,SelectedForum,_forumCommentService));
                return;
            }
            MessageBox.Show("Selektujte temu koju zelite da prikazete.");
        }
        public void Update()
        {
            FilteredForums.Clear();
            FilteredForums = new();
            FilteredForums = _forumService.FilterForums(FilteredForums, CitySearch, CountrySearch);
            SearchForumView.ForumsGrid.ItemsSource = FilteredForums;
        }
    }
}
