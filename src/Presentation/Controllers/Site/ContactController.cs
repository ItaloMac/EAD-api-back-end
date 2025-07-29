using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers;

[ApiController]
[Route("api/[Controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Site")]

public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost]
    public async Task<IActionResult> PostContactAsync([FromBody] CreateContactRequest request)
    {
        try{
            var contact = await _contactService.PostContactAsync(request.Name, request.Email, request.Phone);
            if (contact is null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Recurso não encontrado",
                    Detail = "Não foi possível criar o contato. Recurso necessário não encontrado.",
                    Status = 404
                });
            }

            return Ok(contact);
        } catch(Exception ex)
        {
            return StatusCode(500, new ProblemDetails
            {
                Title = "Erro interno no servidor",
                Detail = ex.Message,
                Status = 500
            });
        }

    }
}