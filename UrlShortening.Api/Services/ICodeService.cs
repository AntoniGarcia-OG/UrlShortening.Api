using UrlShortening.Api.DTOs;
using UrlShortening.Api.DTOs.Analytics;
using UrlShortening.Api.Models;

namespace UrlShortening.Api.Services
{
    public interface ICodeService
    {
        Task<CodeResponseDto?> FindOneAsync(string code);

        Task<CodeResponseDto?> ResolveAndUpdateDataAsync(string code, HttpContext httpContext);
        Task<CodeResponseDto> CreateAsync(CreateCodeDto dto);

        Task<CodeAnalyticsDto?> GetCodeAnalyticsAsync(string code);
    }
}
