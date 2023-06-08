using Sims2023.Serialization;
using System;

namespace Sims2023.Domain.Models
{
    public class SubTourRequest : ISerializable
    {
        public int Id { get; set; }
        public TourRequest TourRequest { get; set; }
        public ComplexTourRequest ComplexTourRequest { get; set; }
        public SubTourRequest()
        {

        }
        public SubTourRequest(TourRequest tourRequest, ComplexTourRequest complexTourRequest)
        {
            TourRequest = tourRequest;
            ComplexTourRequest = complexTourRequest;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourRequest.Id.ToString(),
                ComplexTourRequest.Id.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourRequest = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            ComplexTourRequest = new()
            {
                Id = Convert.ToInt32(values[2])
            };

        }

    }
}
