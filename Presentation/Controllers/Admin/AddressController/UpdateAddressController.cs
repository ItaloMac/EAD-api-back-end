using Application.DTOs.Admin.Address;
using Application.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Admin.AddressController;

[ApiController]
[Route("api/admin/address")]
public class UpdateAddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public UpdateAddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateAddressDTO>> UpdateAddress(Guid id, [FromBody] UpdateAddressDTO updateAddressDTO)
    {
        try
        {
            var updatedAddress = await _addressService.UpdateAddressAsync(id, updateAddressDTO);
            return Ok(updatedAddress);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
