namespace API.Controllers;
[Route("[controller]")]
public class HallController : ControllerBase
{
      private readonly Context _context;

      public HallController(Context context)
      {
            _context = context;
      }

      #region Client Controls

      // Get Halls For Display Cards Path : /halls
      [HttpGet("Get_All_Halls_Cards")]
      // [HttpGet("Get_Admin_Hall_Cards")]
      public async Task<IActionResult> GetAllHalls()
      {
            try
            {

                  var x = await _context.Halls
                  .Select(s => new
                  {
                        HallID = s.HallID,
                        HallName = s.Hall_Name,
                        Description = s.Description,
                        HallLocation = s.Location,
                        DisplayImg = s.DisplayImg,
                        Rating = _context.Events.Where(w => w.Hall_ID == s.HallID).Any()
                                    ? Math.Round((from r in _context.Reviews
                                                  join e in _context.Events on r.ReviewID equals e.EventID
                                                  where s.HallID == e.Hall_ID
                                                  select r.HallRating).Average(), 1) : 0.0
                  })
                  .ToListAsync();
                  return Ok(x);
            }
            catch (Exception e)
            {
                  return Ok(e.StackTrace);
            }
      }

      // Get Details for Specific Hall Path : /halls/hallview
      [HttpGet("Get_Hall_Details/{IdShow}")]
      // [HttpGet("Get_Admin_Hall_Details/{IdShow}")]
      public async Task<IActionResult> GetHallDetails(string IdShow)
      {
            // Hall ID
            // 1B71E1CF-55FF-4B6D-D06B-08DA2AB4423E - (Case 1 - Return All Value)
            // 3FA85F64-5717-4562-B3FC-2C963F66AFA6 - (Case 2 - Return With Null Value)
            var Id = new Guid(IdShow);
            try
            {

                  // Get Booked Event Timing
                  var BookedTime = await (_context.Events
                              .Where(w => w.Hall_ID.Equals(Id))
                              .Where(o => o.EventTime > DateTime.Now)
                              .Select(r => r.EventTime)
                              .ToArrayAsync());

                  // Hall Reviews
                  var Reviews = (Guid Id) =>
                  {
                        var x = (
                        from r in _context.Reviews
                        join e in _context.Events on r.ReviewID equals e.EventID
                        join u in _context.Users on e.User_ID equals u.Username
                        where Id == e.Hall_ID
                        select new
                        {
                              Username = u.Username,
                              Review = r.HallReviewContent,
                              Rating = r.HallRating
                        }).ToArray();
                        return x;
                  };

                  // Checks For Conditional Matches With Values
                  var Exe = _context.Events.Where(w => w.Hall_ID == Id).Any();

                  // Result
                  var x = await (
                        from h in _context.Halls
                        where h.HallID == Id
                        select new
                        {
                              Hall = h,
                              BookedTime = BookedTime,
                              Review = Exe ? Reviews(Id) : null,
                              OverAllRating = Exe ? Math.Round(Reviews(Id).Select(s => s.Rating).Average(), 1) : 0.0
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
            try
            {
                  var x = await _context.Halls
                  .Where(i => i.Location.Equals(Location.ToLower()))
                  .Select(d => new
                  {
                        HallID = d.HallID,
                        HallName = d.Hall_Name,
                        Description = d.Description,
                        HallLocation = d.Location,
                        DisplayImg = d.DisplayImg,
                        OverAllRating = _context.Events.Where(w => w.Hall_ID == d.HallID).Any()
                                    ? Math.Round((from r in _context.Reviews
                                                  join e in _context.Events on r.ReviewID equals e.EventID
                                                  where d.HallID == e.Hall_ID
                                                  select r.HallRating).Average(), 1) : 0.0
                  })
                  .ToListAsync();
                  return Ok(x);

            }
            catch (Exception e)
            {
                  return BadRequest(e.StackTrace);
            }
      }

      #endregion

      #region Business Controls
      [HttpPost("Add_Halls")]
      public async Task<IActionResult> PostAHall([FromBody] HallDTO model)
      {
            var x = await _context.Halls.AddAsync(
                  new Hall
                  {
                        HallID = model.HallID,
                        OwnerUsername = model.OwnerUsername,
                        Hall_Name = model.Hall_Name,
                        Location = model.Location,
                        Description = model.Description
                  }
            );
            await _context.SaveChangesAsync();
            return Ok("Halls Added");
      }

      [HttpPut("EditHall")]
      public async Task<IActionResult> EditAHall([FromBody] HallDTO model)
      {
            if (model == null)
            {
                  return BadRequest("Invalid Data");
            }
            else
            {
                  var hall = await _context.Halls.AsNoTracking()
                              .FirstOrDefaultAsync(e => e.HallID == model.HallID);
                  if (hall == null)
                  {
                        return NotFound(new
                        {
                              StatusCode = 404,
                              Message = "Hall not found",
                        });
                  }
                  else
                  {
                        if (ModelState.IsValid)
                        {
                              var h = new Hall
                              {
                                    HallID = model.HallID,
                                    OwnerUsername = model.OwnerUsername,
                                    DisplayImg = null,
                                    Hall_Name = model.Hall_Name,
                                    Location = model.Location,
                                    Description = model.Description

                              };
                              _context.Entry(h).State = EntityState.Modified;
                              _context.SaveChanges();
                              return new OkObjectResult(new
                              {
                                    StatusCode = 200,
                                    Message = "Hall Updated Successfully"
                              });
                        }
                        return BadRequest();
                  }
            }

      }

      [HttpDelete("Admin/Delete/Hall/{HallID}")]
      // [HttpDelete("DeleteHall/{HallID}")]
      public async Task<IActionResult> Delete(Guid HallID)
      {
            var hall = await _context.Halls.FindAsync(HallID);
            if (hall == null)
            {
                  return NotFound(new
                  {
                        StatusCode = 404,
                        Message = "Hall Not Found",
                  });
            }
            else
            {
                  _context.Halls.Remove(hall);
                  _context.SaveChanges();
                  return Ok(new
                  {
                        StatusCode = 200,
                        Message = "Hall Deleted"
                  });
            }
      }

      #endregion


}
