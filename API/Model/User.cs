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
            public string ProfileImg { get; set; }
            [Key]
            public string Username { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Location { get; set; }
            public string Role { get; set; }
            public byte Status { get; set; }
            public byte[] PasswordHash { get; set; }
            public byte[] PasswordSalt { get; set; }


            public virtual ICollection<Event> UserEvents { get; set; }
            public virtual ICollection<PhotoGallery> Photos { get; set; }
            public virtual ICollection<Hall> HallOwnerIds { get; set; }
            public virtual ICollection<EventBidding> EventBids { get; set; }
            public virtual Photographer PhotoGrapherIds { get; set; }
            public virtual Catering CateringIds { get; set; }


      }
}
