using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using PotatisFunctionLab3.Models;
using PotatisFunctionLab3.Services;

namespace PotatisFunctionLab3;

public class PotatisFunction
{
    private readonly ILogger<PotatisFunction> _logger;
    private readonly PotatisServices _potatisServices;

    public PotatisFunction(ILogger<PotatisFunction> logger, PotatisServices potatisServices)
    {
        _logger = logger;
        _potatisServices = potatisServices;
    }

    [Function("CreatePotatis")]
    public async Task<IResult> CreatePotatisRun([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Potatis")] HttpRequest req, string name, string type, string rank)
    {
        _logger.LogInformation("Creating new Potatis...");

        Potatis potatis = new Potatis()
        {
            Name = name,
            Type = type,
            Rank = rank
        };
        var newPotatis = await _potatisServices.AddAsync("Potatis", potatis);
        if (newPotatis == null)
        {
            return Results.BadRequest("Failed to create Potatis.");
        }

        return Results.Ok(newPotatis);
    }

    [Function("GetAllPotatis")]
    public async Task<IResult> GetAllPotatisRun([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Potatis")] HttpRequest req)
    {
        _logger.LogInformation("Fetching all Potatis...");
        var potatisList = await _potatisServices.GetAllAsync<Potatis>("Potatis");
        _logger.LogInformation("Fetched all Potatis successfully.");
        return Results.Ok(potatisList);
    }

    [Function("GetPotatisById")]
    public async Task<IResult> GetPotatisByIdRun([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Potatis/{id}")] HttpRequest req, string id)
    {
        _logger.LogInformation($"Getting Potatis with ID: {id}...");
        var potatis = await _potatisServices.GetByIdAsync<Potatis>("Potatis", id);
        if (potatis == null)
        {
            return Results.NotFound();
        }
        return potatis != null ? Results.Ok(potatis) : Results.NotFound();
    }

    [Function("UpdatePotatis")]
    public async Task<IResult> UpdatePotatisRun(
    [HttpTrigger(AuthorizationLevel.Function, "put", Route = "Potatis/{id}")] HttpRequest req,
    string id, string name, string type, string rank, Potatis potatis)
    {
        if (!ObjectId.TryParse(id, out _))
            return Results.BadRequest("Invalid id format.");

        var existingPotatis = await _potatisServices.GetByIdAsync<Potatis>("Potatis", id);
        if (existingPotatis == null)
            return Results.NotFound();

        // Update properties
        existingPotatis.Name = name;
        existingPotatis.Type = type;
        existingPotatis.Rank = rank;

        var updatedPotatis = await _potatisServices.UpdateAsync("Potatis", id, existingPotatis);
        return Results.Ok(updatedPotatis);
    }

    [Function("DeletePotatis")]
    public async Task<IResult> DeletePotatisRun([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Potatis/{id}")] HttpRequest req, string id)
    {
        _logger.LogInformation($"Deleting Potatis with ID: {id}...");
        var existingPotatis = await _potatisServices.GetByIdAsync<Potatis>("Potatis", id);
        if (existingPotatis == null)
        {
            return Results.NotFound();
        }
        var deletedPotatis = await _potatisServices.DeleteAsync<Potatis>("Potatis", id);
        return deletedPotatis ? Results.Ok() : Results.NotFound();
    }
}