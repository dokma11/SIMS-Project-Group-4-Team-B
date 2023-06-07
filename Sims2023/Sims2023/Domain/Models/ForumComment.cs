using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.Models
{
    public class ForumComment : ISerializable
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Forum Forum { get; set; }
        public string Comment { get; set; }
        public bool Special { get; set; }

        public ForumComment() { }

        public ForumComment(int id, User user, Forum forum, string comment, bool special)
        {
            Id = id;
            User = user;
            Forum = forum;
            Comment = comment;
            Special = special;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            User.Id.ToString(),
            Forum.Id.ToString(),
            Comment,
            Special.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            Forum = new()
            {
                Id = Convert.ToInt32(values[2])
            };
            Comment = values[3];
            Special = Convert.ToBoolean(values[4]);
        }
    }

}
