using System.Collections.Generic;

namespace GameLeaderboards.Domain.Models.Dto
{
    public class LeaderboardOutputDto
    {
        public class DataList
        {
            public string Username { get; set; }
            public string Country { get; set; }
            public string Vehicle { get; set; }
            public string Time { get; set; }
            public string Platform { get; set; }
            public int RaceId { get; set; }
        }

        public int TotalCount { get; set; }
        public List<DataList> List { get; set; } = new List<DataList>();
    }
}
