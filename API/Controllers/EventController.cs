using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
            public async Task<IActionResult> PostEvents([FromBody] Event model)
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
                        try
                        {
                              var x = await _context.Events.AddAsync(eve);
                              await _context.SaveChangesAsync();
                              return Ok("Event Added");
                        }
                        catch
                        {
                              return BadRequest("Event Not Added , Something Went Wrong !!!");
                        }

                  }
            }
            [HttpGet("Get_Events")]
            public async Task<IActionResult> GetAllEvents()
            {
                  try
                  {
                        var x = await _context.Events.ToListAsync();
                        return Ok(x);
                  }
                  catch
                  {
                        return NotFound("NotFound");
                  }

            }
            [HttpGet("Get_Events/{userId}")]
            public async Task<IActionResult> GetEventsByUser(string userId)
            {
                  try
                  {
                        var x = await _context.Users.Where(a => a.Username.Equals(userId))
                              .Join(_context.Events,
                                     user => user.Username,
                                     events => events.Users.Username,
                                     (user, events) => new
                                     {
                                           User = user.Username,
                                           EventName = events.EventName,
                                           EventTime = events.EventTime,
                                           HallName = events.Halls.Hall_Name,
                                           Location = events.Halls.Location,
                                           Description = events.Halls.Description
                                     })
                              .ToListAsync();
                        return Ok(x);
                  }
                  catch
                  {
                        return NotFound();
                  }
            }

      }
}