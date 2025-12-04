using System.ComponentModel.DataAnnotations;

namespace Identity_10_.Services.User.Email
{
    public class EmailSetting
    {
        [Required]
        public string Mail { get; set; } = string.Empty;
        [Required]
        public string DisplayName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Host { get; set; } = string.Empty;
        [Range(1, int.MaxValue)]
        public int Port { get; set; } 
    }
}
