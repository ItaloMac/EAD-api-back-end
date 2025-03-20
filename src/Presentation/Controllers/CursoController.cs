using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;

namespace webAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        //Injecao de dependencia via construtor
        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cursos = await _cursoService.GetCursosAsync();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCursoByIdAsync(Guid id)
        {
           
            try
            {
                var curso = await _cursoService.GetCursoByIdAsync(id);

                if (curso is null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Title = "Curso não encontrado",
                        Detail = $"O curso com ID {id} não foi encontrado.",
                        Status = 404
                    });
                }

                return Ok(curso);
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


