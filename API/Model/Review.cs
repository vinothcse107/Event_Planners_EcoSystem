using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Model
{
      public class Review
      {
            [Key]
            public int ID { get; set; }
            public string ReviewContent { get; set; }



            [ForeignKey("EventReviews")]
            public int EventID { get; set; }
            [JsonIgnore]
            public Event EventReviews { get; set; }



            [ForeignKey("HallReviews")]
            public int HallID { get; set; }
            [JsonIgnore]
            public Hall HallReviews { get; set; }
      }
}