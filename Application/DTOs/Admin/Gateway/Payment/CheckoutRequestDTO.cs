using System;

namespace Application.DTOs.Admin.Gateway.Payment;

public class CheckoutRequestDTO
{
    public string? CustomerId { get; set; } // Se já existe no Asaas
    public CustomerDataDTO? CustomerData { get; set; } // Se ainda não existe
    public decimal Value { get; set; }
    public string? Description { get; set; }
    
    // Campos obrigatórios para checkout
    public string SuccessUrl { get; set; } = string.Empty;
    public string BackUrl { get; set; } = string.Empty;
    public string? CancelUrl { get; set; } // Opcional, se não informado usa BackUrl
    public string ExternalReference { get; set; } = Guid.NewGuid().ToString();
    
    // Campos opcionais para configuração do checkout
    public string[] PaymentMethods { get; set; } = new[] { "CREDIT_CARD", "PIX" };
    public int? PixExpirationSeconds { get; set; } = 3600;
    public bool EnableCreditCard { get; set; } = true;
    public bool EnablePix { get; set; } = true;
}