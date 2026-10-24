//using Fluent.Infrastructure.FluentModel;
using Microsoft.EntityFrameworkCore;
using schoolproject.core;
using schoolproject.infrastructure;
using schoolproject.infrastructure.abstracts;
using schoolproject.infrastructure.data;
using schoolproject.infrastructure.repositories;
using schoolproject.service;
using schoolproject.service.abstracts;
using schoolproject.service.immplemintaion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
/*
 * 
builder.Services.AddTransient<istudentservice, studentservice>();
we replace this line by a line of code that will add all dependencies from the service layer
 
 */

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
