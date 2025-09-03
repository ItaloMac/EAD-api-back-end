using System;
using Application.DTOs.Admin.GatewayDTO;
using Application.Services.Admin.GatewayService;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.Admin.GatewayController.CustomerControllers;

[ApiController]
[Route("api/admin")]
[ApiExplorerSettings(GroupName = "v1")]
[Tags("Portal Admin")]

public class CreateCustomerController : ControllerBase
{
    private readonly GatewayService _gatewayService;
    public CreateCustomerController(GatewayService gatewayService)
    {
        _gatewayService = gatewayService;
    }

    [HttpPost("criar-cliente")]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequestDTO customerRequestDTO)
    {
        try
        {
            var result = await _gatewayService.CreateCustomer(customerRequestDTO);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
