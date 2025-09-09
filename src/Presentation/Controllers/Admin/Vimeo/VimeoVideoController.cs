using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.Vimeo;

[ApiController]
[Route("api/admin/vimeo")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class VimeoVideoController : ControllerBase
{
    private readonly VimeoAuthService _authService;

    public VimeoVideoController(VimeoAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file, [FromQuery] string accessToken)
    {
        // Se você já persistiu o token do OAuth (ex: no banco ou cache), pode buscar lá.
        var videoService = new VimeoVideoService(accessToken);

        using var stream = file.OpenReadStream();
        var videoId = await videoService.UploadVideoAsync(stream, file.FileName, "Aula EAD");

        return Ok(new { VideoId = videoId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, [FromQuery] string accessToken)
    {
        var videoService = new VimeoVideoService(accessToken);
        var url = await videoService.GetVideoUrlAsync(id);
        return Ok(new { Url = url });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, [FromQuery] string accessToken)
    {
        var videoService = new VimeoVideoService(accessToken);
        await videoService.DeleteVideoAsync(id);
        return NoContent();
    }
}