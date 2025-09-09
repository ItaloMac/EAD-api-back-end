using Application.DTOs.Admin.User;
using Application.Interfaces.Admin;
using Infrastucture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.UserControllers;

[ApiController]
[Authorize]
public class UpdateUserAddressController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ApplicationDbContext _context;

    public UpdateUserAddressController(IUserService userService, ApplicationDbContext context)
    {
        _context = context;
        _userService = userService;
    }
    
    [Authorize]
    [HttpPatch("usuarios/{id}/endereco")]
    public async Task<IActionResult> UpdateUserAddress(Guid id, [FromBody] UpdateUserAddressDTO dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound("Usuário não encontrado.");

        user.AddressId = dto.AddressId;
        await _context.SaveChangesAsync();

        return Ok();
}
}
