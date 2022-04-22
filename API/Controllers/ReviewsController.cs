using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
      [Route("[controller]")]
      // [SerializableAttribute]

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

            [HttpGet("User_Reviews/{userId}")]

            public async Task<IActionResult> Get(string userId)
            {
                  var q = await (
                        from r in _context.Reviews
                        join e in _context.Events on r.EventID equals e.ID
                        join h in _context.Halls on e.Hall_ID equals h.HallID
                        where e.User_ID == userId
                        select new
                        {
                              user = e.User_ID,
                              Event = e.EventName,
                              Hall = h.Hall_Name,
                              Review = r.ReviewContent

                        }).ToListAsync();

                  return Ok(q);
            }


            [HttpGet("Hall_Reviews/{HallId}")]
            public async Task<IActionResult> Get(int HallId)
            {

                  var x = await _context.Reviews.Where(a => a.HallID == HallId).ToListAsync();
                  _context.SaveChanges();
                  return Ok(x);
            }


      }
}