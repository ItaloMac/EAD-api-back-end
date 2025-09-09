using System;

namespace Application.DTOs.Admin.Course
{
    public class CreateCourseDTO
    {
        public bool Status { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Presentation { get; set; }
        public required string StartForecast { get; set; }
        public required string Modality { get; set; }
        public required string Location { get; set; }
        public required string Workload { get; set; }
        public required string Duration { get; set; }
        public required string Proposal { get; set; }
        public required string Requirements { get; set; }
        public required string Documentation { get; set; }
        public required string Curriculum { get; set; }
        public required string RegistrationPrice { get; set; }
        public required decimal MonthlyPrice { get; set; }
        public required decimal TotalPrice { get; set; }
        public required decimal Installments { get; set; }
        public required decimal CashPrice { get; set; }
        public required decimal FullPrice { get; set; }
        public string Discount { get; set; } = null!;
        public string ImagemUrl { get; set; } = null!;

        // Novo campo para receber o ID do coordenador diretamente
        public Guid CoordenadorId { get; set; }
    }
}
