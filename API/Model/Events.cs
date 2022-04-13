using System.ComponentModel.DataAnnotations;

namespace API.Model
{
      public class Events
      {
            [Key]
            public int ID { get; set; }
            [Required]
            public User Users { get; set; }
            public int UserId { get; set; }
      }
}