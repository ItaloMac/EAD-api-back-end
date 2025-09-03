using Application.DTOs.Admin.GatewayDTO;

namespace Application.Interfaces.Admin.GatewayInterface;

public interface ICustomerGateway
{
    Task<CustomerResponseDTO> CreateCustomer(CustomerRequestDTO customerRequestDTO);
    Task<CustomerListResponseDTO> GetAlllCustomers();
    Task<CustomerResponseDTO> GetCustomerById(string customerId);
    Task<CustomerResponseDTO> UpdateCustomer(string customerId, CustomerRequestDTO customerRequestDTO);
    Task<bool> DeleteCustomer(string customerId);
}
