using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Model
{
      public class Review
      {
            [Key]
            public Guid ID { get; set; }

            public string HallReviewContent { get; set; }
            public int HallRating { get; set; }

            public string PhotographerReviewContent { get; set; }
            public int PhotoRating { get; set; }

            public string CateringReviewContent { get; set; }
            public int CateringRating { get; set; }


            [ForeignKey("EventReviews")]
            public Guid EventID { get; set; }
            [JsonIgnore]
            public Event EventReviews { get; set; }
      }
}