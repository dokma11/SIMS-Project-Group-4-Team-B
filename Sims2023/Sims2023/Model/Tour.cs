using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sims2023.Domain.Models
{
    public enum ToursState { Created, Started, Finished, Cancelled, Interrupted }
    public enum ToursLanguage { Serbian, English, German, French, Spanish, Italian, Chinese, Japanese }
    public class Tour : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public ToursLanguage GuideLanguage { get; set; }
        public int MaxGuestNumber { get; set; }
        public string KeyPoints { get; set; }
        public List<KeyPoint> KeyPointList { get; set; }
        public DateTime Start { get; set; }
        public int Length { get; set; }
        public ToursState CurrentState { get; set; }
        private int _availableSpace;

        public int AvailableSpace
        {
            get { return _availableSpace; }
            set
            {
                if (_availableSpace != value)
                {
                    _availableSpace = value;
                    OnPropertyChanged("AvailableSpace");
                }
            }
        }
        public List<string> Pictures { get; set; }
        //concatenating all of the pictures urls into one string so I can save it easier
        public string ConcatenatedPictures { get; set; }
        public int AttendedGuestsNumber { get; set; }
        public User Guide { get; set; }

        public Tour()
        {
            Pictures = new List<string>();
        }

        public Tour(int id, string name, Location location, string description, ToursLanguage guideLanguage, int maxGuestNumber, string keyPointsString, DateTime start, int length, string concatenatedPictures, User guide)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            GuideLanguage = guideLanguage;
            MaxGuestNumber = maxGuestNumber;
            AvailableSpace = maxGuestNumber;
            KeyPoints = keyPointsString;
            Start = start;
            Length = length;
            CurrentState = ToursState.Created;
            ConcatenatedPictures = concatenatedPictures;
            Pictures = new List<string>();
            string[] picturesArray = ConcatenatedPictures.Split("!");
            foreach (string picture in picturesArray)
            {
                Pictures.Add(picture);
            }
            Guide = guide;
            AttendedGuestsNumber = 0;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Location.Id.ToString(),
                Description,
                GuideLanguage.ToString(),
                MaxGuestNumber.ToString(),
                AvailableSpace.ToString(),
                KeyPoints,
                Start.ToString(),
                Length.ToString(),
                ConcatenatedPictures,
                CurrentState.ToString(),
                Guide.Id.ToString(),
                AttendedGuestsNumber.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = new()
            {
                Id = Convert.ToInt32(values[2])
            };
            Description = values[3];
            GuideLanguage = (ToursLanguage)Enum.Parse(typeof(ToursLanguage), values[4]);
            MaxGuestNumber = Convert.ToInt32(values[5]);
            AvailableSpace = Convert.ToInt32(values[6]);
            KeyPoints = values[7];
            Start = DateTime.Parse(values[8]);
            Length = Convert.ToInt32(values[9]);
            ConcatenatedPictures = values[10];
            string[] picturesArray = ConcatenatedPictures.Split("!");
            foreach (string picture in picturesArray)
            {
                Pictures.Add(picture);
            }
            CurrentState = (ToursState)Enum.Parse(typeof(ToursState), values[11]);
            Guide = new()
            {
                Id = Convert.ToInt32(values[12])
            };
            AttendedGuestsNumber = Convert.ToInt32(values[13]);
        }
    }
}
