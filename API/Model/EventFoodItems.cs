namespace API.Model;
public class EventFoodItems
{
      public Guid EventId { get; set; }
      public Guid FoodItemsId { get; set; }

      [ForeignKey("EventId")]
      [JsonIgnore]
      public Event Events { get; set; }
      [ForeignKey("FoodItemsId")]
      [JsonIgnore]
      public CateringFoodItem FoodItem { get; set; }

}
