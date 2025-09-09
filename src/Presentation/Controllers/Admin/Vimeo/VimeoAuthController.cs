using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.Vimeo;

[ApiController]
[Route("api/auth/vimeo")]
public class VimeoAuthController : ControllerBase
{
    private readonly VimeoAuthService _authService;

    public VimeoAuthController(VimeoAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        var url = _authService.GetLoginUrl("state123");
        return Ok(new { LoginUrl = url });
    }

   [HttpGet("callback")]
public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state)
{
    try
    {
        if (string.IsNullOrEmpty(code))
            return BadRequest("Código de autorização não recebido");

        var token = await _authService.GetAccessTokenAsync(code);
        
        // Aqui você deve PERSISTIR o token (banco, cache, etc)
        // Não retorne o token diretamente em produção!
        
        return Ok(new { 
            Success = true, 
            AccessToken = token,
            Message = "Autenticação realizada com sucesso" 
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { 
            Success = false, 
            Error = ex.Message 
        });
    }
}
}


