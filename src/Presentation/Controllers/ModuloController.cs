using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuloController : ControllerBase
    {
        private readonly IModuloService _moduloService;

        public ModuloController(IModuloService moduloService)
        {
            _moduloService = moduloService;
        }

        [HttpGet("{id}/modulos")]
        public async Task<IActionResult> GetModuloByIdCursoAsync(Guid id)
        {
            try{
                var modulos = await _moduloService.GetModuloByIdCursoAsync(id);
                if(modulos is null)
                {
                    return NotFound(new ProblemDetails{
                        Title = "O modulos referente ao curso informado não foi encontrado",
                        Detail = $"O modulos referentes ao Curso de ID {id} não foi encontrado.",
                        Status = 404
                    });
                }
                return Ok(modulos);
            }
            catch(Exception ex){
                return StatusCode(500, new ProblemDetails{
                        Title = "Ocorreu um erro ao tentar buscar os modulos do curso, tente novamente.",
                        Detail = ex.Message,
                        Status = 500
                });
            }
        }
    }
}

