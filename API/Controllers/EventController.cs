

namespace API.Controllers;

[Route("[controller]")]
public class EventController : Controller
{
      private readonly ILogger<EventController> _logger;
      private Context _context;

      public EventController(ILogger<EventController> logger, Context context)
      {
            _logger = logger;
            _context = context;
      }

      [HttpPost("Add_Events")]
      public async Task<IActionResult> PostEvents([FromBody] Event m)
      {
            if (m != null)
            {
                  var eve = new Event
                  {
                        EventName = m.EventName,
                        EventTime = m.EventTime,
                        User_ID = m.User_ID,
                        Hall_ID = m.Hall_ID,
                        PhotoGrapherID = m.PhotoGrapherID,
                        CateringId = m.CateringId,
                  };
                  try
                  {
                        var x = await _context.Events.AddAsync(eve);
                        await _context.SaveChangesAsync();

                        return Ok(new
                        {
                              EventId = eve.EventID,
                              Msg = "Event Added"
                        });
                  }
                  catch { return BadRequest("Event Not Added , Something Went Wrong !!!"); }
            }
            else { return BadRequest("Invalid Data Passed"); }
      }
      [HttpGet("Get_Events/{userId}")]

      // TestCase (EventId): 
      // a9a04b80-2341-4acb-91c5-2a7cc2f8faa8,
      // e8e47fa2-687b-4071-8ba9-92838ea8996e
      public async Task<IActionResult> GetEventsByUser(string userId)
      {
            try
            {
                  var x = await (from u in _context.Users
                                 join e in _context.Events on u.Username equals e.User_ID
                                 where e.User_ID == userId
                                 select new { u, e }
                              ).Select(s => new
                              {
                                    EventId = s.e.EventID,
                                    User = s.u.Username,
                                    EventName = s.e.EventName,
                                    EventTime = s.e.EventTime,
                                    HallName = s.e.Halls.Hall_Name,
                                    Location = s.e.Halls.Location,
                                    Description = s.e.Halls.Description,
                                    PhotoGrapher = s.e.PhotoGrapherID,
                                    Catering = s.e.CateringId,
                                    Food_Items = s.e.EventFoodItems.Select(l => new
                                    {
                                          ItemId = l.FoodItem.ItemId,
                                          ItemName = l.FoodItem.Item,
                                          ItemImg = l.FoodItem.ItemImg
                                    }).ToArray()
                              }).ToListAsync();
                  return Ok(x);
            }
            catch
            {
                  return NotFound();
            }
      }

}
