using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model
{
    public class Tour: ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public enum Language { Serbian, English, German, French, Spanish, Italian, Chinese, Japanese }
        public Language GuideLanguage { get; set; }
        public int MaxGuestNumber { get; set; }
        public List<String> KeyPoints { get; set; }
        //Going to concatenate all of the KeyPoints into one string just so I can save it easier in csv 
        public string KeyPointsString { get; set; }
        public DateTime Start { get; set; }
        public int Length { get; set; }
        public enum State { Created, Started, Ended, Cancelled }
        public State CurrentState { get; set; }
        public List<String> Pictures { get; set; }
        //Same principle as for KeyPoints, I'm going to concatenate all of the urls into one string so I can save it easier
        public string PicturesString { get; set; }
        public Tour() 
        {
            KeyPoints = new List<String>();
            Pictures = new List<String>();
        }

        public Tour(int id, string name, int locationId, string description, Language guideLanguage, int maxGuestNumber, string keyPointsString, DateTime start, int length, string picturesString)
        {
            Id = id;
            Name = name;
            LocationId = locationId;
            Description = description;
            GuideLanguage = guideLanguage;
            MaxGuestNumber = maxGuestNumber;
            KeyPointsString = keyPointsString;
            KeyPoints = new List<String>();
            string[] keyPointsStringArray = KeyPointsString.Split(",");
            foreach(string str in keyPointsStringArray) 
            {
                KeyPoints.Add(str);
            }
            Start = start;
            Length = length;
            CurrentState = State.Created;
            PicturesString = picturesString;
            Pictures = new List<String>();
            string[] picturesStringArray = PicturesString.Split(",");
            foreach (string str in picturesStringArray)
            {
                Pictures.Add(str);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, LocationId.ToString(), Description, GuideLanguage.ToString(), MaxGuestNumber.ToString(), KeyPointsString, Start.ToString(), Length.ToString(), PicturesString};  //Didn't add CurrentState will later on if necessary
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
            KeyPointsString = values[6];
            Start = DateTime.Parse(values[7]);                                          
            Length = Convert.ToInt32(values[8]);
            PicturesString = values[9];
            //CurrentState = (State)Enum.Parse(typeof(State), values[10]);                 Probably don't need it in a file, will make sure later on
        }
    }
}
