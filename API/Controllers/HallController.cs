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


            // Get Halls For Display Cards Path : /halls
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

            // Get Details for Specific Hall Path : /halls/hallview
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

                        // Hall ID
                        // 1B71E1CF-55FF-4B6D-D06B-08DA2AB4423E

                        // Hall Reviews
                        var Reviews = await (
                              from r in _context.Reviews
                              join e in _context.Events on r.ReviewID equals e.EventID
                              join u in _context.Users on e.User_ID equals u.Username
                              where Id == e.Hall_ID
                              select new
                              {
                                    Username = u.Username,
                                    Review = r.HallReviewContent,
                                    Rating = r.HallRating
                              }
                        ).ToArrayAsync();

                        // Result
                        var x = await (
                              from h in _context.Halls
                              where h.HallID == Id
                              select new
                              {
                                    Hall = h,
                                    BookedTime = BookedTime,
                                    Review = Reviews,
                                    OverAllRating = Reviews.Select(s => s.Rating).Average()
                              }
                        ).ToListAsync();

                        return Ok(x);
                  }
                  catch (Exception e)
                  {
                        return Ok(e.StackTrace);
                  }

            }

            // Get Halls Data By Location
            [HttpGet("Get_Halls/{Location}")]
            public async Task<IActionResult> GetHallsByLocation(string Location)
            {
                  var x = await _context.Halls
                        .Where(i => i.Location.Equals(Location))
                        .Select(s => new
                        {
                              HallId = s.HallID,
                              HallName = s.Hall_Name,
                              Location = s.Location,
                              Description = s.Description,
                              Rating = (from r in _context.Reviews
                                        join e in _context.Events on r.ReviewID equals e.EventID
                                        where s.HallID == e.Hall_ID
                                        select r.HallRating).Average()
                        }).ToListAsync();


                  // var d = (from r in _context.Reviews
                  //          join e in _context.Events on r.EventID equals e.ID
                  //          join u in _context.Users on e.User_ID equals u.Username
                  //          join h in _context.Halls on e.Hall_ID equals h.HallID
                  //          //    where h.Location == Location
                  //          select new { r, e, u, h })
                  //    .GroupBy(g => g.h.HallID)
                  //    .Select(s => new
                  //    {
                  //          H = s.Key,
                  //          HallName = s.Select(i => i.h.Hall_Name),
                  //          Location = s.Select(i => i.h.Location),
                  //          Description = s.Select(i => i.h.Description),
                  //          Rating = s.Select(i => i.r.HallRating).Average()
                  //    }).ToArray();

                  // // var p = x.Zip(d);

                  return Ok(x);

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