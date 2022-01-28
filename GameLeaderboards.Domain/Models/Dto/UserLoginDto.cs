using System.ComponentModel.DataAnnotations;

namespace GameLeaderboards.Domain.Models.Dto
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
