using EC2.Context;
using EC2.Models.EFcore;
using EC2.Repository;
using EC2.Service;
using EC2.Service.Implement;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

/// add Serilog addon
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

/// Setting Serilog Logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

try
    
{
    Log.Information("Start Logging...");
    /// Put original program.cs code here

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // EFCore
    var connStr = builder.Configuration.GetConnectionString("Northwind");
    builder.Services.AddDbContext<NorthwindContext>(opt => opt.UseSqlServer(connStr));
    //builder.Services.AddSingleton<NorthwindContext>(s => new NorthwindContext(connStr));

    // Dapper
    builder.Services.AddSingleton<DapperContext>();

    // Register Repositorys to services
    builder.Services.AddTransient<IProductRepository, ProductRepository>();
    builder.Services.AddTransient<ISuppilierRepository, SuppilierRepository>();
    builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
    builder.Services.AddTransient<IProductService, ProductService>();


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
}
catch (Exception e)
{
    Log.Fatal(e, "Host terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}


