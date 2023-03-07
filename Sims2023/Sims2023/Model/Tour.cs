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
        //For now Location is going to be just a normal string
        public string Location { get; set; }
        public string Description { get; set; }
        public enum Language { Serbian, English, German }
        public Language GuideLanguage { get; set; }
        public int MaxGuestNumber { get; set; }
        //For now KeyPoints are going to be just a usual string
        public string KeyPoints { get; set; }
        public DateTime Start { get; set; }
        public int Length { get; set; }
        public enum State { Created, Started, Ended, Cancelled }
        public State CurrentState { get; set; }
        //For now pictures are going to be a list of strings
        public List<String> Pictures { get; set; }
        public Tour() { }

        public Tour(int id, string name, string location, string description, Language guideLanguage, int maxGuestNumber, string keyPoints, DateTime start, int length)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            GuideLanguage = guideLanguage;
            MaxGuestNumber = maxGuestNumber;
            KeyPoints = keyPoints;
            Start = start;
            Length = length;
            //CurrentState = State.Created;           Probably don't need it in a file, will add if it is necessary
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Location, Description, GuideLanguage.ToString(), MaxGuestNumber.ToString(), KeyPoints, Start.ToString(), Length.ToString()};  //Didn't add CurrentState will later on if necessary
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = values[2];
            Description = values[3];
            GuideLanguage = (Language)Enum.Parse(typeof(Language), values[4]);          //Need to test if it works
            MaxGuestNumber = Convert.ToInt32(values[5]);
            KeyPoints = values[6];
            Start = DateTime.Parse(values[7]);                                          //Need to test if it works
            Length = Convert.ToInt32(values[8]);
            //CurrentState = (State)Enum.Parse(typeof(State), values[9]);                 Probably don't need it in a file, will make sure later on
        }
    }
}
