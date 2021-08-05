using System;
using System.Collections.Generic;
using System.Text;

namespace CodemastersLeaderboards.Domain.Models.Dto
{
    public class LeaderboardInputDto
    {
        public int RaceId { get; set; }
        public int CountryId { get; set; }
        public int VehicleId { get; set; }
        public int PlatformId { get; set; }
        public long Time { get; set; }
    }
}
