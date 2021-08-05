using System;
using System.Collections.Generic;
using System.Text;

namespace CodemastersLeaderboards.Domain.Models
{
    public class Leaderboard
    {
        public int UserId { get; set; }
        public int RaceId { get; set; }
        public int CountryId { get; set; }
        public int VehicleId { get; set; }
        public int PlatformId { get; set; }
        public long Time { get; set; }

        public User User { get; set; }
        public Race Race { get; set; }
        public Country Country { get; set; }
        public Vehicle Vehicle { get; set; }
        public Platform Platform { get; set; }
    }
}
