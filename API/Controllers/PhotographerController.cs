
namespace API.Controllers;

[Route("[controller]")]
public class PhotographerController : ControllerBase
{
      private readonly Context _context;
      public PhotographerController(Context context)
      {
            _context = context;
      }
      #region Client Controls
      // Get Halls For Display Cards Path : /halls
      [HttpGet("Get_All_Photographer_Cards")]
      [HttpGet("Get_Admin_Photographer_Cards")]
      public async Task<IActionResult> GetAllPhotoGraphers()
      {
            try
            {
                  var x = await (from u in _context.Users
                                 join p in _context.Photographers on
                                 u.Username equals p.PhotographerUserId
                                 select new { u, p })
                  .Select(s => new
                  {
                        UserId = s.u.Username,
                        Name = s.u.Name,
                        ProfileImg = s.u.ProfileImg,
                        DisplayImg = s.p.DisplayImg,
                        PhotoTeamName = s.p.PhotoTeamName,
                        Phone = s.u.Phone,
                        Email = s.u.Email,
                        Location = s.u.Location,
                        Experience = s.p.Experience,
                        Rating = _context.Events.Where(w => w.PhotoGrapherID == s.p.PhotographerUserId).Any()
                                    ? Math.Round((from r in _context.Reviews
                                                  join e in _context.Events on r.ReviewID equals e.EventID
                                                  where s.p.PhotographerUserId == e.PhotoGrapherID
                                                  select r.PhotoRating).Average(), 1) : 0.0
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
      [HttpGet("Get_PhotoGrapher_Details/{Id}")]
      [HttpGet("Get_Admin_PhotoGrapher_Details/{Id}")]
      public async Task<IActionResult> GetPhotoGraphersById(string Id)
      {
            try
            {
                  // Get All Reviews By PhotographerId
                  var AReviews = await (_context.Events.Where(w => w.PhotoGrapherID == Id).Any()
                              ? from r in _context.Reviews
                                join e in _context.Events on r.ReviewID equals e.EventID
                                join u in _context.Users on e.User_ID equals u.Username
                                where e.PhotoGrapherID == Id
                                select new
                                {
                                      Username = u.Username,
                                      Review = r.PhotographerReviewContent,
                                      Rating = r.PhotoRating
                                } : null).ToArrayAsync();

                  // Show All Related Data About PhotoGrapher
                  var x = await (from u in _context.Users
                                 join p in _context.Photographers
                                 on u.Username equals p.PhotographerUserId
                                 where u.Username == Id
                                 select new { u, p })
                  .Select(s => new
                  {
                        UserId = s.u.Username,
                        Name = s.u.Name,
                        PhotoTeamName = s.p.PhotoTeamName,
                        ProfileImg = s.u.ProfileImg,
                        DisplayImg = s.p.DisplayImg,
                        Phone = s.u.Phone,
                        Email = s.u.Email,
                        Location = s.u.Location,
                        Experience = s.p.Experience,
                        OverAllRating = _context.Events.Where(w => w.PhotoGrapherID == s.p.PhotographerUserId).Any()
                                    ? Math.Round((AReviews).Select(o => o.Rating).Average(), 1) : 0.0,
                        Reviews = AReviews
                  })
                  .FirstAsync();
                  return Ok(x);
            }
            catch (Exception e)
            {
                  return Ok(e.StackTrace);
            }
      }

      // Get Halls Data By Location
      [HttpGet("Get_Halls/{Location}")]
      public async Task<IActionResult> GetPhotoGraphersByLocation(string Location)
      {
            try
            {
                  var x = await (from u in _context.Users
                                 join p in _context.Photographers
                                 on u.Username equals p.PhotographerUserId
                                 where u.Location == Location
                                 select new { u, p })
                  .Select(s => new
                  {
                        UserId = s.u.Username,
                        Name = s.u.Name,
                        PhotoTeamName = s.p.PhotoTeamName,
                        ProfileImg = s.u.ProfileImg,
                        DisplayImg = s.p.DisplayImg,
                        Phone = s.u.Phone,
                        Email = s.u.Email,
                        Location = s.u.Location,
                        Experience = s.p.Experience,
                        OverAllRating = _context.Events.Where(w => w.PhotoGrapherID == s.p.PhotographerUserId).Any()
                                    ? Math.Round((from r in _context.Reviews
                                                  join e in _context.Events on r.ReviewID equals e.EventID
                                                  where e.PhotoGrapherID == s.p.PhotographerUserId
                                                  select r.PhotoRating).Average(), 1) : 0.0,
                  }).ToListAsync();
                  return Ok(x);
            }
            catch (Exception e)
            {
                  return BadRequest(e.StackTrace);
            }
      }

      #endregion

      #region Business & Admin Controls
      [HttpPost("Add_PhotoGrapher")]
      public async Task<IActionResult> PostAPhotoGrapher([FromBody] photographerDTO model)
      {
            var x = await _context.Photographers.AddAsync(new Photographer
            {
                  PhotographerUserId = model.PhotographerUserId,
                  PhotoTeamName = model.PhotoTeamName,
                  Experience = model.Experience,
                  DisplayImg = model.DisplayImg
            });
            await _context.SaveChangesAsync();
            return Ok("PhotoGrapher Added");
      }

      [HttpPut("EditPhotoGrapher")]
      public async Task<IActionResult> EditAPhotoGrapher([FromBody] PhotographerDTO2 m)
      {
            if (m != null)
            {
                  var user = await _context.Users.AsNoTracking()
                              .FirstOrDefaultAsync(e => e.Username == m.UserId);
                  if (user != null)
                  {
                        var photographer = await _context.Photographers.AsNoTracking()
                              .FirstOrDefaultAsync(e => e.PhotographerUserId == m.UserId);
                        if (photographer != null)
                        {
                              if (ModelState.IsValid)
                              {
                                    var u = new User
                                    {
                                          Username = user.Username,
                                          Name = m.Name,
                                          ProfileImg = m.ProfileImg,
                                          Email = m.Email,
                                          Phone = m.Phone,
                                          Location = m.Location

                                    };
                                    var p = new Photographer
                                    {
                                          PhotographerUserId = user.Username,
                                          PhotoTeamName = m.PhotoTeamName,
                                          Experience = m.Experience,
                                          DisplayImg = m.DisplayImg

                                    };


                                    _context.Entry(u).State = EntityState.Modified;
                                    _context.Entry(p).State = EntityState.Modified;

                                    _context.SaveChanges();
                                    return new OkObjectResult(new
                                    {
                                          StatusCode = 200,
                                          Message = "PhotoGrapher Data Successfully Updated"
                                    });
                              }
                              return BadRequest();
                        }
                        return BadRequest("Invalid Data 2");
                  }
                  return BadRequest("Invalid Data 1");
            }
            else
            {
                  return BadRequest(new
                  {
                        StatusCode = 404,
                        Message = "Invalid Data found",
                  });
            }

      }

      [HttpDelete("DeletePhotoGrapher/{UserId}")]
      public async Task<IActionResult> Delete(string UserId)
      {
            var user = await _context.Users.FindAsync(UserId);
            if (user != null)
            {
                  var photographer = await _context.Photographers.FindAsync(UserId);
                  if (photographer != null)
                  {
                        _context.Users.Remove(user);
                        _context.Photographers.Remove(photographer);
                        _context.SaveChanges();
                        return Ok(new
                        {
                              StatusCode = 200,
                              Message = "User Deleted"
                        });
                  }
                  else
                  {
                        return NotFound(new
                        {
                              StatusCode = 404,
                              Message = "Hall Not Found",
                        });
                  }
            }
            else
            {
                  return NotFound(new
                  {
                        StatusCode = 404,
                        Message = "Hall Not Found",
                  });
            }
      }

      #endregion
}