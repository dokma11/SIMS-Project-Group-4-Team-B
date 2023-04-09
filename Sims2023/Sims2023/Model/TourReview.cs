using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sims2023.Model
{
    public class TourReview: ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public User Guest { get; set; }
        public User Guide { get; set; }
        public Tour Tour { get; set; }
        public KeyPoint KeyPointJoined { get; set; }
        public int GuideKnowledge { get; set; }
        public int TourInterest { get; set; }
        public int GuidesLanguageCapability { get; set; }
        public float AverageGrade { get; set; }
        //Slike
        public bool IsValid { get; set; }
        public string Comment { get; set; }

        public TourReview() { } 
        public TourReview(int id, User guest, User guide, Tour tour, KeyPoint keyPointJoined, int guideKnowledge, int tourInterest, int guidesLanguageCapability,  bool isValid, string comment)
        {
            Id = id;
            Guest = guest;
            Guide = guide;
            Tour = tour;
            KeyPointJoined = keyPointJoined;
            GuideKnowledge = guideKnowledge;
            TourInterest = tourInterest;
            GuidesLanguageCapability = guidesLanguageCapability;
            IsValid = isValid;
            Comment = comment;
            AverageGrade = (GuideKnowledge+GuidesLanguageCapability+TourInterest)/3;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Guest.Id.ToString(),
                Guide.Id.ToString(),
                Tour.Id.ToString(),
                KeyPointJoined.Id.ToString(),
                GuideKnowledge.ToString(),
                TourInterest.ToString(),
                GuidesLanguageCapability.ToString(),
                AverageGrade.ToString(),
                IsValid.ToString(),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User guest = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            UserService userController = new();
            Guest = userController.GetById(guest.Id);
            User guide = new()
            {
                Id = Convert.ToInt32(values[2])
            };
            Guide = userController.GetById(guide.Id);
            Tour tour = new()
            {
                Id = Convert.ToInt32(values[3])
            };
            TourService tourController = new();
            Tour = tourController.GetById(tour.Id);
            KeyPoint keyPoint = new()
            {
                Id = Convert.ToInt32(values[4])
            };
            KeyPointService keyPointController = new();
            KeyPointJoined = keyPointController.GetById(keyPoint.Id);
            GuideKnowledge = Convert.ToInt32(values[5]);
            TourInterest = Convert.ToInt32(values[6]);
            GuidesLanguageCapability = Convert.ToInt32(values[7]);
            AverageGrade = float.Parse(values[8]);
            IsValid = bool.Parse(values[9]);
            Comment = values[10];
        }
    }
}
