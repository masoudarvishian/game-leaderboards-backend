using System.ComponentModel.DataAnnotations;

namespace CodemastersLeaderboards.Domain.Models.Dto
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Username should be between 3 and 15 characters")]
        public string Username { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Password should be between 4 and 30 characters")]
        public string Password { get; set; }
    }
}
