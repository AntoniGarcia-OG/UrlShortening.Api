using Microsoft.AspNetCore.Mvc;
using UrlShortening.Api.DTOs;
using UrlShortening.Api.Services;

namespace UrlShortening.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public sealed class UrlMappingController(ICodeService codeService) : ControllerBase
    {
        private readonly ICodeService _codeService = codeService;

        [HttpGet("{code}")]
        public async Task<ActionResult<CodeResponseDto>> FindOne(string code)
        {
            var result = await _codeService.FindOneAsync(code);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CodeResponseDto>> Create([FromBody] CreateCodeDto dto)
        {
            var result = await _codeService.CreateAsync(dto);

            return CreatedAtAction(nameof(FindOne), new
            {
                code = result.Code
            }, result);
        }

        [HttpGet("/redirect/{code}")]
        public async Task<IActionResult> RedirectionByCode(string code)
        {
            var result = await _codeService.FindOneAsync(code);

            if (result is null)
            {
                return NotFound();
            }

            return Redirect(result.OriginalUrl);
        }
    }
}
