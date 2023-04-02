using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sims2023.Model
{
    public class KeyPoint : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public enum State { Visited, NotVisited, BeingVisited }
        public State CurrentState { get; set; }
        public List<int> ShowedGuestsIds { get; set; }
        //Just so I can save it to file, I will concatenate all of them into one string
        public string ShowedGuestsIdsString { get; set; }
        public int ToursId { get; set; }
        public KeyPoint()
        {
            ShowedGuestsIds = new List<int>();
        }
        public KeyPoint(int id, string name, State currentState, string showedGuestsIdsString, int toursId)
        {
            Id = id;
            Name = name;
            CurrentState = currentState;
            string[] showedGuestsIdsStringArray = showedGuestsIdsString.Split(" ");
            foreach (var instanceString in showedGuestsIdsStringArray)
            {
                int instance = int.Parse(instanceString);
                ShowedGuestsIds.Add(instance);
            }
            ToursId = toursId;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues = 
            { 
                Id.ToString(), 
                Name, 
                CurrentState.ToString(), 
                ShowedGuestsIdsString, 
                ToursId.ToString() 
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            CurrentState = (State)Enum.Parse(typeof(State), values[2]);
            ShowedGuestsIdsString = values[3];
            if (!String.IsNullOrEmpty(ShowedGuestsIdsString)){
                
                string[] showedGuestsIdsStringArray = ShowedGuestsIdsString.Split(" ");
                foreach (var instanceString in showedGuestsIdsStringArray)
                {
                    int instance = Convert.ToInt32(instanceString);
                    ShowedGuestsIds.Add(instance);
                }

            }
            ToursId = Convert.ToInt32(values[4]);
        }
    }
}
