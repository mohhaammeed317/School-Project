//using Fluent.Infrastructure.FluentModel;

using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using schoolproject.core;
using schoolproject.core.middleware;
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
#region Localization
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt => { opt.ResourcesPath = "";});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("de-DE"),
        new CultureInfo("fr-FR"),
        new CultureInfo("ar-EG")
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

#endregion




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

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();
#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion


app.UseAuthorization();

app.MapControllers();

app.Run();
