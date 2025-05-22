using PotatisFunctionLab3.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver.Core.Configuration;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

var baseApiUrl = builder.Configuration["BaseApiUrl"];
var functionKey = builder.Configuration["FunctionKey"];

var connectionString = builder.Configuration.GetConnectionString("PotatisCosmosDb")
    ?? throw new InvalidOperationException("Connection string 'PotatisCosmosDb' not found.");

builder.Services.AddScoped<PotatisServices>(_ => new PotatisServices (

    connectionString: connectionString,
    databaseName: "PotatisDb"

));

builder.Build().Run();