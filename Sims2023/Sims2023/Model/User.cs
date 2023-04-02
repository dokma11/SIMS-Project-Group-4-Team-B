using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model
{
    public class User: ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public enum Type { Owner, Guest1, Guide, Guest2 }
        public Type UserType { get; set; }
        //Treba i sliku dodati
        public User() { }

        public User(int id, string userName, string password, string name, string surname, string phoneNumber, string email, Type userType)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            Email = email;
            UserType = userType;
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
                UserName,
                Password,
                Name, 
                Surname,
                PhoneNumber,
                Email,
                UserType.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserName = values[1];
            Password = values[2];
            Name = values[3];
            Surname = values[4];
            PhoneNumber = values[5];
            Email = values[6];
            UserType = (Type)Enum.Parse(typeof(Type), values[7]);
        }
    }
}
