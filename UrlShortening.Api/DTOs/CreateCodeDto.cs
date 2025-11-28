namespace UrlShortening.Api.DTOs
{
    public sealed class CreateCodeDto
    {
        public string OriginalUrl { get; init; } = string.Empty;
    }
}
