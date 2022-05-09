namespace API.Controllers
{
      [Route("[controller]")]
      public class CateringController : ControllerBase
      {
            private readonly Context _context;
            public CateringController(Context context)
            {
                  _context = context;
            }
            #region Client Controls


            [HttpGet("Get_FoodItems_By_Catering/{Id}")]
            public async Task<IActionResult> GetFoodsByCatering(string Id)
            {

                  var x = await _context.Caterings.Where(w => w.CatererUserId == Id)
                        .Select(s => s.CateringItems.Select(o => new
                        {
                              ItemId = o.FoodItem.ItemId,
                              Item = o.FoodItem.Item,
                              Img = o.FoodItem.ItemImg
                        }))
                        .ToListAsync();

                  return Ok(x);
            }

            // Get Halls For Display Cards Path : /halls
            [HttpGet("Get_All_Catering_Cards")]
            [HttpGet("Get_Admin_Catering_Cards")]
            public async Task<IActionResult> GetAllCaterings()
            {
                  try
                  {
                        var x = await (from u in _context.Users join p in _context.Caterings on u.Username equals p.CatererUserId select new { u, p })
                        .Select(s => new
                        {
                              UserId = s.u.Username,
                              Name = s.u.Name,
                              CatererTeamName = s.p.CatererTeamName,
                              ProfileImg = s.u.ProfileImg,
                              DisplayImg = s.p.DisplayImg,
                              Phone = s.u.Phone,
                              Email = s.u.Email,
                              Location = s.u.Location,
                              Experience = s.p.Experience,
                              Rating = _context.Events.Where(w => w.CateringId == s.p.CatererUserId).Any()
                                          ? Math.Round((from r in _context.Reviews
                                                        join e in _context.Events on r.ReviewID equals e.EventID
                                                        where s.p.CatererUserId == e.CateringId
                                                        select r.CateringRating).Average(), 1) : 0.0
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
            [HttpGet("Get_Catering_Details/{Id}")]
            [HttpGet("Get_Admin_Catering_Details/{Id}")]
            public async Task<IActionResult> GetCateringsById(string Id)
            {
                  try
                  {
                        // Get All Reviews By CateringId
                        var AReviews = await (_context.Events.Where(w => w.CateringId == Id).Any()
                                    ? from r in _context.Reviews
                                      join e in _context.Events on r.ReviewID equals e.EventID
                                      join u in _context.Users on e.User_ID equals u.Username
                                      where e.CateringId == Id
                                      select new
                                      {
                                            Username = u.Username,
                                            Review = r.CateringReviewContent,
                                            Rating = r.CateringRating
                                      } : null).ToArrayAsync();

                        // Show All Related Data About Catering
                        var x = await (from u in _context.Users
                                       join p in _context.Caterings
                                       on u.Username equals p.CatererUserId
                                       where u.Username == Id
                                       select new { u, p })
                        .Select(s => new
                        {
                              UserId = s.u.Username,
                              Name = s.u.Name,
                              CatererTeamName = s.p.CatererTeamName,
                              ProfileImg = s.u.ProfileImg,
                              DisplayImg = s.p.DisplayImg,
                              Phone = s.u.Phone,
                              Email = s.u.Email,
                              Location = s.u.Location,
                              Experience = s.p.Experience,
                              OverAllRating = _context.Events.Where(w => w.CateringId == s.p.CatererUserId).Any()
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
            [HttpGet("Get_Catering/{Location}")]
            public async Task<IActionResult> GetCateringsByLocation(string Location)
            {
                  try
                  {
                        var x = await (from u in _context.Users
                                       join p in _context.Caterings
                                       on u.Username equals p.CatererUserId
                                       where u.Location == Location
                                       select new { u, p })
                        .Select(s => new
                        {
                              UserId = s.u.Username,
                              Name = s.u.Name,
                              CatererTeamName = s.p.CatererTeamName,
                              ProfileImg = s.u.ProfileImg,
                              DisplayImg = s.p.DisplayImg,
                              Phone = s.u.Phone,
                              Email = s.u.Email,
                              Location = s.u.Location,
                              Experience = s.p.Experience,
                              OverAllRating = _context.Events.Where(w => w.CateringId == s.p.CatererUserId).Any()
                                          ? Math.Round((from r in _context.Reviews
                                                        join e in _context.Events on r.ReviewID equals e.EventID
                                                        where e.CateringId == s.p.CatererUserId
                                                        select r.CateringRating).Average(), 1) : 0.0,
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
            [HttpPost("Add_Catering")]
            public async Task<IActionResult> PostACatering([FromBody] CateringDTO model)
            {
                  var x = await _context.Caterings.AddAsync(new Catering
                  {
                        CatererUserId = model.CatererUserId,
                        CatererTeamName = model.CatererTeamName,
                        Experience = model.Experience,
                        DisplayImg = model.DisplayImg
                  });
                  await _context.SaveChangesAsync();
                  return Ok("Catering Added");
            }

            [HttpPut("EditCatering")]
            public async Task<IActionResult> EditACatering([FromBody] CateringDTO2 m)
            {
                  if (m != null)
                  {
                        var user = await _context.Users.AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.Username == m.UserId);
                        if (user != null)
                        {
                              var Catering = await _context.Caterings.AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.CatererUserId == m.UserId);
                              if (Catering != null)
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
                                          var p = new Catering
                                          {
                                                CatererTeamName = m.CatererTeamName,
                                                CatererUserId = user.Username,
                                                Experience = m.Experience,
                                                DisplayImg = m.DisplayImg
                                          };


                                          _context.Entry(u).State = EntityState.Modified;
                                          _context.Entry(p).State = EntityState.Modified;

                                          _context.SaveChanges();
                                          return new OkObjectResult(new
                                          {
                                                StatusCode = 200,
                                                Message = "Catering Data Successfully Updated"
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

            /*
            * Error Occured By ForeignKey in Events
            * "FK_Events_Caterings_CateringId"
            */
            [HttpDelete("DeleteCatering/{UserId}")]
            public async Task<IActionResult> DeleteCatering(string UserId)
            {
                  var user = await _context.Users.FindAsync(UserId);
                  if (user != null)
                  {
                        var Catering = await _context.Caterings.FindAsync(UserId);
                        if (Catering != null)
                        {
                              _context.Caterings.Remove(Catering);
                              _context.Users.Remove(user);
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
                                    Message = "Caterer Not Found",
                              });
                        }
                  }
                  else
                  {
                        return NotFound(new
                        {
                              StatusCode = 404,
                              Message = "Caterer Not Found",
                        });
                  }
            }

            #endregion

      }
}

