using Application.DTOs.Admin.GatewayDTO;
using Application.Interfaces.Admin.GatewayInterface;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Infrastucture.Services.Gateway.CustomerGateway;

public class CustomerGatewayService : ICustomerGateway
{
    private readonly RestClient _client;
    private readonly string _apiKey;

    public CustomerGatewayService(IConfiguration configuration)
    {
        var baseUrl = configuration["AsaasSettings:BaseUrl"];
        _apiKey = configuration["AsaasSettings:ApiKey"]!;

        var options = new RestClientOptions(baseUrl!);
        _client = new RestClient(options);
    }

    public async Task<CustomerResponseDTO> CreateCustomer(CustomerRequestDTO customerRequestDTO)
    {
        var request = new RestRequest("customers");
        request.AddHeader("accept", "application/json");
        request.AddHeader("content-type", "application/json");
        request.AddHeader("access_token", _apiKey);
        request.AddJsonBody(customerRequestDTO);
        
        var response = await _client.PostAsync<CustomerResponseDTO>(request);
        return response!;
    }

    public async Task<CustomerListResponseDTO> GetAlllCustomers()
    {
        var request = new RestRequest("customers");
        request.AddHeader("accept", "application/json");
        request.AddHeader("access_token", _apiKey);
        
        var response = await _client.GetAsync<CustomerListResponseDTO>(request);
        return response!;
    }

    public async Task<CustomerResponseDTO> GetCustomerById(string customerId)
    {
        var request = new RestRequest($"customers/{customerId}");
        request.AddHeader("accept", "application/json");
        request.AddHeader("access_token", _apiKey);
        
        var response = await _client.GetAsync<CustomerResponseDTO>(request);
        return response!;
    }

    public async Task<CustomerResponseDTO> UpdateCustomer(string customerId, CustomerRequestDTO customerRequestDTO)
    {
        var request = new RestRequest($"customers/{customerId}");
        request.AddHeader("accept", "application/json");
        request.AddHeader("content-type", "application/json");
        request.AddHeader("access_token", _apiKey);
        request.AddJsonBody(customerRequestDTO);
        
        var response = await _client.PutAsync<CustomerResponseDTO>(request);
        return response!;
    }
    
    public async Task<bool> DeleteCustomer(string customerId)
    {
        var request = new RestRequest($"customers/{customerId}");
        request.AddHeader("accept", "application/json");
        request.AddHeader("access_token", _apiKey);
        
        var response = await _client.DeleteAsync<bool>(request);
        return response!;
    }
}