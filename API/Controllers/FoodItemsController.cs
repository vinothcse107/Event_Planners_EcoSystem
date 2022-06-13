namespace API.Controllers;
[Route("[controller]")]
public class FoodItemsController : Controller
{
      private readonly Context _context;
      public FoodItemsController(Context context)
      {
            _context = context;
      }

      #region CateringFoodItems Admin Controls

      /*
      * Show All the Avilable Food Items
      * A New Catering Service Can able to Choose From the List
      */

      [HttpGet("Get_All_FoodItems")]
      public async Task<IActionResult> GetFoodItems()
      {
            var y = await (_context.CateringFoodItems
                        .Select(s => new
                        {
                              ItemId = s.ItemId,
                              ItemName = s.Item,
                              Img = s.ItemImg
                        }).ToListAsync());
            return Ok(y);
      }

      /*
      * Post New Food Item (Admin Controlled)
      */

      [HttpPost("Post_New_Food_Item")]
      public async Task<IActionResult> PostNewFoods([FromBody] CateringFoodItem Foods)
      {
            Foods.ItemId = Guid.NewGuid(); // Assign New Guid For Food Item
            await _context.CateringFoodItems.AddAsync(Foods);
            await _context.SaveChangesAsync();
            return Ok("New Food Items Added");
      }

      [HttpDelete("Delete_FoodItem")]
      public async Task<IActionResult> DeleteFoodItem(string FoodId)
      {
            var f = await _context.CateringFoodItems
                        .AsNoTracking()
                        .FirstAsync(w => w.ItemId == new Guid(FoodId));
            if (f != null)
            {
                  _context.CateringFoodItems.Remove(f);
                  _context.SaveChanges();
                  return Ok("Item Removed");
            }
            else
            {
                  return BadRequest("Invalid Request");
            }
      }

      #endregion

      /*
      * Controls With in Businees Limits By Adding Food 
      * & Deleting From Menu
      */
      #region Businees Controls
      /*
      * Post Various Food Items In Catering_FoodItems("Many_To_Many Relationship")
      * Selecting Food From Bunch Of FoodList For Catering Service
      */
      [HttpPost("Add_Food_Items_For_Specific_Catering")]
      public async Task<IActionResult> PostFoods([FromBody] Catering_FoodItems[] Foods)
      {
            var e = Foods.AsEnumerable<Catering_FoodItems>();
            await _context.Catering_FoodItems.AddRangeAsync(e);
            await _context.SaveChangesAsync();
            return Ok("Items Added");
      }


      // Delete Item Only For Specific Catering
      // TestCase : 
      // {
      //   "cateringId": "Flynn",
      //   "foodItemsId": "f093576e-95d4-4f3c-8216-a2b495c08019"
      // }
      [HttpDelete("Delete_Catering_FoodItem")]
      public async Task<IActionResult> DeleteCateringFoods([FromBody] Catering_FoodItems Foods)
      {
            var f = await _context.Catering_FoodItems
                        .AsNoTracking()
                        .FirstAsync(w => w.CateringId == Foods.CateringId
                              && w.FoodItemsId == Foods.FoodItemsId);
            if (f != null)
            {
                  _context.Catering_FoodItems.Remove(Foods);
                  _context.SaveChanges();
                  return Ok("Item Removed");
            }
            else
            {
                  return BadRequest("Invalid Request");
            }
      }

      #endregion

      // RD on Events Food Items
      #region EventsFoodItems_Controller

      /*
      * Post Various Food Items To Events 
      * From Selected Catering Recepies("Many_To_Many Relationship")
      *
      * TestCase (EventId): 
      * a9a04b80-2341-4acb-91c5-2a7cc2f8faa8,
      * e8e47fa2-687b-4071-8ba9-92838ea8996e
      */
      [HttpPost("Post_Event_Items")]
      public async Task<IActionResult> PostFoods([FromBody] EventFoodItems[] Foods)
      {
            foreach (var x in Foods)
            {
                  await _context.Event_FoodItems.AddAsync(x);
                  await _context.SaveChangesAsync();
            }
            return Ok("Items Added");
      }

      [HttpDelete("Delete_Event_Food_Item")]
      public async Task<IActionResult> DeleteEventFoods([FromBody] EventFoodItems Foods)
      {
            var f = await _context.Event_FoodItems
                        .AsNoTracking()
                        .FirstAsync(w => w.EventId == Foods.EventId
                              && w.FoodItemsId == Foods.FoodItemsId);
            if (f != null)
            {
                  _context.Event_FoodItems.Remove(Foods);
                  _context.SaveChanges();
                  return Ok("Item Removed");
            }
            else
            {
                  return BadRequest("Invalid Request");
            }
      }

      #endregion
}