using Sims2023.Serialization;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sims2023.Domain.Models
{
    public class User : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public enum Type { Owner, Guest1, Guide, Guest2 }
        public Type UserType { get; set; }

        public bool superOwner { get; set; }
        //Treba i sliku dodati
        public bool SuperGuide { get; set; }
        public bool SuperGuest1 { get; set; }
        public int Guest1Points { get; set; }
        public DateTime DateOfBecomingSuperGuest { get; set; }
        public bool AbleToLogIn { get; set; }
        public User() { }

        public User(int id, string username, string password, string name, string surname, int age, string phoneNumber, string email, Type userType, bool superOwner, bool superGuide, bool superGuest1, int guest1Points, DateTime dateOfBecomingSuperGuest)
        {
            Id = id;
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            Age = age;
            PhoneNumber = phoneNumber;
            Email = email;
            UserType = userType;
            this.superOwner = superOwner;
            SuperGuide = superGuide;
            SuperGuest1 = superGuest1;
            Guest1Points = guest1Points;
            DateOfBecomingSuperGuest = dateOfBecomingSuperGuest;
            AbleToLogIn = true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Username,
                Password,
                Name,
                Surname,
                Age.ToString(),
                PhoneNumber,
                Email,
                UserType.ToString(),
                superOwner.ToString(),
                SuperGuide.ToString(),
                SuperGuest1.ToString(),
                Guest1Points.ToString(),
                DateOfBecomingSuperGuest.ToString(),
                AbleToLogIn.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Name = values[3];
            Surname = values[4];
            Age = Convert.ToInt32(values[5]);
            PhoneNumber = values[6];
            Email = values[7];
            UserType = (Type)Enum.Parse(typeof(Type), values[8]);
            superOwner = Convert.ToBoolean(values[9]);
            SuperGuide = Convert.ToBoolean(values[10]);
            SuperGuest1 = Convert.ToBoolean(values[11]);
            Guest1Points = Convert.ToInt32(values[12]);
            
            AbleToLogIn = Convert.ToBoolean(values[14]);
        }
    }
}
