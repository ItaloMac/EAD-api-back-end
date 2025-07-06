using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InvictusAPI.swagger;

 public class TagConfiguration : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag>
            {
                new OpenApiTag { Name = "Portal Admin", Description = "Rotas do painel administrativo" },
                new OpenApiTag { Name = "Portal do Aluno", Description = "Funcionalidades acessíveis aos alunos" },
                new OpenApiTag { Name = "Site", Description = "Endpoints públicos do site institucional" }
            };
        }
    }