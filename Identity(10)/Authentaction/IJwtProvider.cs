using Identity_10_.Models.Entities;

namespace Identity_10_.Authentaction
{
    public interface IJwtProvider
    {
        (string token , int expireInMinutes) GenerateJwtToken(AppUser user);
    }
}
