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
        public int id { get; set; }
        public string name { get; set; }
        public int locationId { get; set; }
        public string description { get; set; }
        public enum Language { Serbian, English, German, French, Spanish, Italian, Chinese, Japanese }
        public Language guideLanguage { get; set; }
        public int maxGuestNumber { get; set; }
        public List<KeyPoint> keyPoints { get; set; }
        //Going to concatenate all of the KeyPoints into one string just so I can save it easier in csv 
        public string keyPointsString { get; set; }
        public DateTime start { get; set; }
        public int length { get; set; }
        public enum State { Created, Started, Ended, Cancelled }
        public State currentState { get; set; }
        public List<String> pictures { get; set; }
        //Same principle as for KeyPoints, I'm going to concatenate all of the urls into one string so I can save it easier
        public string picturesString { get; set; }
        public Tour() 
        {
            keyPoints = new List<KeyPoint>();
            pictures = new List<String>();
        }

        public Tour(int id, string name, int locationId, string description, Language guideLanguage, int maxGuestNumber, string keyPointsString, DateTime start, int length, string picturesString)
        {
            this.id = id;
            this.name = name;
            this.locationId = locationId;
            this.description = description;
            this.guideLanguage = guideLanguage;
            this.maxGuestNumber = maxGuestNumber;
            this.keyPointsString = keyPointsString;
            keyPoints = new List<KeyPoint>();
            /*string[] keyPointsStringArray = this.keyPointsString.Split(",");
            foreach(string str in keyPointsStringArray) 
            {
                KeyPoint newKeyPoint = new KeyPoint();
                newKeyPoint.name = str;
                keyPoints.Add(newKeyPoint);
            }*/
            this.start = start;
            this.length = length;
            currentState = State.Created;
            this.picturesString = picturesString;
            pictures = new List<String>();
            string[] picturesStringArray = this.picturesString.Split(",");
            foreach (string str in picturesStringArray)
            {
                pictures.Add(str);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues = { id.ToString(), name, locationId.ToString(), description, guideLanguage.ToString(), maxGuestNumber.ToString(), keyPointsString, start.ToString(), length.ToString(), picturesString};  //Didn't add CurrentState will later on if necessary
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            name = values[1];
            locationId = Convert.ToInt32(values[2]);
            description = values[3];
            guideLanguage = (Language)Enum.Parse(typeof(Language), values[4]);          
            maxGuestNumber = Convert.ToInt32(values[5]);
            keyPointsString = values[6];
            start = DateTime.Parse(values[7]);                                          
            length = Convert.ToInt32(values[8]);
            picturesString = values[9];
            //CurrentState = (State)Enum.Parse(typeof(State), values[10]);                 Probably don't need it in a file, will make sure later on
        }
    }
}
