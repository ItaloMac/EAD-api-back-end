using Application.Interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Infrastucture;
using Application.Mappers;
using Swashbuckle.AspNetCore.Swagger;


var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string" + "'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var frontendUrl = "http://localhost:5173";

builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy => {
        policy.WithOrigins(frontendUrl) // Permite apenas o front-end
              .WithMethods("GET", "POST", "PUT", "DELETE") // Define métodos permitidos
              .WithHeaders("Content-Type", "Authorization"); //
    });
});


builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IModuloService, ModuloService>();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(CursoProfile));
builder.Services.AddControllers();
builder.Services.AddProblemDetails();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler();
app.UseStatusCodePages();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseCors("FrontendPolicy");
app.UseAuthorization();
app.MapControllers(); 

app.Run();
