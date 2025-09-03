using System;

namespace Application.DTOs.Admin.GatewayDTO;

public class CustomerResponseDTO
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string CpfCnpj { get; set; } = null!;
    public string ExternalReference { get; set; } = null!;
}

public class CustomerRequestDTO
{
    public string Name { get; set; } = null!;
    public string CpfCnpj { get; set; } = null!;
    public string ExternalReference { get; set; } = null!;
}