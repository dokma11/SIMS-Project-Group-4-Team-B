using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sims2023.Model
{
    public class Tour : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public enum Language { Serbian, English, German, French, Spanish, Italian, Chinese, Japanese }
        public Language GuideLanguage { get; set; }
        public int MaxGuestNumber { get; set; }
        public List<string> KeyPoints { get; set; }
        //Going to concatenate all of the KeyPoints into one string just so I can save it easier in csv 
        public string KeyPointsString { get; set; }
        public DateTime Start { get; set; }
        public int Length { get; set; }
        public enum State { Created, Started, Finished, Cancelled }
        public State CurrentState { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int AvailableSpace { get; set; }
        public List<string> Pictures { get; set; }
        //Same principle as for KeyPoints, I'm going to concatenate all of the pictures urls into one string so I can save it easier
        public string PicturesString { get; set; }
        public Tour()
        {
            KeyPoints = new List<string>();
            Pictures = new List<string>();
        }

        public Tour(int id, string name, int locationId, string description, Language guideLanguage, int maxGuestNumber, string keyPointsString, DateTime start, int length, string picturesString)
        {
            Id = id;
            Name = name;
            LocationId = locationId;
            Description = description;
            GuideLanguage = guideLanguage;
            MaxGuestNumber = maxGuestNumber;
            AvailableSpace = maxGuestNumber;
            KeyPointsString = keyPointsString;
            KeyPoints = new List<string>();
            string[] keyPointsStringArray = this.KeyPointsString.Split(",");
            foreach (string keyPoint in keyPointsStringArray)
            {
                KeyPoints.Add(keyPoint);
            }
            Start = start;
            Length = length;
            CurrentState = State.Created;
            PicturesString = picturesString;
            Pictures = new List<string>();
            string[] picturesStringArray = this.PicturesString.Split(",");
            foreach (string picture in picturesStringArray)
            {
                Pictures.Add(picture);
            }
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
                LocationId.ToString(),
                Description,
                GuideLanguage.ToString(),
                MaxGuestNumber.ToString(),
                AvailableSpace.ToString(),
                KeyPointsString,
                Start.ToString(),
                Length.ToString(),
                PicturesString,
                CurrentState.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            Description = values[3];
            GuideLanguage = (Language)Enum.Parse(typeof(Language), values[4]);
            MaxGuestNumber = Convert.ToInt32(values[5]);
            AvailableSpace = Convert.ToInt32(values[6]);
            KeyPointsString = values[7];
            string[] keyPointsStringArray = this.KeyPointsString.Split(",");
            foreach (string keyPoint in keyPointsStringArray)
            {
                KeyPoints.Add(keyPoint);
            }
            Start = DateTime.Parse(values[8]);
            Length = Convert.ToInt32(values[9]);
            PicturesString = values[10];
            string[] picturesStringArray = this.PicturesString.Split(",");
            foreach (string picture in picturesStringArray)
            {
                Pictures.Add(picture);
            }
            CurrentState = (State)Enum.Parse(typeof(State), values[11]);
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        return "Popunite polje za naziv";
                    }
                }
                else if (columnName == "LocationId")
                {
                    if (string.IsNullOrEmpty(LocationId.ToString()))
                    {
                        return "Popunite polja za lokaciju";
                    }
                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                    {
                        return "Popunite polje za opis";
                    }
                }
                else if (columnName == "GuideLanguage")
                {
                    if (string.IsNullOrEmpty(GuideLanguage.ToString()))
                    {
                        return "Popunite polje za jezik";
                    }
                }
                else if (columnName == "MaxGuestNumber")
                {
                    if (MaxGuestNumber < 1)
                    {
                        return "Popunite polje za maksimalan broj gostiju. Mora biti makar jedan gost da bi se tura odrzala";
                    }
                }
                else if (columnName == "Start")
                {
                    if (string.IsNullOrEmpty(Start.ToString()))
                    {
                        return "Popunite polje za pocetak ture";
                    }
                }
                else if (columnName == "Length")
                {
                    if (Length < 1)
                    {
                        return "Popunite polje za trajanje. Tura treba da traje minimum jedan sat";
                    }
                }
                else if (columnName == "PicturesString")
                {
                    if (string.IsNullOrEmpty(PicturesString))
                    {
                        return "Popunite polje za fotografije";
                    }
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties =
        {
            "Name",
            "LocationId",
            "Description",
            "GuideLanguage",
            "MaxGuestNumber",
            "Start",
            "Length",
            "PicturesString"
        };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
