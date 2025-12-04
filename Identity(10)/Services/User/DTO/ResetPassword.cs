namespace Identity_10_.Services.User.DTO
{
    public record ResetPassword
    (
       string Email , 
       string OldPassword ,
       string NewPassword
    );
}
