using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.Model
{
      public class User
      {
            public string Name { get; set; }
            [Key]
            public string Username { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Location { get; set; }
            public string Role { get; set; }
            public byte[] PasswordHash { get; set; }
            public byte[] PasswordSalt { get; set; }
            public ICollection<Events> UserEvents { get; set; }
            public ICollection<Reviews> User_Event_Reviews { get; set; }

      }
}
namespace API.ModelValidation
{

      public class UserValidator : AbstractValidator<User>
      {
            public UserValidator()
            {
                  RuleFor(x => x.Name).NotEmpty().Length(5, 30).WithMessage("Please specify a first name");
                  RuleFor(x => x.Username).NotEmpty().Length(5, 30).WithMessage("Please specify a Valid Username");
                  RuleFor(x => x.Email).EmailAddress().WithMessage("Enter a Valid Email Address");
                  RuleFor(x => x.Phone).NotNull().NotEmpty().Length(10, 10).WithMessage("Enter a Valid Phone Number");
                  RuleFor(x => x.Location).NotNull().NotEmpty().WithMessage("Enter a Valid Location");
                  RuleFor(x => x.Role).Must(x =>
                  {
                        string[] roles = { "user", "admin", "hall_owner" };
                        foreach (string r in roles)
                        {
                              if (x.Equals(r)) return true;
                        }
                        return false;
                  }).WithMessage("Invalid Access Role");
                  // RuleFor(x => x.PasswordHash).NotNull().NotEmpty();
            }

      }
}