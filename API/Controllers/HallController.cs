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
                        return Ok("Halls Added");
                  }
            }

            [HttpGet("Get_Halls")]
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

      }
}