namespace API.Controllers;

[Route("[controller]")]
public class ReviewsController : Controller
{
      private readonly ILogger<ReviewsController> _logger;
      private readonly Context _context;

      public ReviewsController(ILogger<ReviewsController> logger, Context context)
      {
            _logger = logger;
            _context = context;
      }

      [HttpPost("Add_Review")]
      public async Task<IActionResult> Post([FromBody] Review model)
      {
            if (model == null)
            {
                  return BadRequest();
            }
            else
            {
                  try
                  {
                        var x = await _context.Reviews.AddAsync(model);
                        _context.SaveChanges();
                        return Ok("Review Added");
                  }
                  catch
                  {
                        return BadRequest("Review Not Added , Invalid Data !!!");
                  }

            }
      }
}
