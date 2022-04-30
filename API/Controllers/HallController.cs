using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
      [Route("[controller]")]
      public class HallController : ControllerBase
      {
            private readonly Context _context;

            public HallController(Context context)
            {
                  _context = context;
            }


            [HttpGet("Get_Halls_Cards")]
            public async Task<IActionResult> GetAllHalls()
            {
                  try
                  {
                        var x = await _context.Halls.ToListAsync();
                        return Ok(x);
                  }
                  catch (Exception e)
                  {
                        return Ok(e.StackTrace);
                  }
            }
            [HttpGet("Get_Hall_Details/{IdShow}")]
            public async Task<IActionResult> GetHallDetails(string IdShow)
            {
                  var Id = new Guid(IdShow);
                  try
                  {
                        // Get Booked Event Timing
                        var BookedTime = await (_context.Events
                                    .Where(w => w.Hall_ID.Equals(Id))
                                    .Select(r => r.EventTime)
                                    .ToArrayAsync());

                        // Events Selected for Reviews Seed
                        // D353EFC6-4B41-4D4D-AF99-08DA2ACD21ED
                        // 2775B326-9D5A-4BF7-AF98-08DA2ACD21ED
                        // 916A8A31-4A9F-461B-AF9A-08DA2ACD21ED

                        // Hall ID
                        // 1B71E1CF-55FF-4B6D-D06B-08DA2AB4423E

                        // Hall Reviews
                        var Reviews = await (
                              from r in _context.Reviews
                              join e in _context.Events on r.EventID equals e.ID
                              where Id == e.Hall_ID
                              select r.HallReviewContent
                        ).ToArrayAsync();

                        // Result
                        var x = await (
                              from h in _context.Halls
                              where h.HallID == Id
                              select new
                              {
                                    Hall = h,
                                    BookedTime = BookedTime,
                                    Review = Reviews
                              }
                        ).ToListAsync();

                        return Ok(x);
                  }
                  catch (Exception e)
                  {
                        return Ok(e.StackTrace);
                  }

            }
            [HttpGet("Get_Halls/{Location}")]
            public async Task<IActionResult> GetHallsByLocation(string Location)
            {
                  try
                  {
                        var x = await _context.Halls.Where(i => i.Location.Equals(Location)).ToListAsync();
                        return Ok(x);
                  }
                  catch (Exception e)
                  {
                        return Ok(e.StackTrace);
                  }
            }

            [HttpPost("Add_Halls")]
            public async Task<IActionResult> Post([FromBody] Hall model)
            {
                  if (model == null)
                  {
                        return BadRequest();
                  }
                  else
                  {
                        var hall = new Hall
                        {
                              Hall_Name = model.Hall_Name,
                              Location = model.Location,
                              Description = model.Description
                        };
                        var x = await _context.Halls.AddAsync(hall);
                        await _context.SaveChangesAsync();
                        return Ok("Halls Added");
                  }
            }

      }
}