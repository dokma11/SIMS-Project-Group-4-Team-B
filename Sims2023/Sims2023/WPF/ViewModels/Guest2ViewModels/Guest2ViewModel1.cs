using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class Guest2ViewModel1:ViewModel
    {
        #region Polja
        private string currentLanguage;
        public string CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                currentLanguage = value;
            }
        }

        private User user;
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public NavigationService NavService { get; set; }
        public RelayCommand NavigateToStartPageCommand { get; set; }
        public RelayCommand NavigateToTourListPageCommand { get; set; }
        public RelayCommand NavigateToGuest2TourListPageCommand { get; set; }
        public RelayCommand NavigateToGuest2TourRequestListPageCommand { get; set; }
        public RelayCommand NavigateToGuest2ComplexTourRequestListPageCommand { get; set; }
        public RelayCommand NavigateToGuest2VoucherListPageCommand { get; set; }
        public RelayCommand OpenReportCommand { get; set; }
        public RelayCommand OpenNotificationWindowCommand { get; set; }
        public RelayCommand SwitchToSerbianLanguageCommand { get; set; }
        public RelayCommand SwitchToEnglishLanguageCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }

        #endregion

        #region Akcije
        private void Execute_NavigateToStartPageCommand(object obj)
        {

            this.NavService.Navigate(
                new Uri("Views/Guest2Views/Guest2StartView.xaml", UriKind.Relative));
        }
        private void Execute_NavigateToTourListPageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("Views/Guest2Views/TourListView.xaml",UriKind.Relative));
            Page tourList = new TourListView(User);
           // this.frame.NavigationService.Navigate(tourList);
        }
        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }


        #endregion

        #region Konstruktori
        public Guest2ViewModel1(NavigationService navService,User user)
        {
            this.NavigateToStartPageCommand = new RelayCommand(Execute_NavigateToStartPageCommand, CanExecute_NavigateCommand);
            this.NavigateToTourListPageCommand = new RelayCommand(Execute_NavigateToTourListPageCommand, CanExecute_NavigateCommand);
            
            this.CurrentLanguage = "en-US";
          
            this.NavService = navService;
            this.User = user;
        }
        #endregion
    }
}
