using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Model
{
      public class Event
      {
            [Key]
            public Guid EventID { get; set; }
            public string EventName { get; set; }
            public DateTime EventTime { get; set; }

            // Client UserId
            // => FK UserName => User.cs
            [ForeignKey("Users")]
            public string User_ID { get; set; }
            [JsonIgnore]
            public User Users { get; set; }


            // => FK HallId => Hall.cs
            [ForeignKey("Halls")]
            public Guid Hall_ID { get; set; }
            [JsonIgnore]
            public Hall Halls { get; set; }



            // => FK UserName => Photographer.cs
            [ForeignKey("Photographer")]
            public string PhotoGrapherID { get; set; }
            [JsonIgnore]
            public Photographer Photographer { get; set; }


            [ForeignKey("Catering")]
            public string CateringId { get; set; }
            [JsonIgnore]
            public Catering Catering { get; set; }

            [JsonIgnore]
            public virtual Review EventReviews { get; set; }

      }
}