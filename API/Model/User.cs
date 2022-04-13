using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.Model
{
      public class User
      {
            public string Name { get; set; }
            [Key]
            public string Username { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Location { get; set; }
            public string Role { get; set; }
            public byte[] PasswordHash { get; set; }
            public byte[] PasswordSalt { get; set; }
            public ICollection<Events> UserEvents { get; set; }
            public ICollection<Reviews> User_Event_Reviews { get; set; }

      }
}
