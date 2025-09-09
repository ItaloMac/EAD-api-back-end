using System;

namespace Application.DTOs.Admin.Gateway.Payment;

public class CheckoutResponseDTO
{
    public string Id { get; set; } = string.Empty; // ✅ ID do checkout
    public string Object { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    
    // ✅ Campo calculado para o link do checkout
    public string CheckoutUrl { get; set; } = string.Empty;
}