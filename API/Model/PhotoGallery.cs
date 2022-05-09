namespace API.Model;

public class PhotoGallery
{
      [Key]
      public Guid PhotoId { get; set; }
      public string Photo { get; set; }

      [ForeignKey("User")]
      public string Username { get; set; }
      [JsonIgnore]
      public User User { get; set; }

}
