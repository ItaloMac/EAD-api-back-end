using Application.DTOs.Admin.Class;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.ClassControllers;
[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class UpdateClassController : ControllerBase
{
    private readonly IClassServices _classService;
    private readonly IUserService _userService;

    public UpdateClassController(IClassServices classService, IUserService userService)
    {
        _classService = classService;
        _userService = userService;
    }

    [Authorize]
    [HttpPut("turmas/{id:guid}/update")]
     public async Task<IActionResult> UpdateClassAsync(Guid id, CreateClassDTO dto)
    {
        try
        {
            var (autorizado, resultado) = await new AuthAdmin(_userService).ValidarAdminAsync(User);

            if (!autorizado)
                return resultado;

            var updateClass = await _classService.UpdateClassAsync(id, dto);
            return Ok(updateClass);

        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            return BadRequest($"Erro ao atualizar a turma: {message}");
        }
    }
}
