using System.ComponentModel.DataAnnotations;

namespace API.Model
{
      public class Reviews
      {
            [Key]
            public int ID { get; set; }
            public User Users { get; set; }
            public int UserId { get; set; }
            public string Review { get; set; }
      }
}