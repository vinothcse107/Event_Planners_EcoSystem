using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
      public class BidAccept
      {
            public Guid EventId { get; set; }
            public string BidderId { get; set; }
      }
}