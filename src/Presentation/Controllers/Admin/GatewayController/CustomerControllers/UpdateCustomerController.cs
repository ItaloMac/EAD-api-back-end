using Application.DTOs.Admin.GatewayDTO;
using Application.Services.Admin.GatewayService;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.GatewayController.CustomerControllers;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class UpdateCustomerController : ControllerBase
{
    private readonly GatewayService _gatewayService;
    public UpdateCustomerController(GatewayService gatewayService)
    {
        _gatewayService = gatewayService;
    }

    [HttpPut("atualizar-cliente/{customerId}")]
    public async Task<IActionResult> UpdateCustomer(string customerId, [FromBody] CustomerRequestDTO customerRequestDTO)
    {
        try
        {
            var result = await _gatewayService.UpdateCustomer(customerId, customerRequestDTO);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
