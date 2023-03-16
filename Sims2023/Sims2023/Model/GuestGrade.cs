using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model
{
    public class GuestGrade : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public int GuestId { get; set; }

        public int Cleanliness { get; set; }
        public int RespectRules { get; set; }
        public int Communication { get; set; }
        public string Comment { get; set; }


        public GuestGrade() { }
        public GuestGrade(int id, int acmId, int guestid, int clean, int respect, int communication, string comment)
        {
            Id = id;
            AccommodationId = acmId;
            GuestId = guestid;
            Cleanliness = clean;
            RespectRules = respect;
            Communication = communication;
            Comment = comment;
        }
       

        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), AccommodationId.ToString(), GuestId.ToString(), Cleanliness.ToString(), RespectRules.ToString(), Communication.ToString(), Comment };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            GuestId = int.Parse(values[2]);
            Cleanliness = int.Parse(values[3]);
            RespectRules = int.Parse(values[4]);
            Communication= int.Parse(values[5]);
            Comment = values[6];
        }



    }
}
