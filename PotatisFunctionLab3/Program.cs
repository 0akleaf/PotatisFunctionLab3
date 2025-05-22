using PotatisFunctionLab3.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver.Core.Configuration;
using AzureFunctions.Extensions.Swashbuckle;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

var connectionString = builder.Configuration.GetConnectionString("PotatisCosmosDb")
    ?? throw new InvalidOperationException("Connection string 'AzureConnection:cosmosdb' not found.");

builder.Services.AddScoped<PotatisServices>(_ => new PotatisServices (

    connectionString: connectionString,
    databaseName: "PotatisDb"

));

builder.Build().Run();



//var builder = Host.CreateApplicationBuilder();
//builder.Services.AddSingleton<PotatisServices>(sp =>
//    new PotatisServices(
//        builder.Configuration["MongoDB:ConnectionString"],
//        builder.Configuration["MongoDB:DatabaseName"]
//    )
//);

//// Add Swagger support
//builder.Services.AddSwashBuckle(assembly: typeof(Program).Assembly);

//var app = builder.Build();

//app.UseSwashBuckle(); // Enable Swagger middleware

//app.Run();