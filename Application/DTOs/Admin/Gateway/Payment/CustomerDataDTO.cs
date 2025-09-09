using System;

namespace Application.DTOs.Admin.Gateway.Payment;

public class CustomerDataDTO
{
    public string Name { get; set; } = string.Empty;
    public string CpfCnpj { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int AddressNumber { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
   
    // Campos adicionais opcionais
    public string? ExternalReference { get; set; }
    public bool? NotificationDisabled { get; set; }
}