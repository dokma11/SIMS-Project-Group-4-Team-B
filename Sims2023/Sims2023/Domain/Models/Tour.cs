﻿using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sims2023.Domain.Models
{
    public class Tour : ISerializable, INotifyPropertyChanged
    {
        public enum State { Created, Started, Finished, Cancelled, Interrupted }
        public enum Language { Serbian, English, German, French, Spanish, Italian, Chinese, Japanese }
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public Language GuideLanguage { get; set; }
        public int MaxGuestNumber { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        //Going to concatenate all of the KeyPoints into one string just so I can save it easier in csv 
        public string KeyPointsString { get; set; }
        public DateTime Start { get; set; }
        public int Length { get; set; }
        public State CurrentState { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int AvailableSpace { get; set; }
        public List<string> Pictures { get; set; }
        //Same principle as for KeyPoints, I'm going to concatenate all of the pictures urls into one string so I can save it easier
        public string PicturesString { get; set; }
        public int AttendedGuestsNumber { get; set; }
        public User Guide { get; set; }
        public Tour()
        {
            KeyPoints = new List<KeyPoint>();
            Pictures = new List<string>();
        }

        public Tour(int id, string name, Location location, string description, Language guideLanguage, int maxGuestNumber, string keyPointsString, DateTime start, int length, string picturesString, User guide)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            GuideLanguage = guideLanguage;
            MaxGuestNumber = maxGuestNumber;
            AvailableSpace = maxGuestNumber;
            KeyPointsString = keyPointsString;
            KeyPoints = new List<KeyPoint>();
            //
            string[] keyPointsStringArray = KeyPointsString.Split(",");
            foreach (string keyPoint in keyPointsStringArray)
            {
                //KeyPoints.Add(keyPoint);
            }
            //
            Start = start;
            Length = length;
            CurrentState = State.Created;
            PicturesString = picturesString;
            Pictures = new List<string>();
            string[] picturesStringArray = PicturesString.Split("!");
            foreach (string picture in picturesStringArray)
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
                KeyPointsString,
                Start.ToString(),
                Length.ToString(),
                PicturesString,
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
            LocationService locationService = new();
            Location = locationService.GetById(Convert.ToInt32(values[2]));
            Description = values[3];
            GuideLanguage = (Language)Enum.Parse(typeof(Language), values[4]);
            MaxGuestNumber = Convert.ToInt32(values[5]);
            AvailableSpace = Convert.ToInt32(values[6]);
            //
            KeyPointsString = values[7];
            string[] keyPointsStringArray = KeyPointsString.Split(",");
            foreach (string keyPoint in keyPointsStringArray)
            {
                //KeyPoints.Add(keyPoint);
            }
            //
            Start = DateTime.Parse(values[8]);
            Length = Convert.ToInt32(values[9]);
            PicturesString = values[10];
            string[] picturesStringArray = PicturesString.Split("!");
            foreach (string picture in picturesStringArray)
            {
                Pictures.Add(picture);
            }
            CurrentState = (State)Enum.Parse(typeof(State), values[11]);
            UserService userService = new();
            Guide = userService.GetById(Convert.ToInt32(values[12]));
            AttendedGuestsNumber = Convert.ToInt32(values[13]);
        }
    }
}
