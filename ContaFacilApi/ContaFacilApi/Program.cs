using ContaFacil.Application.People.Commands;
using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ContaFacil.Infrastructure.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContaFacilDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Add services to the container.
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreatePessoaCommand).Assembly)
);

//Services
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
