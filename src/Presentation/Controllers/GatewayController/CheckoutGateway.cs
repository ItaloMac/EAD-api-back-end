using Application.DTOs.Admin.Gateway.Payment;
using Application.Services.Admin.GatewayService;
using Microsoft.AspNetCore.Mvc;

namespace InvictusAPI.Presentation.Controllers.GatewayController;

[ApiController]
[Route("api/[controller]")]
public class AsaasCheckoutGatewayController : ControllerBase
{
    private readonly CheckoutService _checkoutService;

    public AsaasCheckoutGatewayController(CheckoutService checkoutService)
    {
        _checkoutService = checkoutService;
    }

    [HttpPost]
    public async Task<ActionResult<CheckoutResponseDTO>> CreateCheckout([FromBody] CheckoutRequestDTO request)
    {
        try
        {
            var result = await _checkoutService.CreateCheckout(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}