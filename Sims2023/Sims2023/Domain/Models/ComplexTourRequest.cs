﻿using Sims2023.Serialization;
using System;

namespace Sims2023.Domain.Models
{
    public enum ComplexRequestsState { OnHold, Invalid, Accepted }
    public class ComplexTourRequest : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ComplexRequestsState CurrentState { get; set; }
        public User Guest { get; set; }
        public string Date { get; set; }

        public ComplexTourRequest()
        {

        }

        public ComplexTourRequest(int id,string name,User guest,string date)
        {
            Id = id;
            Name = name;
            CurrentState = ComplexRequestsState.OnHold;
            Guest = guest;
            Date = date;

        }
        public ComplexTourRequest(string name, User guest)
        {
            Name = name;
            Guest = guest;
            CurrentState = ComplexRequestsState.OnHold;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                CurrentState.ToString(),
                Guest.Id.ToString(),
                Date
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            CurrentState = (ComplexRequestsState)Enum.Parse(typeof(ComplexRequestsState), values[2]);
            Guest = new()
            {
                Id = Convert.ToInt32(values[3])
            };
            Date = values[4];
            
        }
    }
}
