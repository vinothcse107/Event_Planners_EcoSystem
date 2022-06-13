
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers;

[ApiController]
[Route("api/user")]
public class AccountController : ControllerBase
{
      public readonly Context _context;
      private readonly ITokenService _tokenService;

      public AccountController(Context context, ITokenService TokenService)
      {
            _context = context;
            _tokenService = TokenService;
      }

      [HttpPost("signup")]
      public async Task<ActionResult<UserDTO>> Signup([FromBody] SignupDTO signDTO)
      {
            // ? Check For the UserExists in DB
            if (await _context.Users.AnyAsync(x => x.Username == signDTO.Username.ToLower())) return BadRequest("User Already Exists");

            // * If User Not Exists in DB It generate Password Hash & Add user to DB
            using var hmac = new HMACSHA512();
            var user = new User
            {
                  Name = signDTO.Name,
                  Username = signDTO.Username.ToLower(),
                  Email = signDTO.Email.ToLower(),
                  Phone = signDTO.Phone,
                  Location = signDTO.Location,
                  Role = signDTO.Role.ToLower(),
                  PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signDTO.Password)),
                  PasswordSalt = hmac.Key
            };
            if (!ModelState.IsValid)
            {
                  return BadRequest("Invalid Details");
            }
            else
            {
                  await _context.Users.AddAsync(user);
                  await _context.SaveChangesAsync();

                  return new UserDTO
                  {
                        Username = user.Username,
                        Role = user.Role,
                        Token = _tokenService.CreateToken(user)
                  };
            }
      }

      [HttpPost("login")]
      public async Task<ActionResult<UserDTO>> Login([FromBody] LoginDTO Data)
      {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == Data.Username);
            if (user == null) return Unauthorized("Invalid UserName !!!");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Data.Password));
            for (int i = 0; i < Hash.Length; i++)
            {
                  if (Hash[i] != user.PasswordHash[i]) return Unauthorized("Invalid User Password");
            }
            return new UserDTO
            {
                  Username = user.Username,
                  Role = user.Role,
                  Token = _tokenService.CreateToken(user)
            };
      }

      #region HomeDisplayData
      [HttpGet("Get_Home_Data/{Local}")]
      public async Task<IActionResult> GetHomeData(string Local)
      {
            try
            {

                  var h = await _context.Halls
                  .Where(i => i.Location.Equals(Local.ToLower()))
                  .Select(d => new
                  {
                        HallID = d.HallID,
                        HallName = d.HallName,
                        Description = d.Description,
                        HallLocation = d.Location,
                        DisplayImg = d.DisplayImg,
                        OverAllRating = _context.Events.Where(w => w.Hall_ID == d.HallID).Any()
                                    ? Math.Round((from r in _context.Reviews
                                                  join e in _context.Events on r.ReviewID equals e.EventID
                                                  where d.HallID == e.Hall_ID
                                                  select r.HallRating).Average(), 1) : 0.0
                  })
                  .OrderByDescending(o => o.OverAllRating)
                  .Take(3)
                  .ToListAsync();

                  var ph = await (from u in _context.Users
                                  join p in _context.Photographers
                                  on u.Username equals p.PhotographerUserId
                                  where u.Location == Local
                                  select new { u, p })
                  .Select(s => new
                  {
                        UserId = s.u.Username,
                        Name = s.u.Name,
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
                  })
                  .OrderByDescending(o => o.OverAllRating)
                  .Take(3)
                  .ToListAsync();

                  var c = await (from u in _context.Users
                                 join p in _context.Caterings
                                 on u.Username equals p.CatererUserId
                                 where u.Location == Local
                                 select new { u, p })
                  .Select(s => new
                  {
                        UserId = s.u.Username,
                        Name = s.u.Name,
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
                  })
                  .OrderByDescending(o => o.OverAllRating)
                  .Take(3)
                  .ToListAsync();


                  var x = new { Halls = h, PhotoGraphers = ph, Catering = c };
                  return Ok(x);

            }
            catch (Exception e)
            {
                  return BadRequest($"Somthing Went Wrong:  \n {e.StackTrace}");
            }


      }
      #endregion

}

