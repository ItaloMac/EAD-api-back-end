using Application.Interfaces;
using Infrastucture;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Application.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Domain.Models;
using Resend;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Carregar variáveis de ambiente
DotNetEnv.Env.Load();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configuração do DbContext
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// Configuração hierárquica (variáveis de ambiente > appsettings)
builder.Services.Configure<EmailSettings>(options =>
{
    options.FromEmail =
        Environment.GetEnvironmentVariable("EMAIL_FROM")
        ?? builder.Configuration["EmailSettings:FromEmail"]!; // Replace with a meaningful default value
    
    options.FromName = 
        Environment.GetEnvironmentVariable("EMAIL_FROM_NAME") 
        ?? builder.Configuration["EmailSettings:FromName"]!;
});

//Configuração do ResendClient
builder.Services.AddOptions();
builder.Services.AddHttpClient<ResendClient>();
builder.Services.Configure<ResendClientOptions>( o =>
{
    o.ApiToken = Environment.GetEnvironmentVariable( "RESEND_API_KEY" )!;
} );
builder.Services.AddTransient<IResend, ResendClient>();

// Configuração do Identity
builder.Services.AddIdentityCore<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
})
.AddRoles<IdentityRole<Guid>>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddApiEndpoints();

// Configuração do Swagger
var frontendUrl = "http://localhost:5173";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    c.DocInclusionPredicate((docName, apiDesc) =>
    {
            // Remove endpoints relacionados ao Identity (ForgotPassword, ResetPassword, Register)
            if (apiDesc.ActionDescriptor.EndpointMetadata.Any(em => 
                em is Microsoft.AspNetCore.Identity.Data.ForgotPasswordRequest ||
                em is Microsoft.AspNetCore.Identity.Data.ResetPasswordRequest ||
                em is Microsoft.AspNetCore.Identity.Data.RegisterRequest))
            {
                return false; // Exclui do Swagger
            }
            return true; // Mantém outros endpoints
    });
});

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy => 
    {
        policy.WithOrigins(frontendUrl)
              .WithMethods("GET", "POST", "PUT", "DELETE")
              .WithHeaders("Content-Type", "Authorization");
    });
});

// Registro dos serviços
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IModuloService, ModuloService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();


builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
        options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
    })
    .AddBearerToken(IdentityConstants.BearerScheme, options =>
    {
        options.BearerTokenExpiration = TimeSpan.FromHours(1);
        options.RefreshTokenExpiration = TimeSpan.FromDays(7);
    });

builder.Services.AddAuthorization();
// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(CursoProfile));

// Configuração dos Controllers
builder.Services.AddControllers();
builder.Services.AddProblemDetails();

// Inicialização do aplicativo
var app = builder.Build();

// Configuração do Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Configuração do tratamento de exceções e códigos de status
app.UseExceptionHandler();
app.UseStatusCodePages();

// Mapeamento dos endpoints do Identity (DEPOIS de builder.Build())
app.MapIdentityApi<User>();

// Configuração de arquivos estáticos, roteamento, CORS, autenticação e autorização
app.UseStaticFiles();
app.UseRouting();
app.UseCors("FrontendPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Inicia o aplicativo
app.Run();
