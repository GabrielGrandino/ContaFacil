using ContaFacil.Application.People.Commands;
using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ContaFacil.Infrastructure.Persistence.Repository;
using Microsoft.OpenApi.Models;
using System.Reflection;
using ContaFacil.Application.Common.Interface;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Configuração do Serielog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        fileSizeLimitBytes: 10_000_000,
        rollOnFileSizeLimit: true,
        shared: true
    )
    .CreateLogger();

builder.Host.UseSerilog();


//Configuração do banco de dados
builder.Services.AddDbContext<ContaFacilDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Add services to the container.
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreatePessoaCommand).Assembly)
);

//Repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ContaFacil API",
        Version = "v1",
        Description = "API para gerenciamento de pessoas",
        Contact = new OpenApiContact
        {
            Name = "Sua Equipe",
            Email = "suporte@contafacil.com"
        }
    });

    // Configuração para ler os comentários XML
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEnd", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://localhost:3000"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

app.UseGlobalExceptionHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContaFacil API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseCors("FrontEnd");

app.UseAuthorization();

app.MapControllers();

app.Run();
