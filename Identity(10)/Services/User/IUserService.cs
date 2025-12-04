using Identity_10_.ErrorHandeling;
using Identity_10_.Services.User.DTO;
using System.Linq.Expressions;

namespace Identity_10_.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<Result<string>> RegisterAsync(RegisterDto user);
        Task<Result<string>> LoginAsync(LoginDto user);
        Task<(bool, string)> ResetPasswordAsync(ResetPassword reset);
        Task<Result<string>> ConfirmEmailAsync(ConfirmEmailAsyncDto Email);

    }
}
