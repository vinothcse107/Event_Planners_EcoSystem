namespace API.ModelValidation
{
      public class SignupDTOValidator : AbstractValidator<SignupDTO>
      {
            public SignupDTOValidator()
            {
                  RuleFor(x => x.Name).NotEmpty().Length(5, 30);
                  RuleFor(x => x.Username).NotEmpty().Length(5, 30);
                  RuleFor(x => x.Email).EmailAddress();
                  RuleFor(x => x.Phone).NotNull().NotEmpty().Length(10, 10);
                  RuleFor(x => x.Location).NotNull().NotEmpty();
                  RuleFor(x => x.Role).Must(x =>
                  {
                        string[] roles = { "user", "admin", "hall_owner" };
                        foreach (string r in roles)
                        {
                              if (x.Equals(r)) return true;
                        }
                        return false;
                  }).WithMessage("Invalid Access Role");
            }

      }
}