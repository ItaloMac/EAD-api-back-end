using Application.Services.Admin.GatewayService;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.GatewayController.CustomerControllers;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class GetAllCustomersController : ControllerBase
{
    private readonly GatewayService _gatewayService;
    public GetAllCustomersController(GatewayService gatewayService)
    {
        _gatewayService = gatewayService;
    }
    [HttpGet("buscar-todos-clientes")]
    public async Task<IActionResult> GetAllCustomers()
    {
        try
        {
            var result = await _gatewayService.GetAlllCustomers();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
