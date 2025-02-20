using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webAPI.Domain.Models {
    public class Curso
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public bool Status { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public required string Name { get; set; }

        [Column(TypeName = "varchar(200)")]
        public required string StartForecast { get ; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Modality { get ; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Location { get ; set; }

        [Column(TypeName = "varchar(100)")]
        public required string Workload { get ; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Duration { get ; set; }

        [Column(TypeName = "varchar(200)")]
        public required string PedagogicalCoordination { get ; set; }

        [Column(TypeName = "varchar(200)")]
        public required string Proposal { get ; set; }

        [Column(TypeName = "varchar(200)")]
        public required string Requirements { get ; set; }

        [Column(TypeName = "varchar(200)")]
        public required string Documentation { get ; set; }

        [Column(TypeName = "varchar(500)")]
        public required string Faculty { get ; set; }

        [Column(TypeName = "varchar(500)")]
        public required string Curriculum { get ; set; }

        [Column(TypeName = "varchar(50)")]
        public required string RegistrationPrice { get ; set; }

        [Column(TypeName = "varchar(50)")]
        public required string MonthlyPrice { get ; set; }

        [Column(TypeName = "varchar(50)")]
        public required string TotalPrice { get ; set; }
    }
}