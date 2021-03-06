namespace API.Model;

public class Photographer
{
      [Key, ForeignKey("User")]
      public string PhotographerUserId { get; set; }
      public string PhotoTeamName { get; set; }
      public int Experience { get; set; }
      public string DisplayImg { get; set; }

      [JsonIgnore]
      public virtual User User { get; set; }
      public virtual ICollection<Event> PhotoGrapherEvents { get; set; }
}

public class photographerDTO
{
      public string PhotographerUserId { get; set; }
      public string PhotoTeamName { get; set; }

      public int Experience { get; set; }
      public string DisplayImg { get; set; }
}

public class PhotographerDTO2
{

      public string UserId { get; set; }
      public string Name { get; set; }
      public string PhotoTeamName { get; set; }
      public string ProfileImg { get; set; }
      public string DisplayImg { get; set; }
      public string Phone { get; set; }
      public string Email { get; set; }
      public string Location { get; set; }
      public int Experience { get; set; }

}