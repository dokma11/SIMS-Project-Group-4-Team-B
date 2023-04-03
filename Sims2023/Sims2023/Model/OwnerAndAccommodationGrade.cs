using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model
{
    public class OwnerAndAccommodationGrade : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public int GuestId { get; set; }

        public string Name { get; set; }
        public string Surrname { get; set; }

        public int Cleanliness { get; set; }
        public int Correct { get; set; }
        public int Communication { get; set; }
        public string Comment { get; set; }


        public OwnerAndAccommodationGrade() { }
        public OwnerAndAccommodationGrade(int id, int acmId, int guestid, int clean, int respect, int communication, string comment)
        {
            Id = id;
            AccommodationId = acmId;
            GuestId = guestid;
            Cleanliness = clean;
            Correct = respect;
            Communication = communication;
            Comment = comment;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                GuestId.ToString(),
                Cleanliness.ToString(),
                Correct.ToString(),
                Communication.ToString(),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            GuestId = Convert.ToInt32(values[2]);
            Cleanliness = Convert.ToInt32(values[3]);
            Correct = Convert.ToInt32(values[4]);
            Communication = Convert.ToInt32(values[5]);
            Comment = values[6];
        }
    }
}
