using Application.DTOs.Admin.Class;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.ClassControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class CreateClassController : ControllerBase
{
    private readonly IClassServices _classService;
    private readonly IUserService _userService;

    public CreateClassController(IClassServices classService, IUserService userService)
    {
        _classService = classService;
        _userService = userService;
    }

    [Authorize]
    [HttpPost("turmas/create")]
    public async Task<IActionResult> CreateClassAsync([FromBody] CreateClassDTO dto)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var newClass = await _classService.CreateClassAsync(dto);
            return Ok(newClass);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao criar a turma: {message}");
        }
    }
}
