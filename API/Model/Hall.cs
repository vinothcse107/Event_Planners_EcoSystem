using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model;

public class Hall
{


      [Key]
      public Guid HallID { get; set; }
      [ForeignKey("User")]
      public string OwnerUsername { get; set; }
      [JsonIgnore]
      public User User { get; set; }
      public string DisplayImg { get; set; }
      public string HallName { get; set; }
      public string Location { get; set; }
      public string Description { get; set; }
      public virtual ICollection<Event> HallEvents { get; set; }
}

public class HallDTO
{
      public Guid HallID { get; set; }
      public string OwnerUsername { get; set; }
      public string HallName { get; set; }
      public string Location { get; set; }
      public string Description { get; set; }

}