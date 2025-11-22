using FluentValidation;
using Identity_10_.Services.User.DTO;

namespace Identity_10_.Services.User.Verfication
{
    public class RegisterVerfication :AbstractValidator<RegisterDto>
    {
        public RegisterVerfication() 
        {
           
        }
    }
}
