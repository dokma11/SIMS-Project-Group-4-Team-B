using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public class AccommodationDetailedViewModel
    {
        AccommodationDetailedView AccommodationDetailedView;
        public User User { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public AccommodationDetailedViewModel(AccommodationDetailedView accommodationDetailedView, User guest1, Accommodation selectedAccommodation)
        {
            AccommodationDetailedView = accommodationDetailedView;

            User = guest1;
            SelectedAccommodation = selectedAccommodation;
            FillTextBoxes(SelectedAccommodation);
        }

        public void FillTextBoxes(Accommodation selectedAccommodation)
        {
            AccommodationDetailedView.accommodatioNameTextBox.Text = selectedAccommodation.Name;
            AccommodationDetailedView.accommodatioCityTextBox.Text = selectedAccommodation.Location.City;
            AccommodationDetailedView.accommodatioCountryTextBox.Text = selectedAccommodation.Location.Country;
            AccommodationDetailedView.accommodatioTypeTextBox.Text = selectedAccommodation.Type.ToString();
            AccommodationDetailedView.PicturesListView.ItemsSource = selectedAccommodation.Imageurls;
        }
    }
}
