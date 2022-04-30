namespace API.Model;

public class Photographer
{
      [Key, ForeignKey("User")]
      public string PhotographerUserId { get; set; }
      public int Experience { get; set; }
      public byte[] DisplayImg { get; set; }

      [JsonIgnore]
      public virtual User User { get; set; }
      public virtual ICollection<Event> PhotoGrapherEvents { get; set; }
}
