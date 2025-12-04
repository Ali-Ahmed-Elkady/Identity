using Identity_10_.ErrorHandeling;

namespace Identity_10_.Services.User
{
    public static class UserErrors
    {
        public static readonly Error InvalidCredintials = 
            new ("User.InvalidCredintials", "The provided credintials are invalid.");
        public static readonly Error NotFound =
            new Error("User.NotFound", "User Not Found");
        public static readonly Error UserCreationFailed =
            new Error("User.Registeration", "User Registration Failed");
        public static readonly Error EmailAlreadyExists =
            new Error("User.Registeration", "Email Already Exists");
        public static readonly Error InvalidCode =
            new Error("User.InvalidCode", "Email Is Not Confirmed");
        public static readonly Error EmailConfirmed =
            new Error("User.DublicatedConfirmation", "Email Already Confirmed");
    }
}
