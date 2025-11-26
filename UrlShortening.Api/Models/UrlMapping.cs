namespace UrlShortening.Api.Models
{
    public class UrlMapping
    {
        public int Id { get; set; }
        
        public string OriginalUrl { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        public int HitCount { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
