using System;
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
            var curso = await _cursoService.GetCursoByIdAsync(id);

            if(curso == null)
                return NotFound("Curso não encontrado.");

            return Ok(curso);
        }
    }
}


