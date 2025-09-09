using Application.DTOs.Admin.Gateway.Payment;
using Application.Interfaces.Admin.GatewayInterface;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Text.Json;

namespace Infrastucture.Services.Gateway;

public class AsaasCheckoutGateway : ICheckoutGateway
{
    private readonly RestClient _client;
    private readonly string _apiKey;
    private readonly string? _defaultSuccessUrl;
    private readonly string? _defaultBackUrl;

    public AsaasCheckoutGateway(IConfiguration configuration)
    {
        var baseUrl = configuration["AsaasSettings:BaseUrl"];
        _apiKey = configuration["AsaasSettings:ApiKey"]!;
        var options = new RestClientOptions(baseUrl!);
        _client = new RestClient(options);
        _defaultSuccessUrl = configuration["AsaasSettings:SuccessUrl"];
        _defaultBackUrl = configuration["AsaasSettings:BackUrl"];
    }

    public async Task<CheckoutResponseDTO> CreateCheckout(CheckoutRequestDTO request)
    {
        var restRequest = new RestRequest("v3/checkouts", Method.Post);

        restRequest.AddHeader("access_token", _apiKey);
        restRequest.AddHeader("accept", "application/json");
        restRequest.AddHeader("content-type", "application/json");

        string customerId;

        if (!string.IsNullOrEmpty(request.CustomerId))
        {
            customerId = request.CustomerId;
        }
        else if (request.CustomerData != null)
        {
            customerId = await CreateCustomer(request.CustomerData);
        }
        else
        {
            throw new ArgumentException("É necessário informar CustomerId ou CustomerData");
        }

        var externalReference = Guid.NewGuid().ToString();

        var body = new
        {
            name = TruncateString(request.Description ?? "Pagamento", 30),
            description = request.Description ?? "Pagamento de serviço",
            value = request.Value,
            billingTypes = request.PaymentMethods,
            chargeTypes = new[] { "DETACHED", "INSTALLMENT" },

            items = new[]
            {
                new
                {
                    name = TruncateString(request.Description ?? "Curso", 30),
                    value = request.Value,
                    quantity = 1
                }
            },

            callback = new
            {
                successUrl = !string.IsNullOrEmpty(request.SuccessUrl) ? request.SuccessUrl : _defaultSuccessUrl,
                backUrl = !string.IsNullOrEmpty(request.BackUrl) ? request.BackUrl : _defaultBackUrl,
                cancelUrl = !string.IsNullOrEmpty(request.CancelUrl) ? request.CancelUrl :
                           (!string.IsNullOrEmpty(request.BackUrl) ? request.BackUrl : _defaultBackUrl),
                autoRedirect = true
            },

            creditCard = new
            {
                enabled = request.EnableCreditCard,
                installmentOptions = new[]
                {
                    new { value = request.Value, installmentCount = 12 }
                }
            },

            pix = new
            {
                enabled = request.EnablePix,
                expiresIn = request.PixExpirationSeconds
            },

            customer = customerId,
            externalReference = externalReference
        };

        restRequest.AddJsonBody(body);

        var response = await _client.ExecuteAsync(restRequest);

        if (!response.IsSuccessful)
        {
            throw new Exception($"Falha ao criar checkout: {response.StatusCode} - {response.Content}");
        }

        try
        {
            using var jsonDoc = JsonDocument.Parse(response.Content!);
            var root = jsonDoc.RootElement;

            var checkoutId = root.GetProperty("id").GetString();

            if (string.IsNullOrEmpty(checkoutId))
            {
                throw new Exception("Resposta da API não contém ID do checkout");
            }

            var checkoutResponse = new CheckoutResponseDTO
            {
                Id = checkoutId,
                Object = root.TryGetProperty("object", out var objProp) ? objProp.GetString() ?? "" : "",
                DateCreated = root.TryGetProperty("dateCreated", out var dateProp) &&
                             DateTime.TryParse(dateProp.GetString(), out var date) ? date : DateTime.UtcNow,
                Status = root.TryGetProperty("status", out var statusProp) ? statusProp.GetString() ?? "" : "",
                Description = request.Description ?? "Pagamento",
                Value = request.Value,
                CheckoutUrl = $"https://sandbox.asaas.com/checkoutSession/show?id={checkoutId}"
            };

            return checkoutResponse;
        }
        catch (JsonException ex)
        {
            throw new Exception($"Erro ao processar resposta do checkout: {ex.Message}");
        }
        catch (KeyNotFoundException ex)
        {
            throw new Exception($"Campo obrigatório não encontrado na resposta: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro inesperado ao processar checkout: {ex.Message}");
        }
    }

    private async Task<string> CreateCustomer(CustomerDataDTO customerData)
    {
        var customerRequest = new RestRequest("v3/customers", Method.Post);
        customerRequest.AddHeader("access_token", _apiKey);
        customerRequest.AddHeader("accept", "application/json");
        customerRequest.AddHeader("content-type", "application/json");

        if (string.IsNullOrEmpty(customerData.Name))
            throw new ArgumentException("Nome do cliente é obrigatório");

        if (string.IsNullOrEmpty(customerData.CpfCnpj))
            throw new ArgumentException("CPF/CNPJ do cliente é obrigatório");

        var customerBody = new
        {
            name = customerData.Name,
            cpfCnpj = customerData.CpfCnpj.Replace(".", "").Replace("-", "").Replace("/", ""),
            email = customerData.Email,
            phone = FormatPhone(customerData.Phone),
            address = customerData.Address,
            addressNumber = customerData.AddressNumber,
            postalCode = customerData.PostalCode?.Replace("-", ""),
            province = customerData.Province,
            city = customerData.City,
            description = customerData.Description,
            externalReference = customerData.ExternalReference,
            notificationDisabled = customerData.NotificationDisabled ?? false,
        };

        customerRequest.AddJsonBody(customerBody);

        var response = await _client.ExecuteAsync(customerRequest);

        if (!response.IsSuccessful)
        {
            throw new Exception($"Falha ao criar cliente: {response.StatusCode} - {response.Content}");
        }

        try
        {
            using var jsonDoc = JsonDocument.Parse(response.Content!);
            if (jsonDoc.RootElement.TryGetProperty("id", out var idProperty))
            {
                var customerId = idProperty.GetString();
                if (!string.IsNullOrEmpty(customerId))
                {
                    return customerId;
                }
            }

            throw new Exception("Resposta da API não contém ID do cliente");
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao processar resposta do cliente: {ex.Message}");
        }
    }

    private string? FormatPhone(string? phone)
    {
        if (string.IsNullOrEmpty(phone)) return null;

        var digitsOnly = new string(phone.Where(char.IsDigit).ToArray());

        return digitsOnly;
    }

    private static string TruncateString(string input, int maxLength)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        if (input.Length <= maxLength)
            return input;

        return input.Substring(0, Math.Max(0, maxLength - 3)) + "...";
    }
}