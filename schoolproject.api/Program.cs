//using Fluent.Infrastructure.FluentModel;
using Microsoft.EntityFrameworkCore;
using schoolproject.infrastructure.abstracts;
using schoolproject.infrastructure.data;
using schoolproject.infrastructure.repositories;
using schoolproject.infrastructure;
using schoolproject.service;
using schoolproject.core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


builder.Services.AddInfrastructureDependencies().AddServiceDependencies().AddCOREDependencies();


//Connection SQL
builder.Services.AddDbContext<applicationdbcontext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
});



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
