using System.ComponentModel.DataAnnotations;

namespace API.Model
{
      public class Review
      {
            [Key]
            public int ID { get; set; }
            public int EventID { get; set; }
            public int HallID { get; set; }
            public string ReviewContent { get; set; }
      }
}