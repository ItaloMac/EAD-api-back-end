using Application.DTOs.Admin.Gateway.Payment;
using Application.Interfaces;
using Application.Interfaces.Admin;
using Application.Interfaces.Admin.GatewayInterface;
using Infrastucture.Services.ViaCep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InvictusAPI.Presentation.Controllers.GatewayController;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FinalizarCompra : ControllerBase
{
    private readonly ICheckoutGateway _checkoutGateway;
    private readonly ICursoService _cursoService;
    private readonly IUserService _userService;
    private readonly IAddressService _addressService;
    private readonly ConsultaViaCepService _consultaViaCepService;
 
    public FinalizarCompra(
        ICheckoutGateway checkoutGateway,
        ICursoService cursoService,
        IUserService userService,
        IAddressService addressService,
        ConsultaViaCepService consultaViaCepService)
    {
        _checkoutGateway = checkoutGateway;
        _cursoService = cursoService;
        _userService = userService;
        _addressService = addressService;
        _consultaViaCepService = consultaViaCepService;
    }

    [HttpPost("comprar-curso/{cursoId}")]
    public async Task<IActionResult> ComprarCurso(Guid cursoId)
    {
        try
        {
            var curso = await _cursoService.GetCursoByIdAsync(cursoId);
            if (curso == null)
                return NotFound("Curso não encontrado");

            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(usuarioId, out var usuarioGuid))
                return BadRequest("ID do usuário inválido");

            var usuario = await _userService.GetUserById(usuarioGuid);
            if (usuario == null)
                return BadRequest("Usuário não encontrado");

            if (usuario.AddressId == null)
            {

            }
                

            // Montar os dados de endereço do checkout caso já existam
                var addressObj = await _addressService.GetAddressByIdAsync(usuario.AddressId ?? Guid.Empty);
            var address = addressObj.Road + " - " + addressObj.Neighborhood;
            var addressNumber = addressObj.Number;
            var addressCEP = addressObj.CEP;
            var addressState = addressObj.State;
            var addressCityCodeIbge = await _consultaViaCepService.GetIbgeCodeFromCepAsync(addressCEP);

            var checkoutRequest = new CheckoutRequestDTO
            {
                Value = curso.TotalPrice,
                Description = $"Curso: {curso.Name}",
                CustomerId = null,
                CustomerData = new CustomerDataDTO
                {
                    Name = usuario.Name + " " + usuario.LastName,
                    CpfCnpj = usuario.CPF?.Replace(".", "").Replace("-", "")!,
                    Email = usuario.Email! ?? "",
                    Phone = usuario.PhoneNumber ?? "",
                    Address = address ?? "",
                    AddressNumber = addressNumber,
                    PostalCode = addressCEP?.Replace("-", "") ?? "",
                    Province = addressState ?? "",
                    City = addressCityCodeIbge,
                    Description = $"Cliente {usuario.Name + " " + usuario.LastName} comprando curso {curso.Name}",
                    ExternalReference = usuarioId,
                    NotificationDisabled = false
                },
                PaymentMethods = new[] { "CREDIT_CARD", "PIX" },
                EnableCreditCard = true,
                EnablePix = true,
                PixExpirationSeconds = 3600,
                SuccessUrl = "",
                BackUrl = "",
                CancelUrl = "",
                ExternalReference = $"CURSO_{cursoId}_USER_{usuarioId}_{DateTime.Now:yyyyMMddHHmmss}"
            };

            var checkoutResponse = await _checkoutGateway.CreateCheckout(checkoutRequest);

            return Ok(new
            {
                Success = true,
                CheckoutUrl = checkoutResponse.CheckoutUrl,
                CheckoutId = checkoutResponse.Id,
                Curso = new
                {
                    Id = curso.Id,
                    Nome = curso.Name,
                    Valor = curso.TotalPrice,
                    Descricao = curso.Modality
                },
                ExternalReference = checkoutRequest.ExternalReference,
                Message = "Checkout criado com sucesso! Você será redirecionado para o pagamento."
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Success = false,
                Message = $"Erro ao processar compra: {ex.Message}"
            });
        }
    }
}