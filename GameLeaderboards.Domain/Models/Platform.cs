using System.Collections.Generic;

namespace GameLeaderboards.Domain.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Leaderboard> Leaderboards { get; set; }
    }
}
