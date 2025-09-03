namespace Application.DTOs.Admin.GatewayDTO;

public class CustomerListResponseDTO
{
    public bool HasMore { get; set; }
    public int TotalCount { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public List<CustomerResponseDTO> Data { get; set; } = new List<CustomerResponseDTO>();
}
