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
        public User() { }

        public User(int id, string username, string password, string name, string surname, int age, string phoneNumber, string email, Type userType, bool superOwner)
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
                superOwner.ToString()
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
            Age = values[5];
            PhoneNumber = values[6];
            Email = values[7];
            UserType = (Type)Enum.Parse(typeof(Type), values[8]);
            superOwner = Convert.ToBoolean(values[9]);
        }
    }
}
