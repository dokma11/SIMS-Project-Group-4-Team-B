using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using static Sims2023.Domain.Models.Voucher;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class GuideAccountViewModel
    {
        private TourService _tourService;
        private VoucherService _voucherService;
        private TourReservationService _tourReservationService;
        public User LoggedInGuide { get; set; }
        public GuideAccountViewModel(TourService tourService, VoucherService voucherService, TourReservationService tourReservationService, User loggedInGuide)
        {
            _tourService = tourService;
            _voucherService = voucherService;
            _tourReservationService = tourReservationService;

            LoggedInGuide = loggedInGuide;
        }

        public void Dismissal()
        {
            foreach (var tour in _tourService.GetGuidesCreated(LoggedInGuide))
            {
                foreach (var res in _tourReservationService.GetByToursid(tour.Id))
                {
                    Voucher newVoucher = new(0, VoucherType.CancelingTour, res.User, tour, DateTime.Now, DateTime.Now.AddYears(2), "The guide quit his job", false);
                    _voucherService.Create(newVoucher);
                }
            }
            _tourService.CancelAll(LoggedInGuide);
        }
    }
}
