namespace Identity_10_.Services.User.DTO
{
    public record RegisterDto
    (
        string FirstName,
        string LastName,
        string UserName,
        string Email,
        string Password
    );
}
