
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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

      }
}
