using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
      public class EventBidding
      {
            [Key]
            public Guid BidId { get; set; }

            // Event ID
            [ForeignKey("Event")]
            public Guid EventId { get; set; }
            [JsonIgnore]
            public Event Event { get; set; }
            // Bidder UserId 
            public string Role { get; set; }
            [ForeignKey("User")]
            public string BidderId { get; set; }
            [JsonIgnore]
            public virtual User User { get; set; }
            public string BidDescription { get; set; }
      }
}