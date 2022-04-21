using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Model
{
      public class Event
      {
            [Key]
            public int ID { get; set; }
            public string EventName { get; set; }
            public DateTime EventTime { get; set; }


            [ForeignKey("Users")]
            public string User_ID { get; set; }
            [JsonIgnore]
            public User Users { get; set; }


            [ForeignKey("Halls")]
            public int Hall_ID { get; set; }
            [JsonIgnore]
            public Hall Halls { get; set; }

      }
      public class EventDTO
      {
            [Key]
            public int ID { get; set; }
            public string EventName { get; set; }
            public DateTime EventTime { get; set; }
            public string User_ID { get; set; }
            public int Hall_ID { get; set; }


      }
}