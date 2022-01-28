using System;
using System.Collections.Generic;

namespace GameLeaderboards.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<Leaderboard> Leaderboards { get; set; }
    }
}
