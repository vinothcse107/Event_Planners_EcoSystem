using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
namespace API.Services
{
      public class TokenService : ITokenService
      {
            private readonly SymmetricSecurityKey _key;
            public TokenService(IConfiguration config)
            {
                  //* Key Generation
                  _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            }
            public string CreateToken(User user)
            {
                  // new Claim(ClaimTypes.Role, user.Role)

                  //* Adding Claims
                  var claims = new List<Claim>{
                        new Claim(JwtRegisteredClaimNames.NameId , user.Username)
                  };

                  //* Creating Credentials
                  var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

                  //* Looks Of JWT Token
                  var TokenDesc = new SecurityTokenDescriptor
                  {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddMinutes(5),
                        SigningCredentials = creds
                  };

                  //* Token Handler
                  var tokenHandler = new JwtSecurityTokenHandler();

                  //* Create Token With Credentials
                  var token = tokenHandler.CreateToken(TokenDesc);

                  //* Return Token to User
                  return tokenHandler.WriteToken(token);
            }
      }
}