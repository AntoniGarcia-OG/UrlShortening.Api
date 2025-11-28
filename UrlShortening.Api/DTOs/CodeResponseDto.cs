namespace UrlShortening.Api.DTOs
{
    public sealed class CodeResponseDto
    {
        public string OriginalUrl { get; init; } = string.Empty;        
        public string Code { get; init; } = string.Empty;

        public DateTime CreatedAt { get; init; }
    }
}
