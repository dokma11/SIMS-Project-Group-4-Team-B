using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.AvalonDock.Themes;

namespace Sims2023.Domain.Models
{
    public class FakeComment : ISerializable
    {
        public User Owner { get; set; }
        public ForumComment Comment { get; set; }

        public FakeComment() { }
        public FakeComment(User user, ForumComment comment) 
        {
           Owner = user;
           Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Owner.Id.ToString(),
            Comment.Id.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Owner = new()
            {
                Id = Convert.ToInt32(values[0])
            };
            Comment = new()
            {
                Id = Convert.ToInt32(values[1])
            };
        }
   }
}
