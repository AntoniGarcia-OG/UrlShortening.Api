using Microsoft.EntityFrameworkCore;
using UrlShortening.Api.Data;
using UrlShortening.Api.DTOs;
using UrlShortening.Api.DTOs.Analytics;
using UrlShortening.Api.Models;

namespace UrlShortening.Api.Services
{
    public sealed class CodeService : ICodeService
    {
        private readonly UrlShorteningDbContext _dbContext;
        private readonly Random random = new();
        
        public CodeService(UrlShorteningDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CodeResponseDto?> FindOneAsync(string code)
        {
            var result = await _dbContext.UrlMappings.AsNoTracking().FirstOrDefaultAsync(data => data.Code == code);

            if (result == null)
            {
                return null;
            }

            return new CodeResponseDto
            {
                OriginalUrl = result.OriginalUrl,
                Code = result.Code,
                CreatedAt = result.CreatedAt
            };
        }

        public async Task<CodeResponseDto?> ResolveAndUpdateDataAsync(string code, HttpContext httpContext)
        {
            var result = await _dbContext.UrlMappings.FirstOrDefaultAsync(data => data.Code == code);

            if (result is null)
            {
                return null;
            }

            result.HitCount++;
            result.LastAccessAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return new CodeResponseDto
            {
                OriginalUrl = result.OriginalUrl,
                Code = result.Code,
                CreatedAt = result.CreatedAt
            };
        }

        public async Task<CodeResponseDto> CreateAsync(CreateCodeDto dto)
        {
            var code = await GenerateCodeAsync();

            var model = new UrlMapping
            {
                OriginalUrl = dto.OriginalUrl,
                Code = code
            };

            _dbContext.UrlMappings.Add(model);
            await _dbContext.SaveChangesAsync();

            return new CodeResponseDto
            {
                OriginalUrl = model.OriginalUrl,
                Code = model.Code,
                CreatedAt = model.CreatedAt
            };
        }        
        
        public async Task<CodeAnalyticsDto?> GetCodeAnalyticsAsync(string code)
        {
            var result = await _dbContext.UrlMappings.AsNoTracking().FirstOrDefaultAsync(data => data.Code == code);

            if (result is null)
            {
                return null;
            }

            return new CodeAnalyticsDto
            {
                Code = result.Code,
                HitCount = result.HitCount,
                LastAccessAt = result.LastAccessAt
            };
        }

        private async Task<string> GenerateCodeAsync()
        {
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            while (true)
            {
                var code = new string([.. Enumerable.Range(0, 8).Select(_ =>
                {
                    return characters[random.Next(characters.Length)];
                })]);

                var alreadyExists = await _dbContext.UrlMappings.AnyAsync(data => data.Code == code);

                if (!alreadyExists)
                {
                    return code;
                }
            }
        }
    }
}
