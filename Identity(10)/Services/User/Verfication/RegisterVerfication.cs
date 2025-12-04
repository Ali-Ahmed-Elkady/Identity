using FluentValidation;
using Identity_10_.Helper;
using Identity_10_.Services.User.DTO;

namespace Identity_10_.Services.User.Verfication
{
    public class RegisterVerfication :AbstractValidator<RegisterDto>
    {
        public RegisterVerfication() 
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x=>x.UserName).NotEmpty().Length(3, 100);
            RuleFor(x=>x.FirstName).NotEmpty().Length(3, 100);
            RuleFor(x=>x.LastName).NotEmpty().Length(3, 100);
            RuleFor(x=>x.Password).NotEmpty().Matches(RegexPatterns.Password);
        }
    }
}
