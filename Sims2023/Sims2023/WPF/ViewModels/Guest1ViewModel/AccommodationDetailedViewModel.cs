using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;

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
            AccommodationDetailedView.accommodatioNameLabel.Content = "Naziv smeštaja: " + selectedAccommodation.Name;
            AccommodationDetailedView.accommodatioCityLabel.Content = "Grad: " + selectedAccommodation.Location.City;
            AccommodationDetailedView.accommodatioCountryLabel.Content = "Država: " + selectedAccommodation.Location.Country;
            AccommodationDetailedView.accommodatioTypeLabel.Content = "Tip smeštaja: " + selectedAccommodation.Type.ToString();
            AccommodationDetailedView.PicturesListView.ItemsSource = selectedAccommodation.Imageurls;
        }
    }
}
