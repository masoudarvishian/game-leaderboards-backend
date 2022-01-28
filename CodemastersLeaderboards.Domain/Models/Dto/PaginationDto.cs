namespace GameLeaderboards.Domain.Models.Dto
{
    public class PaginationDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int PlatformId { get; set; } = -1; // -1 means all platforms
        public int RaceId { get; set; } = -1; // -1 means all races
    }
}
