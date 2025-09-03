using System;
using Application.DTOs.Admin.GatewayDTO;
using Application.Interfaces.Admin.GatewayInterface;

namespace Application.Services.Admin.GatewayService;

public class GatewayService
{
    private readonly ICustomerGateway _customerGateway;
    public GatewayService(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    public Task<CustomerResponseDTO> CreateCustomer(CustomerRequestDTO customerRequestDTO)
    {
        return _customerGateway.CreateCustomer(customerRequestDTO);
    }

    public async Task<List<CustomerResponseDTO>> GetAlllCustomers()
    {
        var response = await _customerGateway.GetAlllCustomers();
        return response.Data; 
    }

    public Task<CustomerResponseDTO> GetCustomerById(string customerId)
    {
        return _customerGateway.GetCustomerById(customerId);
    }
    public Task<CustomerResponseDTO> UpdateCustomer(string customerId, CustomerRequestDTO customerRequestDTO)
    {
        return _customerGateway.UpdateCustomer(customerId, customerRequestDTO);
    }
}
