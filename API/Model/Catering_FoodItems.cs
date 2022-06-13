

namespace API.Model;
public class Catering_FoodItems
{

      public string CateringId { get; set; }
      public Guid FoodItemsId { get; set; }

      [ForeignKey("CateringId")]
      [JsonIgnore]  
      public Catering Catering { get; set; }
      [ForeignKey("FoodItemsId")]
      [JsonIgnore]
      public CateringFoodItem FoodItem { get; set; }

}
