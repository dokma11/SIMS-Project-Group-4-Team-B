﻿using Sims2023.Serialization;
using System;
using System.ComponentModel;

namespace Sims2023.Domain.Models
{
    public class Location : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Location() { }
        public Location(int id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                City,
                Country
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }
    }
}