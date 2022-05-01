using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Model
{
      public class Review
      {
            [Key, ForeignKey("EventReviews")]
            public Guid ReviewID { get; set; }

            public string HallReviewContent { get; set; }
            public int HallRating { get; set; }

            public string PhotographerReviewContent { get; set; }
            public int PhotoRating { get; set; }

            public string CateringReviewContent { get; set; }
            public int CateringRating { get; set; }

            [JsonIgnore]
            public virtual Event EventReviews { get; set; }
      }
}