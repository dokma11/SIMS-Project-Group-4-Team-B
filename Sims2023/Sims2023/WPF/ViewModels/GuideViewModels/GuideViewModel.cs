using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using Sims2023.WPF.Views.GuideViews;
using System;
using System.Collections.ObjectModel;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class GuideViewModel : IObserver
    {
        private TourService _tourService;
        private UserService _userService;
        private TourReservationService _tourReservationService;
        private VoucherService _voucherService;
        public Tour? SelectedTour { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }
        public User LoggedInGuide { get; set; }
        public GuideViewModel(User user, TourService tourService, VoucherService voucherService, UserService userService, TourReservationService tourReservationService)
        {
            LoggedInGuide = user;

            _tourService = tourService;
            _voucherService = voucherService;
            _userService = userService;
            _tourReservationService = tourReservationService;

            ToursToDisplay = new ObservableCollection<Tour>(_tourService.GetGuidesCreated(LoggedInGuide));
        }

        public void CancelTour(string additionalComment)
        {
            _tourService.UpdateState(SelectedTour, ToursState.Cancelled);
            CreateVouchersForCancelledTour(SelectedTour.Id, additionalComment);
            Update();
        }

        public void CreateVouchersForCancelledTour(int toursId, string additionalComment)
        {
            foreach (var reservation in _tourReservationService.GetByToursid(toursId))
            {
                Voucher voucher = new(0, Voucher.VoucherType.CancelingTour, _userService.GetById(reservation.User.Id), _tourService.GetById(toursId), DateTime.Now, DateTime.Today.AddYears(1), additionalComment, false);
                _voucherService.Create(voucher);
            }
        }

        public bool IsTourSelected()
        {
            return SelectedTour != null;
        }

        public bool IsTourCreated()
        {
            return SelectedTour.CurrentState == ToursState.Created;
        }

        public bool IsTourEligibleForCancellation()
        {
            return SelectedTour.Start >= DateTime.Now.AddHours(48);
        }

        public void Update()
        {
            ToursToDisplay.Clear();
            foreach (var tour in _tourService.GetGuidesCreated(LoggedInGuide))
            {
                ToursToDisplay.Add(tour);
            }
        }
    }
}
