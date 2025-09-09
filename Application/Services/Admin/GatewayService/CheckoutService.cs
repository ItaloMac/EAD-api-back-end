using Application.DTOs.Admin.Gateway.Payment;
using Application.Interfaces.Admin.GatewayInterface;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Admin.GatewayService;

public class CheckoutService
{
    private readonly ICheckoutGateway _checkoutGateway;
    private readonly IConfiguration _configuration;

    public CheckoutService(ICheckoutGateway checkoutGateway, IConfiguration configuration)
    {
        _checkoutGateway = checkoutGateway;
        _configuration = configuration;

    }

    public async Task<CheckoutResponseDTO> CreateCheckout(CheckoutRequestDTO dto)
    {
        if (string.IsNullOrEmpty(dto.CustomerId) && dto.CustomerData == null)
            throw new ArgumentException("É necessário informar o CustomerId ou CustomerData.");
        
    if (string.IsNullOrEmpty(dto.SuccessUrl))
        dto.SuccessUrl = _configuration["AsaasSettings:SuccessUrl"]!;
        
    if (string.IsNullOrEmpty(dto.BackUrl))
        dto.BackUrl = _configuration["AsaasSettings:BackUrl"]!;

        var response = await _checkoutGateway.CreateCheckout(dto);
        
        response.CheckoutUrl = $"https://sandbox.asaas.com/checkoutSession/show?id={response.Id}";
        
        return response;
    }
}