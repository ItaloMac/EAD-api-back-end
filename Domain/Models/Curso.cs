using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;

namespace webAPI.Domain.Models {
    public class Curso
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public bool Status { get; set; }
        
        [Column(TypeName = "varchar(255)")]
        public required string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Type { get; set; }

        [Column(TypeName = "longtext")]
        public string Presentation { get; set; } = null!
;
        [Column(TypeName = "varchar(255)")]
        public required string StartForecast { get ; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Modality { get ; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Location { get ; set; }

        [Column(TypeName = "varchar(100)")]
        public required string Workload { get ; set; }

        [Column(TypeName = "varchar(500)")]
        public required string Duration { get ; set; }

        [Column(TypeName = "longtext")]
        public required string Proposal { get ; set; }

        [Column(TypeName = "longtext")]
        public required string Requirements { get ; set; }

        [Column(TypeName = "longtext")]
        public required string Documentation { get ; set; }

        [Column(TypeName = "longtext")]
        public required string Curriculum { get ; set; }

        [Column(TypeName = "varchar(64)")]
        public required string RegistrationPrice { get ; set; }

        [Column(TypeName = "varchar(64)")]
        public required string MonthlyPrice { get ; set; }

        [Column(TypeName = "varchar(64)")]
        public required string TotalPrice { get ; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Installments { get; set; }

        [Column(TypeName = "varchar(50)")]
        public required string CashPrice { get; set; }

        [Column(TypeName = "varchar(50)")]
        public required string FullPrice { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Discount { get; set; } = null!;

        [Required]
        public string ImagemUrl { get; set; } = null!;

        //RELACIONAMENTO 1.N
        [Required]
        public List<Modulo> Modulos { get; set; } = null!;

        public Guid CoordenadorId { get; set; }

        // Lista de relacionamentos com a tabela intermediária CursoProfessor        
        [ForeignKey("CoordenadorId")]
        public Professor Coordenador { get; set; } = null!;

        public List<CursoProfessor> CursoProfessores { get; set; } = new();
    }
}