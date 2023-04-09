using Sims2023.Controller;
using Sims2023.Serialization;
using System;
using System.ComponentModel;

namespace Sims2023.Model
{
    public class AccommodationGrade : ISerializable, INotifyPropertyChanged 
    {

        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public User Guest{ get; set; }

        public int Cleanliness { get; set; }
        public int Comfort { get; set; }
        public int Location { get; set; }
        public int Owner { get; set; }
        public int ValueForMoney { get; set; }
        public string Comment { get; set; }


        public AccommodationGrade() { }
        public AccommodationGrade(int id, int cleanliness, int comfort, int location, int owner, int valueForMoney,string comment,Accommodation accommodation,User user)
        {
            Id = id;
            Cleanliness = cleanliness;
            Comfort = comfort;
            Location = location;
            Owner = owner;
            ValueForMoney = valueForMoney;
            Comment = comment;
            Accommodation = accommodation;
            Guest = user;

        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                Guest.Id.ToString(),
                Cleanliness.ToString(),
                Comfort.ToString(),
                Location.ToString(),
                Owner.ToString(),
                ValueForMoney.ToString(),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation accommodation = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            AccommodationController accommodationController = new();
            Accommodation = accommodationController.GetById(accommodation.Id);
            User guest = new()
            {
                Id = Convert.ToInt32(values[2])
            };
            UserController userController = new();
            Guest = userController.GetById(guest.Id);
            
            Cleanliness = Convert.ToInt32(values[3]);
            Comfort = Convert.ToInt32(values[4]);
            Location = Convert.ToInt32(values[5]);
            Owner = Convert.ToInt32(values[6]);
            ValueForMoney = Convert.ToInt32(values[7]);
            Comment = values[8];
        }

        

    }
}
