using UrlShortening.Api.DTOs;

namespace UrlShortening.Api.Services
{
    public interface ICodeService
    {
        Task<CodeResponseDto?> FindOneAsync(string code);

        Task<CodeResponseDto?> ResolveAndUpdateDataAsync(string code, HttpContext httpContext);
        Task<CodeResponseDto> CreateAsync(CreateCodeDto dto);
    }
}
