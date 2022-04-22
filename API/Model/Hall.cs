using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
      public class Hall
      {
            [Key]
            public int HallID { get; set; }
            public string Hall_Name { get; set; }
            public string Location { get; set; }
            public string Description { get; set; }
            public virtual ICollection<Event> HallEvents { get; set; }
            public virtual ICollection<Review> EventReviews { get; set; }

      }

      public class HallDTO
      {
            public int HallID { get; set; }
            public string Hall_Name { get; set; }
            public string Location { get; set; }
            public string Description { get; set; }
      }

}