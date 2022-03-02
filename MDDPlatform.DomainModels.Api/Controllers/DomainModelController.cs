using MDDPlatform.DomainModels.Application.DTO;
using MDDPlatform.DomainModels.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDDPlatform.DomainModels.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DomainModelController : ControllerBase
    {
        private readonly IDomainModelService _domainModelService;

        public DomainModelController(IDomainModelService domainModelService)
        {
            _domainModelService = domainModelService;
        }
        [HttpPost("{modelId}/Concept/Create")]
        public async Task Create(Guid modelId, [FromBody] NewConceptDto newConceptDto){
            await _domainModelService.CreateConceptAsync(newConceptDto.Name,newConceptDto.Type,modelId);
        }
        [HttpGet("{modelId}/Concepts/name/{name}")]
        public async Task<IList<DomainConceptDto>> FindConceptsByName(Guid modelId,[FromRoute] string name){
            return await _domainModelService.FindConceptsByNameAsync(modelId,name);
        }
        [HttpGet("{modelId}/Concepts/type/{type}")]
        public async Task<IList<DomainConceptDto>> FindConceptsByType(Guid modelId,[FromRoute] string type){
            return await _domainModelService.FindConceptsByTypeAsync(modelId,type);
        }
        [HttpGet("{modelId}/Concepts")]
        public async Task<IList<DomainConceptDto>> GetAllConcepts(Guid modelId){
            return await _domainModelService.GetAllConceptsAsync(modelId);
        }
        [HttpGet("{modelId}/Concept/name/{name}/type/{type}")]
        public async Task<DomainConceptDto> GetConcept(Guid modelId,[FromRoute] string name,string type){
            return await _domainModelService.GetConceptAsync(name,type,modelId);
        }
        [HttpGet("{modelId}/Concept/id/{conceptId}")]
        public async Task<DomainConceptDto> GetConceptById(Guid modelId,[FromRoute] Guid conceptId){
            return await _domainModelService.GetConceptByIdAsync(conceptId,modelId);
        }
        [HttpGet("{modelId}")]
        public async Task<DomainModelDto> GetDomainModel(Guid modelId){
            return await _domainModelService.GetDomainModelAsync(modelId);
        }
    }
}