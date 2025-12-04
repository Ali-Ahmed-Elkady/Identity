using AutoMapper;
using Identity_10_.Authentaction;
using Identity_10_.ErrorHandeling;
using Identity_10_.Helper;
using Identity_10_.Models.Entities;
using Identity_10_.Services.User.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Identity_10_.Services.User
{
    public class UserService(
        UserManager<AppUser> user ,SignInManager<AppUser> signIn ,IJwtProvider jwtProvider ,IMapper mapper ,IEmailSender emailSender) 
        : IUserService
    {
        private readonly UserManager<AppUser> _user = user;
        private readonly SignInManager<AppUser> _signIn = signIn;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailSender _emailSender = emailSender;

        public async Task<Result<string>> ConfirmEmailAsync(ConfirmEmailAsyncDto Email)
        {
            try
            {
                if (await _user.FindByIdAsync(Email.UserId.ToString()) is not { } user)
                    return Result.Failure<string>(UserErrors.NotFound);
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Email.Code));
                var result = await _user.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                    return Result.Success("Email Confirmed Successfully");
                return Result.Failure<string>(new Error("EmailConfirmationFailed", string.Join(", ", result.Errors.Select(e => e.Description))));
            }
            catch(Exception ex) 
            {
                return Result.Failure<string>(new Error("EmailConfirmationFailed", ex.Message));
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return _mapper.Map<List<UserDto>>(await _user.Users.ToListAsync());
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            return _mapper.Map<UserDto>(await _user.FindByIdAsync($"{id}"));
        }

        public async Task<Result<string>> LoginAsync(LoginDto user)
        {
            if (await _user.FindByEmailAsync(user.Email)is not { } User)
                return Result.Failure<string>(UserErrors.NotFound);
            var result = await _signIn.PasswordSignInAsync(User, user.password, false, false);
            if (result.Succeeded)
            {
                var (token, expireInMinutes) = _jwtProvider.GenerateJwtToken(User);
                return Result.Success<string>(token);
            }
            else
                return Result.Failure<string>(UserErrors.InvalidCredintials);
        }

        private async Task SendEmail(AppUser user )
        {
            var emailBody = EmailBodyBuilder.GenerateEmailBody("Email", new Dictionary<string, string> {
                        {"{{Name}}", user.UserName! } ,
                        { "{{action_url}}",user.Email!}
                    });
            await _emailSender.SendEmailAsync(user.Email!, "Confirm your email", emailBody);
        }
        public async Task<Result<string>> RegisterAsync(RegisterDto user)
        {
            try
            {
                if (await _user.FindByEmailAsync(user.Email) is not null)
                    return Result.Failure<string>(UserErrors.EmailAlreadyExists);

                var appUser = _mapper.Map<AppUser>(user);
                var result = await _user.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    var token = await _user.GenerateEmailConfirmationTokenAsync(appUser);
                    var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                    //return Result.Success(code);
                    // In a real application, you would send the token via email here.
                    await SendEmail(appUser);
                }
                return Result.Failure<string>(new Error("User Creation Failed" ,string.Join(", ", result.Errors.Select(e => e.Description))));
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(new Error("UserCreationFailed", ex.Message));
            }
        }

        public Task<(bool, string)> ResetPasswordAsync(ResetPassword reset)
        {
            throw new NotImplementedException();
        }
    }
}
