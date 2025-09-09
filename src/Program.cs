using Application.Interfaces;
using Infrastucture;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Application.Mappers;
using Microsoft.OpenApi.Models;
using Domain.Models;
using Resend;
using DotNetEnv;
using Application.Interfaces.Admin;
using Application.Services.Admin.UserServices;
using Application.Services.Admin.RegistrationService;
using Application.Services.Admin;
using InvictusAPI.swagger;
using Application.Services.Admin.TeacherService;
using Application.Services.Admin.ClassService;
using Application.Services.Admin.ModuleService;
using Application.Services.Admin.AulaService;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InvictusAPI.jwt;
using Application.Services.Admin.AddressService;
using Infrastucture.Services.Vimeo;
using Application.Services.Admin.GatewayService;
using Application.Interfaces.Admin.GatewayInterface;
using Infrastucture.Services.Gateway;
using Infrastucture.Services.ViaCep;

var builder = WebApplication.CreateBuilder(args);

// Carregar variáveis de ambiente
DotNetEnv.Env.Load();

var environment = builder.Environment.EnvironmentName;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

// Configuração do DbContext
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Define a URL do frontend (que também varia por ambiente)
var frontendUrl = builder.Configuration["Frontend:BaseUrl"]
    ?? throw new InvalidOperationException("Frontend URL not configured.");


// JWT Settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);

var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"]
    };
});

builder.Services.AddAuthorization();
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

// Configuração do Swagger
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

    c.DocumentFilter<TagConfiguration>();
});



// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(frontendUrl)
              .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")
              .WithHeaders("Content-Type", "Authorization");
    });
});

// Registro dos serviços
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IModuloService, ModuloService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourseServices, CourseService>();
builder.Services.AddScoped<ITeacherServices, TeacherService>();
builder.Services.AddScoped<IClassServices, ClassService>();
builder.Services.AddScoped<IRegistrationService, RegistrationServices>();
builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<IAulasService, AulaService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ConsultaViaCepService>();

// ✅ INJEÇÃO DOS SERVIÇOS (sem AsaasSettings class)
builder.Services.AddScoped<ICheckoutGateway, AsaasCheckoutGateway>();
builder.Services.AddScoped<CheckoutService>();




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
app.UseMiddleware<ExceptionMiddleware>();

// Configuração de arquivos estáticos, roteamento, CORS, autenticação e autorização
app.UseStaticFiles();
app.UseRouting();
app.UseCors("FrontendPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Inicia o aplicativo
app.Run();
