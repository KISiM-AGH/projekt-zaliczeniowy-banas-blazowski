using FluentValidation;
using FluentValidation.AspNetCore;
using Messegify.Application.Middleware;
using Messegify.Application.Services;
using Messegify.Application.Services.Configuration;
using Messegify.Domain.Abstractions;
using Messegify.Domain.Entities;
using Messegify.Domain.Validators;
using Messegify.Infrastructure;
using Messegify.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;

// Add services to the container.
var services = builder.Services;

services.Configure<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<MessegifyDbContext>(contextOptionsBuilder =>
    contextOptionsBuilder.UseMySQL(configuration.GetConnectionString("MessegifyDatabaseConnectionString")));

services.AddValidatorsFromAssembly(typeof(Messegify.Domain.Validators.AssemblyMarker).Assembly);

services.AddScoped<IJwtService, JwtService>();

services.AddScoped<IRepository<Account>, Repository<Account, MessegifyDbContext>>();

services.AddScoped<IHashingService, HashingService>();
services.AddScoped<IAccountService, AccountService>();

services.AddScoped<ErrorHandlingMiddleware>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

