using Application.DTOs.Admin.Gateway.Payment;

namespace Application.Interfaces.Admin.GatewayInterface;

public interface ICheckoutGateway
{
    Task<CheckoutResponseDTO> CreateCheckout(CheckoutRequestDTO request);

}
