using System.ComponentModel.DataAnnotations;

namespace CodemastersLeaderboards.Domain.Models.Dto
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
