using MDDPlatform.DomainModels.Infrastructure.Data.Seeders;
using Microsoft.AspNetCore.Mvc;

namespace MDDPlatform.DomainModels.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase 
{
    private readonly IDataSeeder _dataSeeder;

    public DataController(IDataSeeder dataSeeder)
    {
        _dataSeeder = dataSeeder;
    }
    [HttpGet("Seed/CRAC/{modelId}/Order")]
    public async Task SeedOrder(Guid modelId){
        await _dataSeeder.SeedOrderModelAsync(modelId);
    }
    [HttpGet("Seed/CRAC/{modelId}/Cart")]
    public async Task SeedCart(Guid modelId){
        await _dataSeeder.SeedCartModelAsync(modelId);
    }
    [HttpGet("Seed/CRAC/{modelId}/Product")]
    public async Task SeedProduct(Guid modelId){
        await _dataSeeder.SeedProductModelAsync(modelId);
    }
}
