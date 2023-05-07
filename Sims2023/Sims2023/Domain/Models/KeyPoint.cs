using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Sims2023.Domain.Models
{
    public enum KeyPointsState { Visited, NotVisited, BeingVisited }

    public class KeyPoint : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public KeyPointsState CurrentState { get; set; }
        public List<int> PresentGuestsIds { get; set; }
        //Just so I can save it to file, I will concatenate all of them into one string
        public string PresentGuestsIdsString { get; set; }
        public int PresentGuestsNumber { get; set; }
        public Tour Tour { get; set; }

        public KeyPoint()
        {
            PresentGuestsIds = new List<int>();
        }

        public KeyPoint(int id, string name, KeyPointsState currentState, string showedGuestsIdsString, Tour tour, int presentGuestsNumber)
        {
            Id = id;
            Name = name;
            CurrentState = currentState;
            List<int> ShowedGuestsIds = showedGuestsIdsString.Split(",").Select(int.Parse).ToList();
            Tour = tour;
            PresentGuestsNumber = presentGuestsNumber;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                CurrentState.ToString(),
                PresentGuestsIdsString,
                PresentGuestsNumber.ToString(),
                Tour.Id.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            CurrentState = (KeyPointsState)Enum.Parse(typeof(KeyPointsState), values[2]);
            PresentGuestsIdsString = values[3];
            if (!string.IsNullOrEmpty(PresentGuestsIdsString))
            {
                string[] showedGuestsIdsStringArray = PresentGuestsIdsString.Split(",");
                foreach (var instanceString in showedGuestsIdsStringArray)
                {
                    int instance = Convert.ToInt32(instanceString);
                    PresentGuestsIds.Add(instance);
                }
            }
            PresentGuestsNumber = Convert.ToInt32(values[4]);
            TourService tourService = new();
            Tour = tourService.GetById(Convert.ToInt32(values[5]));
        }
    }
}
