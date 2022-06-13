namespace API.Controllers;

[Route("[controller]")]
public class UsersController : ControllerBase
{
      private readonly Context _context;
      public UsersController(Context context)
      {
            _context = context;
      }

      [HttpGet("Admin/Get_Users")]
      public async Task<IActionResult> GetUsers()
      {
            try
            {
                  var x = await _context.Users.Where(w => w.Role.ToLower() == "member")
                              .Select(s => new
                              {
                                    Name = s.Name,
                                    ProfileImg = s.ProfileImg,
                                    username = s.Username,
                                    email = s.Email,
                                    phone = s.Phone,
                                    location = s.Location,
                                    role = s.Role
                              })
                              .ToListAsync();
                  return Ok(x);
            }
            catch (Exception e)
            {
                  return BadRequest(e.StackTrace);
            }
      }
      [HttpGet("Admin/Get_HallOwner")]
      public async Task<IActionResult> GetHallOwner()
      {
            try
            {
                  // Checks For Conditional Matches With Values
                  var x = await _context.Users.Where(w => w.Role.ToLower() == "hall_owner")
                        .Select(s => new
                        {
                              Name = s.Name,
                              ProfileImg = s.ProfileImg,
                              username = s.Username,
                              email = s.Email,
                              phone = s.Phone,
                              location = s.Location,
                              role = s.Role,
                              Halls = _context.Halls.Where(e => e.OwnerUsername == s.Username)
                                          .Select(r => new
                                          {
                                                HallName = r.HallName,
                                                HallId = r.HallID,
                                                Location = r.Location,
                                                Description = r.Description,
                                                Rating = _context.Events.Where(w => w.Hall_ID == r.HallID).Any()
                                                            ? Math.Round((from k in _context.Reviews
                                                                          join e in _context.Events on k.ReviewID equals e.EventID
                                                                          where r.HallID == e.Hall_ID
                                                                          select k.HallRating).Average(), 1) : 0.0
                                          }).ToArray()
                        })
                        .ToListAsync();
                  return Ok(x);
            }
            catch (Exception e)
            {
                  return BadRequest(e.StackTrace);
            }
      }
      [HttpGet("Admin/Get_Photographer")]
      public async Task<IActionResult> GetPhotoGraphers()
      {
            try
            {
                  var x = await _context.Users.Where(w => w.Role.ToLower() == "photographer")
                              .Select(s => new
                              {
                                    Name = s.Name,
                                    ProfileImg = s.ProfileImg,
                                    Experience = s.PhotoGrapherIds.Experience,
                                    username = s.Username,
                                    email = s.Email,
                                    phone = s.Phone,
                                    location = s.Location,
                                    role = s.Role,
                                    Rating = (_context.Events.Where(w => w.PhotoGrapherID == s.Username).Any()
                                                ? Math.Round((from k in _context.Reviews
                                                              join e in _context.Events on k.ReviewID equals e.EventID
                                                              where s.Username == e.PhotoGrapherID
                                                              select k.HallRating).ToArray().Average(), 1) : 0.0)
                              }).ToListAsync();
                  return Ok(x);
            }
            catch (Exception e)
            {
                  return BadRequest(e.StackTrace);
            }
      }
      [HttpGet("Admin/Get_Catering")]
      public async Task<IActionResult> GetCatering()
      {
            try
            {
                  var x = await _context.Users.Where(w => w.Role.ToLower() == "catering")
                              .Select(s => new
                              {
                                    Name = s.Name,
                                    ProfileImg = s.ProfileImg,
                                    Experience = s.CateringIds.Experience,
                                    username = s.Username,
                                    email = s.Email,
                                    phone = s.Phone,
                                    location = s.Location,
                                    role = s.Role,
                                    Rating = (_context.Events.Where(w => w.CateringId == s.Username).Any()
                                    ? Math.Round((from k in _context.Reviews
                                                  join e in _context.Events on k.ReviewID equals e.EventID
                                                  where s.Username == e.CateringId
                                                  select k.CateringRating).ToArray().Average(), 1) : 0.0)
                              }).ToListAsync();
                  return Ok(x);
            }
            catch (Exception e)
            {
                  return BadRequest(e.StackTrace);
            }
      }

      [HttpDelete("Admin/DeleteUser/{Id}")]
      public async Task<IActionResult> DeleteUser(string Id)
      {
            var id = new Guid("id");

            var i = await _context.Users.FindAsync(id);
            if (i != null)
            {
                  i.Status = 0;
                  _context.Users.Update(i);
                  return Ok("Account Deleted !!");
            }
            else
            {
                  return BadRequest("Account Not Found !!! ");
            }
      }
}
