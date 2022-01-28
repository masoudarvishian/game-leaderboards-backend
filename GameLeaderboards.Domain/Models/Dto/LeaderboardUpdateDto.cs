using System;
using System.Collections.Generic;
using System.Text;

namespace GameLeaderboards.Domain.Models.Dto
{
    public class LeaderboardUpdateDto
    {
        public int RaceId { get; set; }
        public long Time { get; set; }
    }
}
