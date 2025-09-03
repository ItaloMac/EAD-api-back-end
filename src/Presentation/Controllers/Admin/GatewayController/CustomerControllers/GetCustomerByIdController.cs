using Application.Services.Admin.GatewayService;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.GatewayController.CustomerControllers;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class GetCustomerByIdController : ControllerBase
{

    private readonly GatewayService _gatewayService;
    public GetCustomerByIdController(GatewayService gatewayService)
    {
        _gatewayService = gatewayService;
    }
    [HttpGet("buscar-cliente-por-id/{customerId}")]
    public async Task<IActionResult> GetCustomerById(string customerId)
    {
        try
        {
            var result = await _gatewayService.GetCustomerById(customerId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
