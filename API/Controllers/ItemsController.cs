namespace API.Controllers;
[Route("[controller]")]
public class ItemsController : Controller
{
      private readonly Context _context;
      public ItemsController(Context context)
      {
            _context = context;
      }

      #region CateringFoodItems

      [HttpGet("Get_FoodItems")]
      public async Task<IActionResult> GetFoodItems()
      {
            // return Ok(Guid.NewGuid());
            var y = await (_context.Catering_FoodItems
                        .Where(w => w.CateringId == "Flynn")
                        .Select(x => x.FoodItem)
                        .Select(s => new
                        {
                              ItemId = s.ItemId,
                              ItemName = s.Item,
                              Img = s.ItemImg
                        }).ToListAsync());
            return Ok(y);
      }

      [HttpPost("Post_Items")]
      public async Task<IActionResult> PostFoods([FromBody] Catering_FoodItems[] Foods)
      {
            foreach (var x in Foods)
            {
                  await _context.Catering_FoodItems.AddAsync(x);
                  await _context.SaveChangesAsync();
            }
            return Ok("Items Added");
      }


      // Delete Item Only For Specific Catering
      // TestCase : 
      // {
      //   "cateringId": "Flynn",
      //   "foodItemsId": "f093576e-95d4-4f3c-8216-a2b495c08019"
      // }
      [HttpDelete("DeleteCateringFoodItem")]
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

      #region EventsFoodItems_Controller
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