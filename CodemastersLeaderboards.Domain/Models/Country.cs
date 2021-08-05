using System.Collections.Generic;

namespace CodemastersLeaderboards.Domain.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Leaderboard> Leaderboards { get; set; }
    }
}
