using System.Collections.Generic;

namespace GameLeaderboards.Domain.Models
{
    public class Race
    {
        public int Id { get; set; }

        public int LapCount { get; set; }

        public virtual ICollection<Leaderboard> Leaderboards { get; set; }
    }
}
