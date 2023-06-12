using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sims2023.Domain.Models
{
    public class TourReview : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public User Guest { get; set; }
        public Tour Tour { get; set; }
        public KeyPoint KeyPointJoined { get; set; }
        public int GuideKnowledge { get; set; }
        public int TourInterest { get; set; }
        public int GuidesLanguageCapability { get; set; }
        public float AverageGrade { get; set; }
        public List<string> Pictures { get; set; }
        public string ConcatenatedPictures { get; set; }
        public bool IsValid { get; set; }
        public string Comment { get; set; }

        public TourReview()
        {
            Pictures = new List<string>();
        } 
        public TourReview(int id, User guest, Tour tour,KeyPoint keyPointJoined, int guideKnowledge, int tourInterest, int guidesLanguageCapability,  bool isValid, string comment,string concatenatedPictures)
        {
            Id = id;
            Guest = guest;
            Tour = tour;
            KeyPointJoined = keyPointJoined;
            GuideKnowledge = guideKnowledge;
            TourInterest = tourInterest;
            GuidesLanguageCapability = guidesLanguageCapability;
            IsValid = isValid;
            Comment = comment;
            AverageGrade = (GuideKnowledge+GuidesLanguageCapability+TourInterest)/3;
            ConcatenatedPictures = concatenatedPictures;
            Pictures = new List<string>();
            string[] pictures = ConcatenatedPictures.Split("!");
            foreach (string picture in pictures)
            {
                Pictures.Add(picture);
            }
        }

        public TourReview(User guest, Tour tour, KeyPoint keyPointJoined, int guideKnowledge, int tourInterest, int guidesLanguageCapability, string comment)//constructor for guest
        {
            Guest = guest;
            Tour = tour;
            KeyPointJoined = keyPointJoined;
            GuideKnowledge = guideKnowledge;
            TourInterest = tourInterest;
            GuidesLanguageCapability = guidesLanguageCapability;
            IsValid = true;
            Comment = comment;
            AverageGrade = (GuideKnowledge + GuidesLanguageCapability + TourInterest) / 3;
        }

        public event PropertyChangedEventHandler? PropertyChanged;


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Guest.Id.ToString(),
                Tour.Id.ToString(),
                KeyPointJoined.Id.ToString(),
                GuideKnowledge.ToString(),
                TourInterest.ToString(),
                GuidesLanguageCapability.ToString(),
                AverageGrade.ToString(),
                IsValid.ToString(),
                Comment,
                ConcatenatedPictures
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            Tour = new()
            {
                Id = Convert.ToInt32(values[2])
            };
            KeyPointJoined = new()
            {
                Id = Convert.ToInt32(values[3])
            };
            GuideKnowledge = Convert.ToInt32(values[4]);
            TourInterest = Convert.ToInt32(values[5]);
            GuidesLanguageCapability = Convert.ToInt32(values[6]);
            AverageGrade = float.Parse(values[7]);
            IsValid = bool.Parse(values[8]);
            Comment = values[9];
            ConcatenatedPictures = values[10];
            string[] pictures = ConcatenatedPictures.Split("!");
            foreach (string picture in pictures)
            {
                Pictures.Add(picture);
            }
        }
    }
}
