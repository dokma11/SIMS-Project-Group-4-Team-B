using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class TourDetailedViewModel
    {
        public TourDetailedView TourDetailedView;
        public Tour Tour { get; set; }
        public User User { get; set; }
        
        private TourService _tourService;
        private TourReservationService _tourReservationService;
        private VoucherService _voucherService;
        
        public int currentIndex;
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ReserveCommand { get; set; }    
        public TourDetailedViewModel(TourDetailedView tourDetailedView,Tour tour,User user)
        {
            TourDetailedView = tourDetailedView;
            Tour = tour;
            _tourService = new TourService();
            _tourReservationService = new TourReservationService();
            _voucherService = new VoucherService();
            User = user;
            this.CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecute_Command);
            this.ReserveCommand= new RelayCommand(Execute_ReserveCommand, CanExecute_Command);
            

            currentIndex = 0;
           
            FillTextBoxes();
        }

        public void FillTextBoxes()
        {
            TourDetailedView.nameTextBlock.Text = Tour.Name;
            TourDetailedView.cityTextBlock.Text = Tour.Location.City;
            TourDetailedView.countryTextBlock.Text = Tour.Location.Country;
            TourDetailedView.languageTextBlock.Text = Tour.GuideLanguage.ToString();
            TourDetailedView.hoursTextBlock.Text=Tour.Length.ToString();
            TourDetailedView.startTimeTextBlock.Text = Tour.Start.ToString();
            TourDetailedView.descriptionTextBlock.Text=Tour.Description;
            TourDetailedView.tourImage.Source = new BitmapImage(_tourService.GetPictureUri(Tour,currentIndex));
            
           
        }

        public void NextPicture()
        {
            
            if (currentIndex < Tour.Pictures.Count - 1)
            {
                currentIndex++;
            }
            TourDetailedView.tourImage.Source = new BitmapImage(_tourService.GetPictureUri(Tour, currentIndex));
              

        }
        public void PreviousPicture()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            TourDetailedView.tourImage.Source = new BitmapImage(_tourService.GetPictureUri(Tour, currentIndex));
        }
        private void Execute_ReserveCommand(object obj)
        {
            int reservedSpace = TourDetailedView.GuestNumber;




            TourReservation tourReservation = new TourReservation(Tour, User, reservedSpace);
            _tourReservationService.Create(tourReservation);
            _tourService.UpdateAvailableSpace(reservedSpace, Tour);

            if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
            {
                MessageBox.Show("Uspešna rezervacija");
            }
            else
            {
                MessageBox.Show("Successful reservation");
            }
            CheckVouchers(tourReservation);

            ShowVoucherListView();
        }
        

        public void ShowVoucherListView()
        {
            if (_voucherService.GetByUser(User).Count > 0)
            {
                var Guest2VouchersActivationListView = new Guest2VouchersActivationListView(User);
                Guest2VouchersActivationListView.Closed += Guest2VouchersActivationListView_Closed;
                Guest2VouchersActivationListView.Show();
            }
            else
            {
                TourDetailedView.Close();
            }
        }

        private void Guest2VouchersActivationListView_Closed(object sender, EventArgs e)
        {
            
            TourDetailedView.Close();

        }

        public void CheckVouchers(TourReservation tourReservation)
        {
            if (_tourReservationService.CheckVouchers(tourReservation))
            {
                Voucher Voucher = new Voucher(Voucher.VoucherType.FiveReservations, User, Tour);
                _voucherService.Create(Voucher);
            }
        }
        
        private void Execute_CancelCommand(object obj)
        {
            TourDetailedView.Close();
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

    }
}
