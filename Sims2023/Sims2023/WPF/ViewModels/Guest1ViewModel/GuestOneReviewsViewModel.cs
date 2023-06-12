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
    public class GuestOneReviewsViewModel
    {
        GuestOneReviewsView GuestOneReviewsView;
        public ObservableCollection <GuestGrade> GuestGrades { get; set; }

        private GuestGradeService _guestGradeService;

        User User;
        public GuestGrade SelectedGuestGrade { get; set; }

        List<GuestGrade> FilteredData = new ();

        private AccommodationReservationService _accommodationReservationService;

        public GuestOneReviewsViewModel(GuestOneReviewsView guestOneReviewsView,User guest)
        {
            GuestOneReviewsView = guestOneReviewsView;
            User = guest;

            _guestGradeService = new ();
            GuestGrades = new (_guestGradeService.GetAllGrades());
            _accommodationReservationService = new AccommodationReservationService();
            FilteredData = _accommodationReservationService.FindSuitableGrades(User, _guestGradeService.GetAllGrades());
            GuestOneReviewsView.myDataGrid.ItemsSource = FilteredData;
        }

        public bool ShowComment()
        {
            SelectedGuestGrade = (GuestGrade)GuestOneReviewsView.myDataGrid.SelectedItem;

            if (SelectedGuestGrade == null)
            {
                MessageBox.Show("Molimo Vas selektujte ocenu.");
                return false;
            }
            MessageBox.Show($"Komentar Vlasnika smeštaja:\n\n{SelectedGuestGrade.Comment}");
            return true;
        }
    }
}
