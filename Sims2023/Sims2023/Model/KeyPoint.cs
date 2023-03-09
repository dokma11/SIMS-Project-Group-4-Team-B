using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model
{
    public class KeyPoint: ISerializable, INotifyPropertyChanged
    {
        public int id {  get; set; }
        public string name { get; set; }
        public enum State { Visited, NotVisited, BeingVisited}
        public State currentState { get; set; }
        //For now (while guest2 has not finished his work) I will be using only "simulated" ids from guests, will se if I'll continue like that
        public List<int> showedGuestsIds { get; set; }
        //Just so I can save it to file
        public string showedGuestsIdsString { get; set; }
        public int toursId { get; set; }
        public KeyPoint()
        {
            showedGuestsIds = new List<int>();
        }
        public KeyPoint(int id, string name, State currentState, string showedGuestsIdsString, int toursId) 
        {
            this.id = id;
            this.name = name;
            this.currentState = currentState;
            string[] showedGuestsIdsStringArray = showedGuestsIdsString.Split(",");
            foreach (var instanceString in showedGuestsIdsStringArray)
            {
                int instance = int.Parse(instanceString);
                showedGuestsIds.Add(instance);
            }
            this.toursId = toursId;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues = { id.ToString(), name, currentState.ToString(), showedGuestsIdsString, toursId.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            name = values[1];
            currentState = (State)Enum.Parse(typeof(State), values[2]);
            showedGuestsIdsString = values[3];
            toursId = Convert.ToInt32(values[4]);
        }
    }
}
