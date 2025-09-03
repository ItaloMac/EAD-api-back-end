namespace Application.DTOs.Admin.Address;

public class AddressResponseDTO
{
    public Guid Id { get; set; }
    public string CEP { get; set; } = string.Empty;
    public string Road { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}
