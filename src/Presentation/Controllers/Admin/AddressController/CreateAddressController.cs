using Application.DTOs.Admin.Address;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Admin.AddressController;

[ApiController]
[Route("api/admin/address")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]
public class CreateAddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public CreateAddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CreateAddressDTO>> CreateAddress([FromBody] CreateAddressDTO createAddressDTO)
    {
        try
        {
            var createdAddress = await _addressService.CreateAddressAsync(createAddressDTO);
            return Created($"api/admin/address/{createdAddress}", createdAddress);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
