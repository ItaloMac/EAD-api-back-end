namespace Application.DTOs.Admin.Address;

public class CreateAddressDTO
{
    public required string CEP { get; set; }
    public required string Road { get; set; }
    public required int Number { get; set; }
    public required string Neighborhood { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
}
