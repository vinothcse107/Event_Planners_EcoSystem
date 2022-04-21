using System;
using System.Collections.Generic;
using System.Linq;
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
            [HttpPost("Add_Halls")]
            public async Task<IActionResult> Post([FromBody] HallDTO model)
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
                        return Ok(x);
                  }
            }
            [HttpPost("Add_Events")]
            public async Task<IActionResult> Post([FromBody] EventDTO model)
            {
                  if (model == null)
                  {
                        return BadRequest();
                  }
                  else
                  {
                        var eve = new Event
                        {
                              EventName = model.EventName,
                              EventTime = model.EventTime,
                              User_ID = model.User_ID,
                              Hall_ID = model.Hall_ID
                        };
                        var x = await _context.Events.AddAsync(eve);
                        await _context.SaveChangesAsync();
                        return Ok(x);
                  }
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
                        var x = await _context.Reviews.AddAsync(model);
                        await _context.SaveChangesAsync();
                        return Ok(x);
                  }
            }

            [HttpGet("Get_Reviews")]
            public async Task<IActionResult> Get()
            {
                  try
                  {
                        var x = await _context.Halls.ToListAsync();
                        return Ok(new { StatusCode = 200, Message = "Hall Added" });
                  }
                  catch (Exception e)
                  {
                        return Ok(e.StackTrace);
                  }

            }

      }
}