using IncomeTax.DbContexts;
using IncomeTax.InterfaceImplementations;
using IncomeTax.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IIncomeTaxService, IncomeTaxService>();
builder.Services.AddScoped<IIncomeTaxRepository, IncomeTaxRepository>();

builder.Services.AddDbContext<IncomeTaxDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TaxConnection")));

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
