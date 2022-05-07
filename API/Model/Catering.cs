namespace API.Model;
public class Catering
{
      [Key, ForeignKey("User")]
      public string CatererUserId { get; set; }
      public string CatererTeamName { get; set; }
      public int Experience { get; set; }
      public byte[] DisplayImg { get; set; }
      [JsonIgnore]
      public User User { get; set; }
      public virtual ICollection<Catering_FoodItems> CateringItems { get; set; }
      public virtual ICollection<Event> CateringEvents { get; set; }
}

public class CateringDTO
{

      public string CatererUserId { get; set; }
      public string CatererTeamName { get; set; }
      public int Experience { get; set; }
      public byte[] DisplayImg { get; set; }
}

public class CateringDTO2
{
      public string UserId { get; set; }
      public string Name { get; set; }
      public byte[] ProfileImg { get; set; }
      public byte[] DisplayImg { get; set; }
      public string Phone { get; set; }
      public string Email { get; set; }
      public string Location { get; set; }
      public string CatererTeamName { get; set; }
      public int Experience { get; set; }
}