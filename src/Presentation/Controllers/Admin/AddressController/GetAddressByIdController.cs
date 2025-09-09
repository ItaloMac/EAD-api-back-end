using Application.DTOs.Admin.Address;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Admin.AddressController;

[ApiController]
[Route("api/address")]
[Authorize]

public class GetAddressByIdController : ControllerBase
{
    private readonly IAddressService _addressService;

    public GetAddressByIdController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<AddressResponseDTO>> GetAddressById(Guid id)
    {
        try
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            return Ok(address);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
