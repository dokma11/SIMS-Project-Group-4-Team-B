using Microsoft.Win32;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest2Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class RateTourViewModel
    {
        public TourReviewService _tourReviewService;
        public KeyPointService _keyPointService;
        public User User { get; set; }
        public Tour Tour { get; set; }
        public TourReview TourReview { get; set; }
        public KeyPoint KeyPoint { get; set; }
        public RateTourView RateTourView { get; set; }
        public List<string> _picturesList;

        private List<string> _addedPictures = new List<string>();
        List<BitmapImage> _imageList = new List<BitmapImage>();
        private List<BitmapImage> _permanentPictures = new List<BitmapImage>();

        public RateTourViewModel(User user, Tour tour, RateTourView rateTourView)
        {
            _tourReviewService = new TourReviewService();
            _keyPointService = new KeyPointService();

            User = user;
            Tour = tour;
            RateTourView = rateTourView;
            

            _picturesList = new List<string>();
            KeyPoint = _keyPointService.GetWhereGuestJoined(Tour, User);
        }

        public void Send_Click()
        {
            TourReview = new TourReview(User, Tour, KeyPoint, (int)RateTourView.guidesKnowledgeBox.Value, (int)RateTourView.tourInterestBox.Value, (int)RateTourView.guidesLanguageCapabilityBox.Value, (string)RateTourView.CommentTextBox.Text);
            _tourReviewService.Create(TourReview);
            _tourReviewService.AddReviewsPictures(_picturesList, TourReview);
            if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
            {
                MessageBox.Show("Hvala na recenziji");
            }
            else
            {
                MessageBox.Show("Thank you for the review");
            }
            RateTourView.Close();

        }

       
        public void SaveButton_Click()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                _imageList.Clear(); // Clear the imageList collection before adding new images
                foreach (string filename in openFileDialog.FileNames)
                {
                    string relativePath = $"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/Images/Guest2Images/{Path.GetFileName(filename)}";
                    Uri imageUri = new Uri(relativePath, UriKind.Absolute);
                    BitmapImage bitmapImage = new BitmapImage(imageUri);
                    _imageList.Add(bitmapImage);
                    _addedPictures.Add(relativePath);
                    _permanentPictures.Add(bitmapImage);
                    _picturesList.Add(bitmapImage.ToString());  
                }
                RateTourView.picturesOutput.ItemsSource = null;
                RateTourView.picturesOutput.ItemsSource = _permanentPictures;
            }
        }
    }
}
