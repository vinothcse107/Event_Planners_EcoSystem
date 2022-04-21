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
                        return Ok("Event Added");
                  }
            }
            [HttpGet("Get_Events")]
            public async Task<IActionResult> Get()
            {
                  try
                  {
                        var x = await _context.Events.ToListAsync();
                        return Ok(x);
                  }
                  catch (Exception e)
                  {
                        return Ok(e.StackTrace);
                  }

            }
            [HttpGet("Get_Events/{userId}")]
            public async Task<IActionResult> Get(string userId)
            {
                  try
                  {
                        var x = await _context.Users
                              .Join(_context.Events,
                                     user => user.Username,
                                     events => events.Users.Username,
                                     (user, events) => new
                                     {
                                           User = user.Username,
                                           EventName = events.EventName,
                                           EventTime = events.EventTime,
                                     })
                                .ToListAsync();
                        return Ok(x);
                  }
                  catch (Exception e)
                  {
                        return Ok(e.StackTrace);
                  }

            }

      }
}