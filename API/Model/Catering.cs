namespace API.Model;
public class Catering
{
      [Key, ForeignKey("User")]
      public string CateringUserId { get; set; }
      public int Experience { get; set; }
      public byte[] DisplayImg { get; set; }
      [JsonIgnore]
      public User User { get; set; }

      public virtual ICollection<CateringFoodItem> CateringItems { get; set; }
      public virtual ICollection<Event> CateringEvents { get; set; }




}