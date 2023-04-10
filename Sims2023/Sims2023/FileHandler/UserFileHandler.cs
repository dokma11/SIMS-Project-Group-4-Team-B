using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.FileHandler
{
    public class UserFileHandler
    {
        private List<User> _users;
        private readonly Serializer<User> _serializer;
        private const string FilePath = "../../../Resources/Data/users.csv";

        public UserFileHandler()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public User GetById(int id)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> Load()
        {
            _users = _serializer.FromCSV(FilePath);
            return _users;
        }

        public void Save(List<User> users)
        {
            _serializer.ToCSV(FilePath, users);
        }
    }
}
