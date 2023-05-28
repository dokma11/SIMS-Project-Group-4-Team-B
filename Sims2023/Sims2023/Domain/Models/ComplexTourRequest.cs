using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Serialization;

namespace Sims2023.Domain.Models
{
    public enum ComplexRequestsState { OnHold, Invalid, Accepted }
    public class ComplexTourRequest: ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TourRequests { get; set; }
        public List<TourRequest> TourRequestList { get; set; }
        public ComplexRequestsState CurrentState { get; set; }
        public User Guest { get; set; }

        public ComplexTourRequest()
        {

        }

        public ComplexTourRequest(int id,string name,string tourRequestString,User guest)
        {
            Id = id;
            Name = name;
            TourRequests = tourRequestString;
            CurrentState = ComplexRequestsState.OnHold;
            Guest = guest;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                TourRequests,
                Guest.Id.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            TourRequests = values[2];
            CurrentState = (ComplexRequestsState)Enum.Parse(typeof(ComplexRequestsState), values[3]);
            Guest = new()
            {
                Id = Convert.ToInt32(values[4])
            };
            
        }
    }
}
