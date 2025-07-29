using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;

namespace webAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Site")]

    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorService _professorService;

        //Injecao de dependencia via construtor
        public ProfessorController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet("{id}/professores")]
        public async Task<IActionResult> GetProfessoresByCursoId(Guid id)
        {
            try
            {
                var professores = await _professorService.GetProfessoresByIdCursoAsync(id);

                if (professores is null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Title = "O professor informado não foi encontrado",
                        Detail = $"O professor com ID {id} não foi encontrado.",
                        Status = 404
                    });
                }

                return Ok(professores);

            }

            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Title = "Ocorreu um erro na sua solicitação, tente novamente.",
                    Detail = ex.Message,
                    Status = 500
                });
            }
        }

        [HttpGet("corpo-docente")]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var professores = await _professorService.GetAllProfessores();
                if (professores is null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Title = "Não foi possível listar os professores",
                        Detail = $"Os professores não foram encontrados.",
                        Status = 404
                    });
                }
                return Ok(professores);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Title = "Ocorreu um erro na sua solicitação, tente novamente.",
                    Detail = ex.Message,
                    Status = 500
                });
            }
        }
    }
}


