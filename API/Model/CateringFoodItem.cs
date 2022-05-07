

namespace API.Model;
public class CateringFoodItem
{
      [Key]
      public Guid ItemId { get; set; }
      public string Item { get; set; }
      public byte[] ItemImg { get; set; }
      public virtual ICollection<Catering_FoodItems> CateringItems { get; set; }
      public virtual ICollection<EventFoodItems> EventFoodItems { get; set; }

}